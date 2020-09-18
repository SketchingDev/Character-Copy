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

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using KeePass;
using KeePassLib;

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
        private readonly Regex COMMA_SEPARATED_NUMBERS_REGEXP = new Regex("^[0-9][0-9]*(,[0-9][0-9]*)*$");
        private readonly IPluginHost host;
        private readonly ProtectedString protectedString;

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
                this.copyMultipleCharacters.Enabled = true;
            }
            else
            {
                this.copyMultipleCharacters.Enabled = false;
            }
        }

        private char CopyProtectedCharacter(int position)
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

        private bool CopyToClipboard(String characters)
        {
            return ClipboardUtil.CopyAndMinimize(
                characters,
                false,
                this,
                null,
                null);
        }

        private void CopyMultipleCharacters_Click(object sender, EventArgs e)
        {
            String multipleCharacters = multipleCharactersText.Text;
            
            if (!COMMA_SEPARATED_NUMBERS_REGEXP.IsMatch(multipleCharacters))
            {
                MessageBox.Show(PluginStrings.CharactersPositionsInvalidFormat);
                return;
            }
            List<int> charPositionsToCopy = SplitCharactersByCommaAndSort(multipleCharacters);
            int lastCharPositionToCopy = charPositionsToCopy.Last();
            
            if (protectedString.Length < lastCharPositionToCopy)
            {
                MessageBox.Show(String.Format(PluginStrings.CharacterPositionExceedesBoundaries, lastCharPositionToCopy, protectedString.Length));
                return;
            }
            
            CopyChosenCharactersToClipboard(charPositionsToCopy);
        }

        private static List<int> SplitCharactersByCommaAndSort(string multipleCharacters)
        {
            List<int> charPositionsToCopy = new List<int>();
            foreach (var charToCopy in multipleCharacters.Split(','))
            {
                charPositionsToCopy.Add(int.Parse(charToCopy));
            }

            charPositionsToCopy.Sort();
            return charPositionsToCopy;
        }

        private void CopyChosenCharactersToClipboard(List<int> charPositionsToCopy)
        {
            StringBuilder chosenCharactersFromPassword = new StringBuilder();
            foreach (var charPosition in charPositionsToCopy)
            {
                chosenCharactersFromPassword.Append(CopyProtectedCharacter(charPosition));
            }
            if (CopyToClipboard(chosenCharactersFromPassword.ToString()))
            {
                host.MainWindow.StartClipboardCountdown();
            }
        }

        private void AutoType_Click(object sender, EventArgs e)
        {
            performAutoType(ClipboardUtil.GetText());
        }
        
        private void performAutoType(String chosenCharactersFromPassword)
        {
            //Needs for printing special chars
            String passwordElement = chosenCharactersFromPassword.Replace("~", "{~}")
                .Replace("+", "{+}")
                .Replace("^", "{^}")
                .Replace("%", "{%}");
            
            PwEntry pe = new PwEntry(false, false);
            pe.AutoType.Enabled = true;
            Thread.Sleep(3000);
            AutoType.PerformIntoCurrentWindow(pe, Program.MainForm.DocumentManager.FindContainerOf(pe), passwordElement);
        }
    }
}
