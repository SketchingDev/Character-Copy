// <copyright file="CharacterSelectorForm.cs" company="Flying Top Hat">
//    Character Copy - A KeePass Plugin for copying protected characters
//    Copyright (C) 2012 Lucas <lucas@flyingtophat.co.uk>
//
//    This program is free software; you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation; either version 2 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
// </copyright>
namespace CharacterCopy
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using KeePass.Plugins;
    using KeePass.Util;
    using KeePassLib.Security;
    using KeePassLib.Utility;

    public partial class CharacterSelectorForm : Form
    {
        private IPluginHost host;
        private ProtectedString protectedString;

        private CharacterSelectorForm()
        {
            InitializeComponent();
        }

        public CharacterSelectorForm(IPluginHost pluginHost, ProtectedString procString)
            : this()
        {
            if (pluginHost == null)
            {
                throw new ArgumentNullException("pluginHost");
            }

            if (procString == null)
            {
                throw new ArgumentNullException("procString");
            }

            this.host = pluginHost;
            this.protectedString = procString;
            
            if (this.protectedString.Length > 0)
            {
                // Limit range to the length of the protected string
                this.SetNumericUpDownRange(1, this.protectedString.Length);
                this.copyToClipboardButton.Enabled = true;
            }
            else
            {
                // Disable control
                this.SetNumericUpDownRange(0, 0);
                this.copyToClipboardButton.Enabled = false;
            }
        }

        public void SetNumericUpDownRange(decimal min, decimal max)
        {
            this.charToCopyNumericUpDown.Minimum = min;
            this.charToCopyNumericUpDown.Maximum = max;
        }

        /// <summary>
        /// Copies the character at the chosen position from the protected string 
        /// to the user's clipboard then starts the clipboard countdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToClipboardButton_Click(object sender, System.EventArgs e)
        {
            int characterPosition = decimal.ToInt32(this.charToCopyNumericUpDown.Value);

            // Check valid position then copy to clipboard
            if ((characterPosition >= 1) && (characterPosition <= this.protectedString.Length))
            {
                char character = this.CopyProtectedCharacter(
                        this.protectedString,
                        characterPosition);

                if (this.CopyCharToClipboard(character))
                {
                    this.host.MainWindow.StartClipboardCountdown();
                }
            }
        }

        private char CopyProtectedCharacter(ProtectedString protectedString, int position)
        {
            // Read bytes of protected string, then extract required character
            byte[] procStringUtf8 = this.protectedString.ReadUtf8();
            char[] characters = Encoding.UTF8.GetChars(
                procStringUtf8,
                (position - 1), // 0-based index
                1);

            // Clear extracted bytes
            MemUtil.ZeroByteArray(procStringUtf8);

            return characters[0];
        }

        private bool CopyCharToClipboard(char character)
        {
            return ClipboardUtil.CopyAndMinimize(
                    character.ToString(),
                    false,
                    this,
                    null,
                    null);
        }
    }
}
