// <copyright file="CharacterCopyExt.cs" company="Flying Top Hat">
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
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using KeePass.Plugins;
    using KeePassLib;
    using KeePassLib.Security;

    public sealed class CharacterCopyExt : Plugin
    {
        private IPluginHost host;
        private ToolStripMenuItem pluginMenuItem;
        private ToolStripSeparator pluginMenuItemSeparator;

        /// <summary>
        /// Image for the plugin menu's drop-down items
        /// </summary>
        private Image dropDownMenuItemIcon;

        public override bool Initialize(IPluginHost pluginHost)
        {
            bool initialised = false;
            if (pluginHost != null)
            {
                this.host = pluginHost;

                ContextMenuStrip contextMenu = this.host.MainWindow.EntryContextMenu;

                // Add plugin menu item
                this.pluginMenuItem = new ToolStripMenuItem(PluginStrings.MenuItemText);

                if (!this.InsertItemAboveSeparator(contextMenu.Items, this.pluginMenuItem))
                {
                    this.pluginMenuItemSeparator = new ToolStripSeparator();
                    contextMenu.Items.Add(this.pluginMenuItemSeparator);
                    contextMenu.Items.Add(this.pluginMenuItem);
                }

                // Add event handlers
                this.pluginMenuItem.DropDownItemClicked += this.OnEntryMenuItemClicked;
                contextMenu.Opening += this.OnEntryContextMenuOpening;
                contextMenu.Closing += this.OnEntryContextMenuClosing;

                // Get image for items in plugin menu's drop-down list
                this.dropDownMenuItemIcon = this.host.MainWindow.ClientIcons.Images[(int)PwIcon.Key];

                initialised = true;
            }

            return initialised;
        }

        private bool InsertItemAboveSeparator(ToolStripItemCollection menuItems, ToolStripMenuItem item)
        {
            bool inserted = false;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i] is ToolStripSeparator)
                {
                    menuItems.Insert(i, item);
                    inserted = true;
                    break;
                }
            }

            return inserted;
        }

        /// <summary>
        /// Enables/disables the plugin menu and populates its drop-down menu with
        /// the protected strings from the selected <see cref="KeePassLib.PwEntry"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEntryContextMenuOpening(object sender, EventArgs e)
        {
            uint totalSelectedEntries = this.host.MainWindow.GetSelectedEntriesCount();
            if (totalSelectedEntries == 1)
            {
                PwEntry entry = this.host.MainWindow.GetSelectedEntry(false);
                this.PopulateWithProtectedStringMenuItems(
                    entry,
                    this.pluginMenuItem.DropDown,
                    this.dropDownMenuItemIcon);

                this.pluginMenuItem.Enabled = true;
            }
            else
            {
                this.pluginMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Clears the plugin menu's drop-down menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEntryContextMenuClosing(object sender, EventArgs e)
        {
            this.pluginMenuItem.DropDownItems.Clear();
        }

        /// <summary>
        /// Displays the character selection form for the protected string referenced by the 
        /// clicked <see cref="ProtectedStringMenuItem" />.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEntryMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ProtectedStringMenuItem)
            {
                ProtectedStringMenuItem item = (ProtectedStringMenuItem)e.ClickedItem;

                PwEntry entry = this.host.Database.RootGroup.FindEntry(item.EntryUuid, true);
                if (entry != null)
                {
                    if (entry.Strings.Exists(item.ProtectedStringKey))
                    {
                        ProtectedString procString = entry.Strings.Get(item.ProtectedStringKey);

                        // Display character selector form for the protected string
                        using (CharacterSelectorForm selectorForm = new CharacterSelectorForm(this.host, procString))
                        {
                            selectorForm.ShowDialog(this.host.MainWindow);
                        }

                        entry.Touch(false);
                    }
                }
            }
        }

        /// <summary>
        /// Populates a drop-down menu with ProtectedStringMenuItem objects
        /// representing an entry's protected strings
        /// </summary>
        /// <param name="entry">Entry whos protected strings are listed</param>
        /// <param name="dropDown">Drop-down menu that's populated</param>
        /// <param name="itemsImage">Images to assign to all the menu-items</param>
        private void PopulateWithProtectedStringMenuItems(PwEntry entry, ToolStripDropDown dropDown, Image itemsImage = null)
        {
            foreach (KeyValuePair<string, ProtectedString> kvpA in entry.Strings)
            {
                if (kvpA.Value.IsProtected)
                {
                    ProtectedStringMenuItem item =
                        this.CreateProtectedStringMenuItem(
                            entry,
                            kvpA.Key,
                            itemsImage
                        );

                    dropDown.Items.Add(item);
                }
            }
        }

        private ProtectedStringMenuItem CreateProtectedStringMenuItem(PwEntry entry, string protectedStringKey, Image image = null)
        {
            ProtectedStringMenuItem menuItem = new ProtectedStringMenuItem(entry.Uuid, protectedStringKey);

            menuItem.Text = string.Format(
                PluginStrings.EntryMenuItem,
                protectedStringKey);

            if (image != null)
            {
                menuItem.Image = image;
            }

            return menuItem;
        }

        /// <summary>
        /// Removes menu items and cleans up after itself
        /// </summary>
        public override void Terminate()
        {
            ContextMenuStrip contextMenu = this.host.MainWindow.EntryContextMenu;
            contextMenu.Opening -= this.OnEntryContextMenuOpening;
            contextMenu.Closing -= this.OnEntryContextMenuClosing;

            if (this.pluginMenuItemSeparator != null)
            {
                contextMenu.Items.Remove(this.pluginMenuItemSeparator);
            }

            if (this.pluginMenuItem != null)
            {
                contextMenu.Items.Remove(this.pluginMenuItem);
                this.pluginMenuItem.DropDownItemClicked -= this.OnEntryMenuItemClicked;
                this.pluginMenuItem.DropDownItems.Clear();
            }

            if (this.dropDownMenuItemIcon != null)
            {
                this.dropDownMenuItemIcon.Dispose();
            }
        }
    }
}