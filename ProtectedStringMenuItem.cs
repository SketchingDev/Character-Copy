// <copyright file="ProtectedStringMenuItem.cs" company="Flying Top Hat">
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
    using System.Windows.Forms;
    using KeePassLib;

    /// <summary>
    /// Menu item that references an protected string stored in a <see cref="KeePassLib.PwEntry" />.
    /// </summary>
    public class ProtectedStringMenuItem : ToolStripMenuItem
    {
        public PwUuid EntryUuid { get; private set; }

        public string ProtectedStringKey { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ProtectedStringMenuItem" /> class
        /// </summary>
        /// <param name="entryUuid">UUID for the entry containing the protected string</param>
        /// <param name="protectedStringKey">Key for the protected string</param>
        public ProtectedStringMenuItem(PwUuid entryUuid, string protectedStringKey)
        {
            if (entryUuid == null)
            {
                throw new ArgumentNullException("entryUuid");
            }

            if (protectedStringKey == null)
            {
                throw new ArgumentNullException("protectedStringKey");
            }

            this.EntryUuid = entryUuid;
            this.ProtectedStringKey = protectedStringKey;
        }
    }
}
