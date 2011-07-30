namespace Medius
{
    partial class SplitPostDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitTitleText = new System.Windows.Forms.TextBox();
            this.splitTitleLabel = new System.Windows.Forms.Label();
            this.existingTitleText = new System.Windows.Forms.TextBox();
            this.existingTitleLabel = new System.Windows.Forms.Label();
            this.splitPointText = new System.Windows.Forms.TextBox();
            this.splitPointLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.existingContentLabel = new System.Windows.Forms.Label();
            this.existingContentBrowser = new System.Windows.Forms.WebBrowser();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContentLabel = new System.Windows.Forms.Label();
            this.splitContentBrowser = new System.Windows.Forms.WebBrowser();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(707, 373);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitTitleText);
            this.panel1.Controls.Add(this.splitTitleLabel);
            this.panel1.Controls.Add(this.existingTitleText);
            this.panel1.Controls.Add(this.existingTitleLabel);
            this.panel1.Controls.Add(this.splitPointText);
            this.panel1.Controls.Add(this.splitPointLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 367);
            this.panel1.TabIndex = 0;
            // 
            // splitTitleText
            // 
            this.splitTitleText.Location = new System.Drawing.Point(0, 60);
            this.splitTitleText.Name = "splitTitleText";
            this.splitTitleText.Size = new System.Drawing.Size(234, 20);
            this.splitTitleText.TabIndex = 5;
            // 
            // splitTitleLabel
            // 
            this.splitTitleLabel.AutoSize = true;
            this.splitTitleLabel.Location = new System.Drawing.Point(3, 44);
            this.splitTitleLabel.Name = "splitTitleLabel";
            this.splitTitleLabel.Size = new System.Drawing.Size(57, 13);
            this.splitTitleLabel.TabIndex = 4;
            this.splitTitleLabel.Text = "Part 2 title:";
            // 
            // existingTitleText
            // 
            this.existingTitleText.Location = new System.Drawing.Point(0, 21);
            this.existingTitleText.Name = "existingTitleText";
            this.existingTitleText.Size = new System.Drawing.Size(234, 20);
            this.existingTitleText.TabIndex = 3;
            // 
            // existingTitleLabel
            // 
            this.existingTitleLabel.AutoSize = true;
            this.existingTitleLabel.Location = new System.Drawing.Point(3, 5);
            this.existingTitleLabel.Name = "existingTitleLabel";
            this.existingTitleLabel.Size = new System.Drawing.Size(57, 13);
            this.existingTitleLabel.TabIndex = 2;
            this.existingTitleLabel.Text = "Part 1 title:";
            // 
            // splitPointText
            // 
            this.splitPointText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitPointText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitPointText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitPointText.Location = new System.Drawing.Point(0, 99);
            this.splitPointText.Multiline = true;
            this.splitPointText.Name = "splitPointText";
            this.splitPointText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.splitPointText.Size = new System.Drawing.Size(234, 268);
            this.splitPointText.TabIndex = 1;
            this.splitPointText.Click += new System.EventHandler(this.updatePreview);
            this.splitPointText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitPointText_KeyDown);
            // 
            // splitPointLabel
            // 
            this.splitPointLabel.AutoSize = true;
            this.splitPointLabel.Location = new System.Drawing.Point(3, 83);
            this.splitPointLabel.Name = "splitPointLabel";
            this.splitPointLabel.Size = new System.Drawing.Size(126, 13);
            this.splitPointLabel.TabIndex = 0;
            this.splitPointLabel.Text = "Select the portion to split:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.existingContentLabel);
            this.panel2.Controls.Add(this.existingContentBrowser);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(243, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(227, 367);
            this.panel2.TabIndex = 3;
            // 
            // existingContentLabel
            // 
            this.existingContentLabel.AutoSize = true;
            this.existingContentLabel.Location = new System.Drawing.Point(4, 4);
            this.existingContentLabel.Name = "existingContentLabel";
            this.existingContentLabel.Size = new System.Drawing.Size(38, 13);
            this.existingContentLabel.TabIndex = 2;
            this.existingContentLabel.Text = "Part 1:";
            // 
            // existingContentBrowser
            // 
            this.existingContentBrowser.Location = new System.Drawing.Point(0, 21);
            this.existingContentBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.existingContentBrowser.Name = "existingContentBrowser";
            this.existingContentBrowser.Size = new System.Drawing.Size(227, 346);
            this.existingContentBrowser.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContentLabel);
            this.panel3.Controls.Add(this.splitContentBrowser);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(476, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 367);
            this.panel3.TabIndex = 4;
            // 
            // splitContentLabel
            // 
            this.splitContentLabel.AutoSize = true;
            this.splitContentLabel.Location = new System.Drawing.Point(3, 5);
            this.splitContentLabel.Name = "splitContentLabel";
            this.splitContentLabel.Size = new System.Drawing.Size(38, 13);
            this.splitContentLabel.TabIndex = 3;
            this.splitContentLabel.Text = "Part 2:";
            // 
            // splitContentBrowser
            // 
            this.splitContentBrowser.Location = new System.Drawing.Point(0, 21);
            this.splitContentBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.splitContentBrowser.Name = "splitContentBrowser";
            this.splitContentBrowser.Size = new System.Drawing.Size(228, 346);
            this.splitContentBrowser.TabIndex = 2;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(582, 391);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(70, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(658, 391);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(61, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // SplitPostDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(731, 426);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplitPostDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Split Post";
            this.Load += new System.EventHandler(this.updatePreview);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox splitPointText;
        private System.Windows.Forms.Label splitPointLabel;
        private System.Windows.Forms.WebBrowser existingContentBrowser;
        private System.Windows.Forms.WebBrowser splitContentBrowser;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox splitTitleText;
        private System.Windows.Forms.Label splitTitleLabel;
        private System.Windows.Forms.TextBox existingTitleText;
        private System.Windows.Forms.Label existingTitleLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label existingContentLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label splitContentLabel;
    }
}