namespace CurtainWindows
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.trackBarPosition = new System.Windows.Forms.TrackBar();
            this.btnGo = new System.Windows.Forms.Button();
            this.lblPortName = new System.Windows.Forms.Label();
            this.tbPortName = new System.Windows.Forms.TextBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.tbBaudRate = new System.Windows.Forms.TextBox();
            this.tbPosition = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnConnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarPosition
            // 
            this.trackBarPosition.Location = new System.Drawing.Point(12, 44);
            this.trackBarPosition.Maximum = 1050;
            this.trackBarPosition.Name = "trackBarPosition";
            this.trackBarPosition.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBarPosition.Size = new System.Drawing.Size(781, 45);
            this.trackBarPosition.TabIndex = 0;
            this.trackBarPosition.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(13, 96);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(780, 23);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "G O";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(13, 13);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(55, 13);
            this.lblPortName.TabIndex = 2;
            this.lblPortName.Text = "Port name";
            // 
            // tbPortName
            // 
            this.tbPortName.Location = new System.Drawing.Point(74, 10);
            this.tbPortName.Name = "tbPortName";
            this.tbPortName.Size = new System.Drawing.Size(100, 20);
            this.tbPortName.TabIndex = 3;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(193, 13);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(53, 13);
            this.lblBaudRate.TabIndex = 4;
            this.lblBaudRate.Text = "Baud rate";
            // 
            // tbBaudRate
            // 
            this.tbBaudRate.Location = new System.Drawing.Point(252, 10);
            this.tbBaudRate.Name = "tbBaudRate";
            this.tbBaudRate.Size = new System.Drawing.Size(100, 20);
            this.tbBaudRate.TabIndex = 5;
            // 
            // tbPosition
            // 
            this.tbPosition.Enabled = false;
            this.tbPosition.ForeColor = System.Drawing.Color.Black;
            this.tbPosition.Location = new System.Drawing.Point(761, 12);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.Size = new System.Drawing.Size(32, 20);
            this.tbPosition.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 133);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(805, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(370, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 155);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbPosition);
            this.Controls.Add(this.tbBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.tbPortName);
            this.Controls.Add(this.lblPortName);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.trackBarPosition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Curtain controller";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarPosition;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.TextBox tbPortName;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.TextBox tbBaudRate;
        private System.Windows.Forms.TextBox tbPosition;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button btnConnect;
    }
}

