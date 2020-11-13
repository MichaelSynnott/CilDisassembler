namespace CilDisassembler
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;


    public partial class MainForm : Form
    {
        private string _codeBytes;
        private int _codeSize;
        private int _headerSize;

        private readonly Stack<int> _trackBack = new Stack<int>();

        private int _highlightedRow = -1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void RawBytes_TextChanged(object sender, EventArgs e)
        {
            RawBytes.TextChanged -= RawBytes_TextChanged;
            ProcessBytes();
            RawBytes.TextChanged += RawBytes_TextChanged;
        }

        private void ProcessBytes()
        {
            try
            {
                _trackBack.Clear();

                this.toolStripStatusLabel1.Text = string.Empty;
                this.disassembly.Text = string.Empty;
                ResetHeaderTextBoxes();
                this.RawBytes.Text = FormatHelper.Lineify(this.RawBytes.Text);
                var codeAndHeaderBytes = FormatHelper.PurgeSeparators(this.RawBytes.Text);
                HandleHeader(codeAndHeaderBytes);
                int.TryParse(this.CodeSize.Text, out _codeSize);
                var codeData = OpCodes.Disassemble(_codeBytes, _codeSize);

                this.disassembly.Rows.Clear();
                PopulateCodeGrid(codeData);

                this.extraDataSections.Rows.Clear();
                PopulateExtraDataSectionsGrid(codeAndHeaderBytes);

                _trackBack.Push(0);
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabel1.Text = ex.Message.Replace("\r\n", "\u0020");
            }
        }

        private void PopulateExtraDataSectionsGrid(string codeAndHeaderBytes)
        {
            if (!this.HasHeader.Checked) return;
            if (_headerSize != 12) return;
            if (string.IsNullOrEmpty(codeAndHeaderBytes)) return;

            var headerAndCodeSize = 12 + _codeSize;
            var multipleOfFour = headerAndCodeSize / 4;
            int boundaryPaddingSize = 4 - (headerAndCodeSize - multipleOfFour * 4);
            boundaryPaddingSize = boundaryPaddingSize == 4 ? 0 : boundaryPaddingSize;

            var offset = headerAndCodeSize + boundaryPaddingSize;
            byte[] extraDataSectionsBytes = ConvertToByteArray(codeAndHeaderBytes.Substring(offset * 2));

            int bytePointer = 0;

            var isExceptionHandlingData = (extraDataSectionsBytes[bytePointer] & CorILMethodSect.CorILMethodSectEHTable) == CorILMethodSect.CorILMethodSectEHTable;
            var isFatFormat = (extraDataSectionsBytes[bytePointer] & CorILMethodSect.CorILMethodSectFatFormat) == CorILMethodSect.CorILMethodSectFatFormat;
            var hasMoreSections = (extraDataSectionsBytes[bytePointer] & CorILMethodSect.CorILMethodSectMoreSects) == CorILMethodSect.CorILMethodSectMoreSects;

            bytePointer++;
            var dataSize = 0;
            if (isFatFormat)
            {
                dataSize = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8 | extraDataSectionsBytes[bytePointer + 2] << 16;
                bytePointer += 3;
            }
            else
            {
                dataSize = extraDataSectionsBytes[bytePointer];
                bytePointer += 3; // We skip 3 bytes here because there are 2 reserved bytes after the length in a small format header
            }


            int flags;
            while (bytePointer < extraDataSectionsBytes.Length)
            {
                var gridRow = new DataGridViewRow();

                if (isFatFormat)
                {
                    flags = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8 | extraDataSectionsBytes[bytePointer + 2] << 16 | extraDataSectionsBytes[bytePointer + 3] << 24;
                    bytePointer += 4;
                }
                else
                {
                    flags = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8;
                    bytePointer += 2;
                }

                gridRow.Cells.Add(new DataGridViewCheckBoxCell { Value = (flags & CorExceptionFlag.CorILExceptionClauseNone) == CorExceptionFlag.CorILExceptionClauseNone });
                gridRow.Cells.Add(new DataGridViewCheckBoxCell { Value = (flags & CorExceptionFlag.CorILExceptionClauseFilter) == CorExceptionFlag.CorILExceptionClauseFilter });
                gridRow.Cells.Add(new DataGridViewCheckBoxCell { Value = (flags & CorExceptionFlag.CorILExceptionClauseFinally) == CorExceptionFlag.CorILExceptionClauseFinally });
                gridRow.Cells.Add(new DataGridViewCheckBoxCell { Value = (flags & CorExceptionFlag.CorILExceptionClauseFault) == CorExceptionFlag.CorILExceptionClauseFinally });

                int tryOffset;
                if (isFatFormat)
                {
                    tryOffset = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8 | extraDataSectionsBytes[bytePointer + 2] << 16 | extraDataSectionsBytes[bytePointer + 3] << 24;
                    bytePointer += 4;
                }
                else
                {
                    tryOffset = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8;
                    bytePointer += 2;
                }
                gridRow.Cells.Add(new DataGridViewTextBoxCell { Value = $"0x{tryOffset,4:X4}", Tag = tryOffset});

                int tryLength;
                if (isFatFormat)
                {
                    tryLength = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8 | extraDataSectionsBytes[bytePointer + 2] << 16 | extraDataSectionsBytes[bytePointer + 3] << 24;
                    bytePointer += 4;
                }
                else
                {
                    tryLength = extraDataSectionsBytes[bytePointer];
                    bytePointer++;
                }
                gridRow.Cells.Add(new DataGridViewTextBoxCell { Value = $"0x{tryOffset + tryLength-1,4:X4} (0x{tryLength,4:X4}; {tryLength})", Tag = tryLength });

                int handlerOffset = 0;
                if (isFatFormat)
                {
                    handlerOffset = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8 | extraDataSectionsBytes[bytePointer + 2] << 16 | extraDataSectionsBytes[bytePointer + 3] << 24;
                    bytePointer += 4;
                }
                else
                {
                    handlerOffset = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8;
                    bytePointer += 2;
                }
                gridRow.Cells.Add(new DataGridViewTextBoxCell { Value = $"0x{handlerOffset,4:X4}", Tag = handlerOffset });

                int handlerLength = 0;
                if (isFatFormat)
                {
                    handlerLength = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8 | extraDataSectionsBytes[bytePointer + 2] << 16 | extraDataSectionsBytes[bytePointer + 3] << 24;
                    bytePointer += 4;
                }
                else
                {
                    handlerLength = extraDataSectionsBytes[bytePointer];
                    bytePointer++;
                }
                gridRow.Cells.Add(new DataGridViewTextBoxCell { Value = $"0x{handlerOffset + handlerLength - 1,4:X4} (0x{handlerLength,4:X4}; {handlerLength})", Tag = handlerLength });

                int classTokenOrFilterOffset = extraDataSectionsBytes[bytePointer] | extraDataSectionsBytes[bytePointer + 1] << 8 | extraDataSectionsBytes[bytePointer + 2] << 16 | extraDataSectionsBytes[bytePointer + 3] << 24;
                bytePointer += 4;
                gridRow.Cells.Add(new DataGridViewTextBoxCell { Value = $"0x{classTokenOrFilterOffset,8:X8}" });

                gridRow.DefaultCellStyle.Font = new Font(new FontFamily("Consolas", new InstalledFontCollection()), 10);
                gridRow.Resizable = DataGridViewTriState.False;
                this.extraDataSections.Rows.Add(gridRow);
            }
        }


        private byte[] ConvertToByteArray(string byteData)
        {
            var retVal = new byte[byteData.Length / 2];

            var j = 0;
            for (var i = 0; i < byteData.Length; i += 2)
            {
                retVal[j++] = byte.Parse(byteData.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            }

            return retVal;
        }

        private void PopulateCodeGrid(string codeData)
        {
            if (string.IsNullOrEmpty(codeData)) return;

            var rowDatas = codeData.Split(new [] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rowData in rowDatas)
            {
                var row = rowData.Split(',');
                this.disassembly.Rows.Add(row[0], row[1], row[2]);
                this.disassembly.Rows[this.disassembly.Rows.Count - 1].MinimumHeight = 2;
            }
        }

        private void ResetHeaderTextBoxes()
        {
            this.HeaderType.Text = string.Empty;
            this.MoreSections.Text = string.Empty;
            this.InitLocals.Text = string.Empty;
            this.Size.Text = string.Empty;
            this.MaxStack.Text = string.Empty;
            this.CodeSize.Text = string.Empty;
            this.LocalVarSigTok.Text = string.Empty;
        }

        private void HandleHeader(string bytesWithHeader)
        {
            if (string.IsNullOrEmpty(bytesWithHeader) || !this.HasHeader.Checked)
            {
                _codeBytes = bytesWithHeader;
                return;
            }

            var byt = bytesWithHeader.Substring(0, 2);
            byte byteValue = byte.Parse(byt, NumberStyles.AllowHexSpecifier);

            if ((byteValue & 0b11) == CorILMethod.TinyFormat)
            {
                this.HeaderType.Text = "Tiny";
                this.MoreSections.Text = "n/a";
                this.InitLocals.Text = "n/a";
                this.Size.Text = "n/a";
                this.MaxStack.Text = "n/a";
                this.CodeSize.Text = "" + (byteValue >> 2);
                this.LocalVarSigTok.Text = "n/a";

                _codeBytes = bytesWithHeader.Substring(2);
                _headerSize = 1;
                return;
            }

            if ((byteValue & 0b11) == CorILMethod.FatFormat)
            {
                var flagsAndSize = bytesWithHeader.Substring(0, 4);
                var littleEndianWord = flagsAndSize.Substring(2, 2) + flagsAndSize.Substring(0, 2);
                Int16 wordValue = Int16.Parse(littleEndianWord, NumberStyles.AllowHexSpecifier);

                this.HeaderType.Text = "Fat";
                this.MoreSections.Text = (wordValue & CorILMethod.MoreSections) == CorILMethod.MoreSections ? "Yes" : "No";
                this.InitLocals.Text = (wordValue & CorILMethod.InitLocals) == CorILMethod.InitLocals ? "Yes" : "No";
                this.Size.Text = "" + (wordValue >> 12);

                var maxStack = bytesWithHeader.Substring(4, 4);
                var littleEndianMaxStack = maxStack.Substring(2, 2) + maxStack.Substring(0, 2);
                Int16 maxStackValue = Int16.Parse(littleEndianMaxStack, NumberStyles.AllowHexSpecifier);
                this.MaxStack.Text = "" + maxStackValue;

                var codeSize = bytesWithHeader.Substring(8, 8);
                var littleEndianCodeSize = codeSize.Substring(6, 2) + codeSize.Substring(4, 2) + codeSize.Substring(2, 2) + codeSize.Substring(0, 2);
                Int32 codeSizeValue = Int32.Parse(littleEndianCodeSize, NumberStyles.AllowHexSpecifier);
                this.CodeSize.Text = "" + codeSizeValue;

                var localVarSigTok = bytesWithHeader.Substring(16, 8);
                var littleEndianLocalVarSigTok = localVarSigTok.Substring(6, 2) + localVarSigTok.Substring(4, 2) + localVarSigTok.Substring(2, 2) + localVarSigTok.Substring(0, 2);
                this.LocalVarSigTok.Text = "0x" + littleEndianLocalVarSigTok;

                _codeBytes = bytesWithHeader.Substring(24);
                _headerSize = 12;
                return;
            }

            throw new BadImageFormatException($"CIL Header is neither a Tiny (0x02) nor a Fat (0x03) header. Actual value found is {byteValue & 0b11}");
        }

        private void HasHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (this.HasHeader.Checked)
            {
                this.HeaderInfo.Enabled = true;
            }
            else
            {
                this.HeaderInfo.Enabled = false;
                ResetHeaderTextBoxes();
            }
            ProcessBytes();
        }

        private void Disassembly_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.disassembly.Rows.Count == 0) return;

            var row = this.disassembly.Rows[e.RowIndex];
            var operand = row.Cells[2].Value as string;

            if (string.IsNullOrEmpty(operand)) return;

            var targetAddress = ExtractBranchTargetAddress(operand);

            if (string.IsNullOrEmpty(targetAddress)) return;

            var i = -1;
            while (this.disassembly.Rows[++i].Cells[0].Value as string != targetAddress && i < this.disassembly.Rows.Count) ;

            _trackBack.Push(e.RowIndex);

            //this.disassembly.FirstDisplayedScrollingRowIndex = i;
            NavigateToCodeRowAndHighlight(i);
        }

        private void NavigateToCodeRowAndHighlight(int row)
        {
            if (_highlightedRow != -1)
            {
                this.disassembly.Rows[_highlightedRow].Selected = false;
            }

            this.disassembly.Rows[row].Selected = true;
            this.disassembly.FirstDisplayedScrollingRowIndex = row;
            
            _highlightedRow = row;
        }

        private static string ExtractBranchTargetAddress(string operand)
        {
            var pattern = @"^.*\(\s(IL_[0-9A-F]{4})\s\)$";
            var regex = new Regex(pattern);
            var m = regex.Match(operand);
            return !m.Success ? null : m.Groups[1].Value;
        }

        private void ExtraDataSections_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || this.extraDataSections.Rows.Count == 0) return;

            _trackBack.Push(this.disassembly.FirstDisplayedScrollingRowIndex);

            ResetCodeGridBackground();

            var row = this.extraDataSections.Rows[e.RowIndex];

            var tryStart = (int) (row.Cells[4].Tag);
            var tryStartOffset = $"IL_{tryStart,4:X4}";

            var tryLength = (int)(row.Cells[5].Tag);
            var tryEndOffset = $"IL_{tryStart + tryLength,4:X4}";

            var handlerStart = (int)(row.Cells[6].Tag);
            var handlerStartOffset = $"IL_{handlerStart,4:X4}";

            var handlerLength = (int)(row.Cells[7].Tag);
            var handlerEndOffset = $"IL_{handlerStart + handlerLength,4:X4}";

            var i = -1;
            while (this.disassembly.Rows[++i].Cells[0].Value as string != tryStartOffset && i < this.disassembly.Rows.Count) ;

            //this.disassembly.FirstDisplayedScrollingRowIndex = i;
            NavigateToCodeRowAndHighlight(i);

            while (this.disassembly.Rows[i].Cells[0].Value as string != tryEndOffset && i < this.disassembly.Rows.Count)
            {
                this.disassembly.Rows[i++].DefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
            }

            i--;
            while (this.disassembly.Rows[++i].Cells[0].Value as string != handlerStartOffset && i < this.disassembly.Rows.Count) ;

            while (this.disassembly.Rows[i].Cells[0].Value as string != handlerEndOffset && i < this.disassembly.Rows.Count)
            {
                this.disassembly.Rows[i++].DefaultCellStyle.BackColor = SystemColors.Info;
            }

        }

        private void ResetCodeGridBackground()
        {
            for (var r = 0; r < this.disassembly.Rows.Count; r++)
            {
                this.disassembly.Rows[r].DefaultCellStyle.BackColor = SystemColors.Window;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (_trackBack.Count == 0) return;

            var row = _trackBack.Pop();
            //this.disassembly.FirstDisplayedScrollingRowIndex = row;
            NavigateToCodeRowAndHighlight(row);
        }

        private void GoToSelectedRow_Click(object sender, EventArgs e)
        {
            if (this.disassembly.SelectedRows.Count == 0) return;
            this.disassembly.FirstDisplayedScrollingRowIndex = this.disassembly.SelectedRows[0].Index;
        }

        private void zoom_Scroll(object sender, EventArgs e)
        {
            float fontSize = this.zoom.Value;
            ScaleCodeGrid(fontSize);
        }

        private void ScaleCodeGrid(float fontSize)
        {
            var existingFont = this.disassembly.DefaultCellStyle.Font;
            this.disassembly.DefaultCellStyle.Font = new Font(existingFont.FontFamily, fontSize);
        }
    }
}
