namespace CilDisassembler
{
    using System.Windows.Forms;

    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.RawBytes = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.HasHeader = new System.Windows.Forms.CheckBox();
            this.HeaderInfo = new System.Windows.Forms.GroupBox();
            this.InitLocals = new System.Windows.Forms.Label();
            this.MoreSections = new System.Windows.Forms.Label();
            this.LocalVarSigTok = new System.Windows.Forms.Label();
            this.CodeSize = new System.Windows.Forms.Label();
            this.MaxStack = new System.Windows.Forms.Label();
            this.Size = new System.Windows.Forms.Label();
            this.HeaderType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.extraDataSections = new System.Windows.Forms.DataGridView();
            this.IsException = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsFilter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsFinally = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsFault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TryStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TryEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HandlerStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HandlerEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.disassembly = new System.Windows.Forms.DataGridView();
            this.Offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.back = new System.Windows.Forms.Button();
            this.goToSelectedRow = new System.Windows.Forms.Button();
            this.zoom = new System.Windows.Forms.TrackBar();
            this.statusStrip1.SuspendLayout();
            this.HeaderInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extraDataSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.disassembly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).BeginInit();
            this.SuspendLayout();
            // 
            // RawBytes
            // 
            this.RawBytes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RawBytes.Font = new System.Drawing.Font("Consolas", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RawBytes.Location = new System.Drawing.Point(23, 48);
            this.RawBytes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RawBytes.MaxLength = 524288;
            this.RawBytes.Multiline = true;
            this.RawBytes.Name = "RawBytes";
            this.RawBytes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RawBytes.Size = new System.Drawing.Size(90, 1210);
            this.RawBytes.TabIndex = 0;
            this.RawBytes.TextChanged += new System.EventHandler(this.RawBytes_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1261);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1540, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // HasHeader
            // 
            this.HasHeader.AutoSize = true;
            this.HasHeader.Checked = true;
            this.HasHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HasHeader.Location = new System.Drawing.Point(23, 14);
            this.HasHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HasHeader.Name = "HasHeader";
            this.HasHeader.Size = new System.Drawing.Size(88, 24);
            this.HasHeader.TabIndex = 2;
            this.HasHeader.Text = "Header";
            this.HasHeader.UseVisualStyleBackColor = true;
            this.HasHeader.CheckedChanged += new System.EventHandler(this.HasHeader_CheckedChanged);
            // 
            // HeaderInfo
            // 
            this.HeaderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeaderInfo.Controls.Add(this.InitLocals);
            this.HeaderInfo.Controls.Add(this.MoreSections);
            this.HeaderInfo.Controls.Add(this.LocalVarSigTok);
            this.HeaderInfo.Controls.Add(this.CodeSize);
            this.HeaderInfo.Controls.Add(this.MaxStack);
            this.HeaderInfo.Controls.Add(this.Size);
            this.HeaderInfo.Controls.Add(this.HeaderType);
            this.HeaderInfo.Controls.Add(this.label2);
            this.HeaderInfo.Controls.Add(this.label6);
            this.HeaderInfo.Controls.Add(this.label7);
            this.HeaderInfo.Controls.Add(this.label5);
            this.HeaderInfo.Controls.Add(this.label4);
            this.HeaderInfo.Controls.Add(this.label3);
            this.HeaderInfo.Controls.Add(this.label1);
            this.HeaderInfo.Enabled = false;
            this.HeaderInfo.Location = new System.Drawing.Point(125, 14);
            this.HeaderInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HeaderInfo.Name = "HeaderInfo";
            this.HeaderInfo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HeaderInfo.Size = new System.Drawing.Size(1402, 149);
            this.HeaderInfo.TabIndex = 3;
            this.HeaderInfo.TabStop = false;
            this.HeaderInfo.Text = "Header Information";
            // 
            // InitLocals
            // 
            this.InitLocals.AutoSize = true;
            this.InitLocals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitLocals.Location = new System.Drawing.Point(126, 83);
            this.InitLocals.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.InitLocals.Name = "InitLocals";
            this.InitLocals.Size = new System.Drawing.Size(0, 20);
            this.InitLocals.TabIndex = 0;
            // 
            // MoreSections
            // 
            this.MoreSections.AutoSize = true;
            this.MoreSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoreSections.Location = new System.Drawing.Point(126, 54);
            this.MoreSections.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MoreSections.Name = "MoreSections";
            this.MoreSections.Size = new System.Drawing.Size(0, 20);
            this.MoreSections.TabIndex = 0;
            // 
            // LocalVarSigTok
            // 
            this.LocalVarSigTok.AutoSize = true;
            this.LocalVarSigTok.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalVarSigTok.Location = new System.Drawing.Point(393, 112);
            this.LocalVarSigTok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LocalVarSigTok.Name = "LocalVarSigTok";
            this.LocalVarSigTok.Size = new System.Drawing.Size(0, 20);
            this.LocalVarSigTok.TabIndex = 0;
            // 
            // CodeSize
            // 
            this.CodeSize.AutoSize = true;
            this.CodeSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeSize.Location = new System.Drawing.Point(393, 83);
            this.CodeSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CodeSize.Name = "CodeSize";
            this.CodeSize.Size = new System.Drawing.Size(0, 20);
            this.CodeSize.TabIndex = 0;
            // 
            // MaxStack
            // 
            this.MaxStack.AutoSize = true;
            this.MaxStack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxStack.Location = new System.Drawing.Point(393, 54);
            this.MaxStack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaxStack.Name = "MaxStack";
            this.MaxStack.Size = new System.Drawing.Size(0, 20);
            this.MaxStack.TabIndex = 0;
            // 
            // Size
            // 
            this.Size.AutoSize = true;
            this.Size.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Size.Location = new System.Drawing.Point(393, 25);
            this.Size.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Size.Name = "Size";
            this.Size.Size = new System.Drawing.Size(0, 20);
            this.Size.TabIndex = 0;
            // 
            // HeaderType
            // 
            this.HeaderType.AutoSize = true;
            this.HeaderType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderType.Location = new System.Drawing.Point(126, 25);
            this.HeaderType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HeaderType.Name = "HeaderType";
            this.HeaderType.Size = new System.Drawing.Size(0, 20);
            this.HeaderType.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Init Locals";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "More Sections";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(260, 112);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "LocalVarSigTok";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(260, 83);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "CodeSize";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "MaxStack";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type";
            // 
            // extraDataSections
            // 
            this.extraDataSections.AllowUserToAddRows = false;
            this.extraDataSections.AllowUserToDeleteRows = false;
            this.extraDataSections.AllowUserToResizeRows = false;
            this.extraDataSections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extraDataSections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extraDataSections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsException,
            this.IsFilter,
            this.IsFinally,
            this.IsFault,
            this.TryStart,
            this.TryEnd,
            this.HandlerStart,
            this.HandlerEnd,
            this.Token});
            this.extraDataSections.Location = new System.Drawing.Point(125, 171);
            this.extraDataSections.MultiSelect = false;
            this.extraDataSections.Name = "extraDataSections";
            this.extraDataSections.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.extraDataSections.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.extraDataSections.RowHeadersVisible = false;
            this.extraDataSections.RowTemplate.Height = 28;
            this.extraDataSections.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.extraDataSections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.extraDataSections.Size = new System.Drawing.Size(1400, 292);
            this.extraDataSections.TabIndex = 4;
            this.extraDataSections.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtraDataSections_CellClick);
            // 
            // IsException
            // 
            this.IsException.HeaderText = "Exception";
            this.IsException.Name = "IsException";
            this.IsException.ReadOnly = true;
            this.IsException.Width = 35;
            // 
            // IsFilter
            // 
            this.IsFilter.HeaderText = "Filter";
            this.IsFilter.Name = "IsFilter";
            this.IsFilter.ReadOnly = true;
            this.IsFilter.Width = 35;
            // 
            // IsFinally
            // 
            this.IsFinally.HeaderText = "Finally";
            this.IsFinally.Name = "IsFinally";
            this.IsFinally.ReadOnly = true;
            this.IsFinally.Width = 35;
            // 
            // IsFault
            // 
            this.IsFault.HeaderText = "Fault";
            this.IsFault.Name = "IsFault";
            this.IsFault.ReadOnly = true;
            this.IsFault.Width = 35;
            // 
            // TryStart
            // 
            this.TryStart.HeaderText = "Try Start";
            this.TryStart.Name = "TryStart";
            this.TryStart.ReadOnly = true;
            // 
            // TryEnd
            // 
            this.TryEnd.HeaderText = "Try End";
            this.TryEnd.Name = "TryEnd";
            this.TryEnd.ReadOnly = true;
            this.TryEnd.Width = 200;
            // 
            // HandlerStart
            // 
            this.HandlerStart.HeaderText = "Handler Start";
            this.HandlerStart.Name = "HandlerStart";
            this.HandlerStart.ReadOnly = true;
            // 
            // HandlerEnd
            // 
            this.HandlerEnd.HeaderText = "Handler End";
            this.HandlerEnd.Name = "HandlerEnd";
            this.HandlerEnd.ReadOnly = true;
            this.HandlerEnd.Width = 200;
            // 
            // Token
            // 
            this.Token.HeaderText = "Token";
            this.Token.Name = "Token";
            this.Token.ReadOnly = true;
            this.Token.Width = 150;
            // 
            // disassembly
            // 
            this.disassembly.AllowUserToAddRows = false;
            this.disassembly.AllowUserToDeleteRows = false;
            this.disassembly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.disassembly.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.disassembly.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.disassembly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.disassembly.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Offset,
            this.OpCode,
            this.Operand});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.disassembly.DefaultCellStyle = dataGridViewCellStyle2;
            this.disassembly.Location = new System.Drawing.Point(125, 544);
            this.disassembly.MultiSelect = false;
            this.disassembly.Name = "disassembly";
            this.disassembly.ReadOnly = true;
            this.disassembly.RowHeadersVisible = false;
            this.disassembly.RowTemplate.Height = 28;
            this.disassembly.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.disassembly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.disassembly.Size = new System.Drawing.Size(1400, 714);
            this.disassembly.TabIndex = 5;
            this.disassembly.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Disassembly_CellDoubleClick);
            // 
            // Offset
            // 
            this.Offset.HeaderText = "Offset";
            this.Offset.Name = "Offset";
            this.Offset.ReadOnly = true;
            this.Offset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Offset.Width = 59;
            // 
            // OpCode
            // 
            this.OpCode.HeaderText = "OpCode";
            this.OpCode.Name = "OpCode";
            this.OpCode.ReadOnly = true;
            this.OpCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OpCode.Width = 74;
            // 
            // Operand
            // 
            this.Operand.HeaderText = "Operand";
            this.Operand.Name = "Operand";
            this.Operand.ReadOnly = true;
            this.Operand.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Operand.Width = 77;
            // 
            // back
            // 
            this.back.Image = ((System.Drawing.Image)(resources.GetObject("back.Image")));
            this.back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.back.Location = new System.Drawing.Point(125, 469);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(90, 43);
            this.back.TabIndex = 6;
            this.back.Text = "Back";
            this.back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // goToSelectedRow
            // 
            this.goToSelectedRow.Image = ((System.Drawing.Image)(resources.GetObject("goToSelectedRow.Image")));
            this.goToSelectedRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.goToSelectedRow.Location = new System.Drawing.Point(221, 469);
            this.goToSelectedRow.Name = "goToSelectedRow";
            this.goToSelectedRow.Size = new System.Drawing.Size(195, 43);
            this.goToSelectedRow.TabIndex = 6;
            this.goToSelectedRow.Text = "Go To Selected Row";
            this.goToSelectedRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.goToSelectedRow.UseVisualStyleBackColor = true;
            this.goToSelectedRow.Click += new System.EventHandler(this.GoToSelectedRow_Click);
            // 
            // zoom
            // 
            this.zoom.LargeChange = 2;
            this.zoom.Location = new System.Drawing.Point(1189, 469);
            this.zoom.Maximum = 30;
            this.zoom.Minimum = 1;
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(335, 69);
            this.zoom.TabIndex = 7;
            this.zoom.Value = 10;
            this.zoom.Scroll += new System.EventHandler(this.zoom_Scroll);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 1283);
            this.Controls.Add(this.zoom);
            this.Controls.Add(this.goToSelectedRow);
            this.Controls.Add(this.back);
            this.Controls.Add(this.disassembly);
            this.Controls.Add(this.extraDataSections);
            this.Controls.Add(this.HeaderInfo);
            this.Controls.Add(this.HasHeader);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.RawBytes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "CIL Disassembler";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.HeaderInfo.ResumeLayout(false);
            this.HeaderInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extraDataSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.disassembly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RawBytes;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox HasHeader;
        private System.Windows.Forms.GroupBox HeaderInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label InitLocals;
        private System.Windows.Forms.Label MoreSections;
        private System.Windows.Forms.Label LocalVarSigTok;
        private System.Windows.Forms.Label CodeSize;
        private System.Windows.Forms.Label MaxStack;
        private System.Windows.Forms.Label Size;
        private System.Windows.Forms.Label HeaderType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView extraDataSections;
        private System.Windows.Forms.DataGridView disassembly;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsException;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFilter;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFinally;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFault;
        private System.Windows.Forms.DataGridViewTextBoxColumn TryStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn TryEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn HandlerStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn HandlerEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Token;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operand;
        private Button back;
        private Button goToSelectedRow;
        private TrackBar zoom;
    }
}

