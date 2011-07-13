namespace Medius
{
    partial class ValidateHtmlDialog
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
            this.validationLabel = new System.Windows.Forms.Label();
            this.postList = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // validationLabel
            // 
            this.validationLabel.AutoSize = true;
            this.validationLabel.Location = new System.Drawing.Point(12, 9);
            this.validationLabel.Name = "validationLabel";
            this.validationLabel.Size = new System.Drawing.Size(214, 13);
            this.validationLabel.TabIndex = 0;
            this.validationLabel.Text = "The following posts do not validate properly:";
            // 
            // postList
            // 
            this.postList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.postList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.postList.FormattingEnabled = true;
            this.postList.Location = new System.Drawing.Point(12, 31);
            this.postList.Name = "postList";
            this.postList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.postList.Size = new System.Drawing.Size(260, 184);
            this.postList.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(202, 223);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(70, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // ValidateHtmlDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 258);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.postList);
            this.Controls.Add(this.validationLabel);
            this.Name = "ValidateHtmlDialog";
            this.Text = "Validate HTML";
            this.Load += new System.EventHandler(this.ValidateHtmlDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label validationLabel;
        private System.Windows.Forms.ListBox postList;
        private System.Windows.Forms.Button okButton;
    }
}