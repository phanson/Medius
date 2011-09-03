namespace Medius
{
    partial class RenameAuthorDialog
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
            this.oldNameLabel = new System.Windows.Forms.Label();
            this.oldNameComboBox = new System.Windows.Forms.ComboBox();
            this.newNameLabel = new System.Windows.Forms.Label();
            this.newNameText = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // oldNameLabel
            // 
            this.oldNameLabel.AutoSize = true;
            this.oldNameLabel.Location = new System.Drawing.Point(32, 15);
            this.oldNameLabel.Name = "oldNameLabel";
            this.oldNameLabel.Size = new System.Drawing.Size(41, 13);
            this.oldNameLabel.TabIndex = 0;
            this.oldNameLabel.Text = "Author:";
            // 
            // oldNameComboBox
            // 
            this.oldNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.oldNameComboBox.FormattingEnabled = true;
            this.oldNameComboBox.Location = new System.Drawing.Point(79, 12);
            this.oldNameComboBox.Name = "oldNameComboBox";
            this.oldNameComboBox.Size = new System.Drawing.Size(193, 21);
            this.oldNameComboBox.Sorted = true;
            this.oldNameComboBox.TabIndex = 1;
            // 
            // newNameLabel
            // 
            this.newNameLabel.AutoSize = true;
            this.newNameLabel.Location = new System.Drawing.Point(12, 42);
            this.newNameLabel.Name = "newNameLabel";
            this.newNameLabel.Size = new System.Drawing.Size(61, 13);
            this.newNameLabel.TabIndex = 2;
            this.newNameLabel.Text = "New name:";
            // 
            // newNameText
            // 
            this.newNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newNameText.Location = new System.Drawing.Point(79, 39);
            this.newNameText.Name = "newNameText";
            this.newNameText.Size = new System.Drawing.Size(193, 20);
            this.newNameText.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(135, 71);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(70, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(211, 71);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(61, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // RenameAuthorDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 106);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.newNameText);
            this.Controls.Add(this.newNameLabel);
            this.Controls.Add(this.oldNameComboBox);
            this.Controls.Add(this.oldNameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameAuthorDialog";
            this.ShowIcon = false;
            this.Text = "RenameAuthorDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label oldNameLabel;
        private System.Windows.Forms.ComboBox oldNameComboBox;
        private System.Windows.Forms.Label newNameLabel;
        private System.Windows.Forms.TextBox newNameText;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}