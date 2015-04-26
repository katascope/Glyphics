namespace WinGlyphics
{
    partial class WinGlyphics
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveDirectPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveObliquePNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySerializedRectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySerializedRectsLimit255ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxSerializedRects = new System.Windows.Forms.TextBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.labelSerializedRects = new System.Windows.Forms.Label();
            this.labelSerializedRectsLimit255 = new System.Windows.Forms.Label();
            this.textBoxSerializedRectsLimit255 = new System.Windows.Forms.TextBox();
            this.comboBoxGly = new System.Windows.Forms.ComboBox();
            this.labelTokens = new System.Windows.Forms.Label();
            this.textBoxTokens = new System.Windows.Forms.TextBox();
            this.labelRects = new System.Windows.Forms.Label();
            this.textBoxRects = new System.Windows.Forms.TextBox();
            this.labelQuads = new System.Windows.Forms.Label();
            this.textBoxQuads = new System.Windows.Forms.TextBox();
            this.labelTriangles = new System.Windows.Forms.Label();
            this.textBoxTriangles = new System.Windows.Forms.TextBox();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1481, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveDirectPNGToolStripMenuItem,
            this.saveObliquePNGToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridToolStripMenuItem,
            this.modelToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modelToolStripMenuItem.Text = "Model";
            this.modelToolStripMenuItem.Click += new System.EventHandler(this.modelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // saveDirectPNGToolStripMenuItem
            // 
            this.saveDirectPNGToolStripMenuItem.Name = "saveDirectPNGToolStripMenuItem";
            this.saveDirectPNGToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveDirectPNGToolStripMenuItem.Text = "Save Direct PNG";
            // 
            // saveObliquePNGToolStripMenuItem
            // 
            this.saveObliquePNGToolStripMenuItem.Name = "saveObliquePNGToolStripMenuItem";
            this.saveObliquePNGToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveObliquePNGToolStripMenuItem.Text = "Save Oblique PNG";
            this.saveObliquePNGToolStripMenuItem.Click += new System.EventHandler(this.saveObliquePNGToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySerializedRectsToolStripMenuItem,
            this.copySerializedRectsLimit255ToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copySerializedRectsToolStripMenuItem
            // 
            this.copySerializedRectsToolStripMenuItem.Name = "copySerializedRectsToolStripMenuItem";
            this.copySerializedRectsToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.copySerializedRectsToolStripMenuItem.Text = "&Copy Serialized Rects";
            this.copySerializedRectsToolStripMenuItem.Click += new System.EventHandler(this.copySerializedRectsToolStripMenuItem_Click);
            // 
            // copySerializedRectsLimit255ToolStripMenuItem
            // 
            this.copySerializedRectsLimit255ToolStripMenuItem.Name = "copySerializedRectsLimit255ToolStripMenuItem";
            this.copySerializedRectsLimit255ToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.copySerializedRectsLimit255ToolStripMenuItem.Text = "Copy Serialized Rects Limit 255";
            this.copySerializedRectsLimit255ToolStripMenuItem.Click += new System.EventHandler(this.copySerializedRectsLimit255ToolStripMenuItem_Click);
            // 
            // textBoxMain
            // 
            this.textBoxMain.Location = new System.Drawing.Point(13, 61);
            this.textBoxMain.Multiline = true;
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.Size = new System.Drawing.Size(276, 404);
            this.textBoxMain.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(296, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(776, 784);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // textBoxSerializedRects
            // 
            this.textBoxSerializedRects.Location = new System.Drawing.Point(1075, 78);
            this.textBoxSerializedRects.Multiline = true;
            this.textBoxSerializedRects.Name = "textBoxSerializedRects";
            this.textBoxSerializedRects.ReadOnly = true;
            this.textBoxSerializedRects.Size = new System.Drawing.Size(394, 177);
            this.textBoxSerializedRects.TabIndex = 3;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(13, 28);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 4;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // labelSerializedRects
            // 
            this.labelSerializedRects.AutoSize = true;
            this.labelSerializedRects.Location = new System.Drawing.Point(1075, 62);
            this.labelSerializedRects.Name = "labelSerializedRects";
            this.labelSerializedRects.Size = new System.Drawing.Size(83, 13);
            this.labelSerializedRects.TabIndex = 5;
            this.labelSerializedRects.Text = "Serialized Rects";
            // 
            // labelSerializedRectsLimit255
            // 
            this.labelSerializedRectsLimit255.AutoSize = true;
            this.labelSerializedRectsLimit255.Location = new System.Drawing.Point(1075, 258);
            this.labelSerializedRectsLimit255.Name = "labelSerializedRectsLimit255";
            this.labelSerializedRectsLimit255.Size = new System.Drawing.Size(130, 13);
            this.labelSerializedRectsLimit255.TabIndex = 6;
            this.labelSerializedRectsLimit255.Text = "Serialized Rects (limit 255)";
            // 
            // textBoxSerializedRectsLimit255
            // 
            this.textBoxSerializedRectsLimit255.Location = new System.Drawing.Point(1078, 274);
            this.textBoxSerializedRectsLimit255.Multiline = true;
            this.textBoxSerializedRectsLimit255.Name = "textBoxSerializedRectsLimit255";
            this.textBoxSerializedRectsLimit255.ReadOnly = true;
            this.textBoxSerializedRectsLimit255.Size = new System.Drawing.Size(391, 80);
            this.textBoxSerializedRectsLimit255.TabIndex = 7;
            // 
            // comboBoxGly
            // 
            this.comboBoxGly.FormattingEnabled = true;
            this.comboBoxGly.Location = new System.Drawing.Point(94, 30);
            this.comboBoxGly.Name = "comboBoxGly";
            this.comboBoxGly.Size = new System.Drawing.Size(1302, 21);
            this.comboBoxGly.TabIndex = 8;
            this.comboBoxGly.SelectedIndexChanged += new System.EventHandler(this.comboBoxGly_SelectedIndexChanged);
            // 
            // labelTokens
            // 
            this.labelTokens.AutoSize = true;
            this.labelTokens.Location = new System.Drawing.Point(12, 468);
            this.labelTokens.Name = "labelTokens";
            this.labelTokens.Size = new System.Drawing.Size(43, 13);
            this.labelTokens.TabIndex = 9;
            this.labelTokens.Text = "Tokens";
            // 
            // textBoxTokens
            // 
            this.textBoxTokens.Location = new System.Drawing.Point(12, 484);
            this.textBoxTokens.Multiline = true;
            this.textBoxTokens.Name = "textBoxTokens";
            this.textBoxTokens.ReadOnly = true;
            this.textBoxTokens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTokens.Size = new System.Drawing.Size(277, 361);
            this.textBoxTokens.TabIndex = 10;
            // 
            // labelRects
            // 
            this.labelRects.AutoSize = true;
            this.labelRects.Location = new System.Drawing.Point(1078, 357);
            this.labelRects.Name = "labelRects";
            this.labelRects.Size = new System.Drawing.Size(35, 13);
            this.labelRects.TabIndex = 11;
            this.labelRects.Text = "Rects";
            // 
            // textBoxRects
            // 
            this.textBoxRects.Location = new System.Drawing.Point(1078, 373);
            this.textBoxRects.Multiline = true;
            this.textBoxRects.Name = "textBoxRects";
            this.textBoxRects.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRects.Size = new System.Drawing.Size(391, 92);
            this.textBoxRects.TabIndex = 12;
            // 
            // labelQuads
            // 
            this.labelQuads.AutoSize = true;
            this.labelQuads.Location = new System.Drawing.Point(1078, 468);
            this.labelQuads.Name = "labelQuads";
            this.labelQuads.Size = new System.Drawing.Size(38, 13);
            this.labelQuads.TabIndex = 13;
            this.labelQuads.Text = "Quads";
            // 
            // textBoxQuads
            // 
            this.textBoxQuads.Location = new System.Drawing.Point(1081, 484);
            this.textBoxQuads.Multiline = true;
            this.textBoxQuads.Name = "textBoxQuads";
            this.textBoxQuads.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxQuads.Size = new System.Drawing.Size(388, 143);
            this.textBoxQuads.TabIndex = 14;
            // 
            // labelTriangles
            // 
            this.labelTriangles.AutoSize = true;
            this.labelTriangles.Location = new System.Drawing.Point(1081, 630);
            this.labelTriangles.Name = "labelTriangles";
            this.labelTriangles.Size = new System.Drawing.Size(50, 13);
            this.labelTriangles.TabIndex = 15;
            this.labelTriangles.Text = "Triangles";
            // 
            // textBoxTriangles
            // 
            this.textBoxTriangles.Location = new System.Drawing.Point(1081, 646);
            this.textBoxTriangles.Multiline = true;
            this.textBoxTriangles.Name = "textBoxTriangles";
            this.textBoxTriangles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTriangles.Size = new System.Drawing.Size(388, 129);
            this.textBoxTriangles.TabIndex = 16;
            // 
            // WinGlyphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 857);
            this.Controls.Add(this.textBoxTriangles);
            this.Controls.Add(this.labelTriangles);
            this.Controls.Add(this.textBoxQuads);
            this.Controls.Add(this.labelQuads);
            this.Controls.Add(this.textBoxRects);
            this.Controls.Add(this.labelRects);
            this.Controls.Add(this.textBoxTokens);
            this.Controls.Add(this.labelTokens);
            this.Controls.Add(this.comboBoxGly);
            this.Controls.Add(this.textBoxSerializedRectsLimit255);
            this.Controls.Add(this.labelSerializedRectsLimit255);
            this.Controls.Add(this.labelSerializedRects);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.textBoxSerializedRects);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "WinGlyphics";
            this.Text = "WinGlyphics";
            this.Load += new System.EventHandler(this.WinGlyphics_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox textBoxMain;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxSerializedRects;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySerializedRectsToolStripMenuItem;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Label labelSerializedRects;
        private System.Windows.Forms.Label labelSerializedRectsLimit255;
        private System.Windows.Forms.TextBox textBoxSerializedRectsLimit255;
        private System.Windows.Forms.ToolStripMenuItem copySerializedRectsLimit255ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxGly;
        private System.Windows.Forms.Label labelTokens;
        private System.Windows.Forms.TextBox textBoxTokens;
        private System.Windows.Forms.ToolStripMenuItem saveDirectPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveObliquePNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label labelRects;
        private System.Windows.Forms.TextBox textBoxRects;
        private System.Windows.Forms.Label labelQuads;
        private System.Windows.Forms.TextBox textBoxQuads;
        private System.Windows.Forms.Label labelTriangles;
        private System.Windows.Forms.TextBox textBoxTriangles;
    }
}

