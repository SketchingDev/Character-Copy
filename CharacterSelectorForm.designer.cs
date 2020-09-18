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
            this.copyMultipleCharacters = new System.Windows.Forms.Button();
            this.multipleCharactersText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AutoTypeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // copyMultipleCharacters
            // 
            this.copyMultipleCharacters.Location = new System.Drawing.Point(11, 95);
            this.copyMultipleCharacters.Name = "copyMultipleCharacters";
            this.copyMultipleCharacters.Size = new System.Drawing.Size(127, 30);
            this.copyMultipleCharacters.TabIndex = 3;
            this.copyMultipleCharacters.Text = "Copy to clipboard";
            this.copyMultipleCharacters.UseVisualStyleBackColor = true;
            this.copyMultipleCharacters.Click += new System.EventHandler(this.CopyMultipleCharacters_Click);
            // 
            // multipleCharactersText
            // 
            this.multipleCharactersText.Location = new System.Drawing.Point(11, 54);
            this.multipleCharactersText.Name = "multipleCharactersText";
            this.multipleCharactersText.Size = new System.Drawing.Size(267, 25);
            this.multipleCharactersText.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 34);
            this.label1.TabIndex = 5;
            this.label1.Text = "Positions of characters to copy \r\n(multiple positions separated by comma)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AutoTypeBtn
            // 
            this.AutoTypeBtn.Location = new System.Drawing.Point(154, 95);
            this.AutoTypeBtn.Name = "AutoTypeBtn";
            this.AutoTypeBtn.Size = new System.Drawing.Size(124, 30);
            this.AutoTypeBtn.TabIndex = 6;
            this.AutoTypeBtn.Text = "Perform auto type";
            this.AutoTypeBtn.UseVisualStyleBackColor = true;
            this.AutoTypeBtn.Click += new System.EventHandler(this.AutoType_Click);
            // 
            // CharacterSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(290, 127);
            this.Controls.Add(this.AutoTypeBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.multipleCharactersText);
            this.Controls.Add(this.copyMultipleCharacters);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterSelectorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button AutoTypeBtn;

        private System.Windows.Forms.TextBox multipleCharactersText;

        private System.Windows.Forms.Button copyMultipleCharacters;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}