namespace CharacterCopy
{
    partial class CharacterSelectorForm
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
            this.copyToClipboardButton = new System.Windows.Forms.Button();
            this.charToCopyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.positionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.charToCopyNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // copyToClipboardButton
            // 
            this.copyToClipboardButton.Location = new System.Drawing.Point(12, 47);
            this.copyToClipboardButton.Name = "copyToClipboardButton";
            this.copyToClipboardButton.Size = new System.Drawing.Size(149, 30);
            this.copyToClipboardButton.TabIndex = 1;
            this.copyToClipboardButton.Text = "Copy to clipboard";
            this.copyToClipboardButton.UseVisualStyleBackColor = true;
            this.copyToClipboardButton.Click += new System.EventHandler(this.copyToClipboardButton_Click);
            // 
            // charToCopyNumericUpDown
            // 
            this.charToCopyNumericUpDown.Location = new System.Drawing.Point(81, 10);
            this.charToCopyNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.charToCopyNumericUpDown.Name = "charToCopyNumericUpDown";
            this.charToCopyNumericUpDown.Size = new System.Drawing.Size(80, 25);
            this.charToCopyNumericUpDown.TabIndex = 0;
            this.charToCopyNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(9, 13);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(66, 17);
            this.positionLabel.TabIndex = 2;
            this.positionLabel.Text = "Position #";
            // 
            // CharacterSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(173, 87);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.charToCopyNumericUpDown);
            this.Controls.Add(this.copyToClipboardButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterSelectorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Character";
            ((System.ComponentModel.ISupportInitialize)(this.charToCopyNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button copyToClipboardButton;
        private System.Windows.Forms.NumericUpDown charToCopyNumericUpDown;
        private System.Windows.Forms.Label positionLabel;
    }
}