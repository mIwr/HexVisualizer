namespace HexVisualizer
{
    partial class StringTypeChooser
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
            if(disposing && (components != null))
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
            this.uiRadio_Hex = new System.Windows.Forms.RadioButton();
            this.uiRadio_Base64 = new System.Windows.Forms.RadioButton();
            this.uiRadio_Encoding = new System.Windows.Forms.RadioButton();
            this.uiEncoding = new System.Windows.Forms.ComboBox();
            this.uiCancelButton = new System.Windows.Forms.Button();
            this.uiOkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiRadio_Hex
            // 
            this.uiRadio_Hex.AutoSize = true;
            this.uiRadio_Hex.Location = new System.Drawing.Point(12, 12);
            this.uiRadio_Hex.Name = "uiRadio_Hex";
            this.uiRadio_Hex.Size = new System.Drawing.Size(72, 17);
            this.uiRadio_Hex.TabIndex = 0;
            this.uiRadio_Hex.TabStop = true;
            this.uiRadio_Hex.Text = "Hex string";
            this.uiRadio_Hex.UseVisualStyleBackColor = true;
            this.uiRadio_Hex.CheckedChanged += new System.EventHandler(this.uiRadio_CheckedChanged);
            // 
            // uiRadio_Base64
            // 
            this.uiRadio_Base64.AutoSize = true;
            this.uiRadio_Base64.Location = new System.Drawing.Point(12, 53);
            this.uiRadio_Base64.Name = "uiRadio_Base64";
            this.uiRadio_Base64.Size = new System.Drawing.Size(91, 17);
            this.uiRadio_Base64.TabIndex = 1;
            this.uiRadio_Base64.TabStop = true;
            this.uiRadio_Base64.Text = "Base64 String";
            this.uiRadio_Base64.UseVisualStyleBackColor = true;
            this.uiRadio_Base64.CheckedChanged += new System.EventHandler(this.uiRadio_CheckedChanged);
            // 
            // uiRadio_Encoding
            // 
            this.uiRadio_Encoding.AutoSize = true;
            this.uiRadio_Encoding.Location = new System.Drawing.Point(12, 92);
            this.uiRadio_Encoding.Name = "uiRadio_Encoding";
            this.uiRadio_Encoding.Size = new System.Drawing.Size(98, 17);
            this.uiRadio_Encoding.TabIndex = 2;
            this.uiRadio_Encoding.TabStop = true;
            this.uiRadio_Encoding.Text = "Encoded String";
            this.uiRadio_Encoding.UseVisualStyleBackColor = true;
            this.uiRadio_Encoding.CheckedChanged += new System.EventHandler(this.uiRadio_CheckedChanged);
            // 
            // uiEncoding
            // 
            this.uiEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiEncoding.FormattingEnabled = true;
            this.uiEncoding.Location = new System.Drawing.Point(31, 115);
            this.uiEncoding.Name = "uiEncoding";
            this.uiEncoding.Size = new System.Drawing.Size(241, 21);
            this.uiEncoding.TabIndex = 3;
            // 
            // uiCancelButton
            // 
            this.uiCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancelButton.Location = new System.Drawing.Point(116, 173);
            this.uiCancelButton.Name = "uiCancelButton";
            this.uiCancelButton.Size = new System.Drawing.Size(75, 23);
            this.uiCancelButton.TabIndex = 4;
            this.uiCancelButton.Text = "C&ancel";
            this.uiCancelButton.UseVisualStyleBackColor = true;
            // 
            // uiOkButton
            // 
            this.uiOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uiOkButton.Enabled = false;
            this.uiOkButton.Location = new System.Drawing.Point(197, 173);
            this.uiOkButton.Name = "uiOkButton";
            this.uiOkButton.Size = new System.Drawing.Size(75, 23);
            this.uiOkButton.TabIndex = 5;
            this.uiOkButton.Text = "&Ok";
            this.uiOkButton.UseVisualStyleBackColor = true;
            // 
            // StringTypeChooser
            // 
            this.AcceptButton = this.uiOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancelButton;
            this.ClientSize = new System.Drawing.Size(284, 209);
            this.ControlBox = false;
            this.Controls.Add(this.uiOkButton);
            this.Controls.Add(this.uiCancelButton);
            this.Controls.Add(this.uiEncoding);
            this.Controls.Add(this.uiRadio_Encoding);
            this.Controls.Add(this.uiRadio_Base64);
            this.Controls.Add(this.uiRadio_Hex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StringTypeChooser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StringTypeChooser";
            this.Load += new System.EventHandler(this.StringTypeChooser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton uiRadio_Hex;
        private System.Windows.Forms.RadioButton uiRadio_Base64;
        private System.Windows.Forms.RadioButton uiRadio_Encoding;
        private System.Windows.Forms.ComboBox uiEncoding;
        private System.Windows.Forms.Button uiCancelButton;
        private System.Windows.Forms.Button uiOkButton;
    }
}