﻿//  Excel__Module__Manager.cs
//
//  Author:
//       Victor L. Senior (VLS) <betselection(&)gmail.com>
//
//  Web: 
//       http://betselection.cc/betsoftware/
//
//  Sources:
//       http://github.com/betselection/
//
//  Copyright (c) 2014 Victor L. Senior
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Runtime.CompilerServices;
using System.Net;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Excel__Module__Manager
{
    // Directives
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Microsoft.CSharp;

    /// <summary>
    /// Excel Module Manager class.
    /// </summary>
    public partial class Excel__Module__Manager : Form
    {
        /// <summary>
        /// The load excel file button.
        /// </summary>
        private Button loadExcelFileButton;

        /// <summary>
        /// The sheet format combo box.
        /// </summary>
        private ComboBox sheetFormatComboBox;

        /// <summary>
        /// The set sheet format label.
        /// </summary>
        private Label setSheetFormatLabel;

        /// <summary>
        /// The set excel sheet label.
        /// </summary>
        private Label setExcelSheetLabel;

        /// <summary>
        /// The set module type label.
        /// </summary>
        private Label setModuleTypeLabel;

        /// <summary>
        /// The module type list box.
        /// </summary>
        private ListBox moduleTypeListBox;

        /// <summary>
        /// The generate module label.
        /// </summary>
        private Label generateModuleLabel;

        /// <summary>
        /// The compile module button.
        /// </summary>
        private Button compileModuleButton;

        /// <summary>
        /// The remove excel module label.
        /// </summary>
        private Label removeExcelModuleLabel;

        /// <summary>
        /// The delete button.
        /// </summary>
        private Button deleteButton;

        /// <summary>
        /// The remove module type list box.
        /// </summary>
        private ListBox removeModuleTypeListBox;

        /// <summary>
        /// The remove module list box.
        /// </summary>
        private ListBox removeModulesListBox;

        /// <summary>
        /// The excel file.
        /// </summary>
        private string excelFile = string.Empty;

        /// <summary>
        /// The type of the module.
        /// </summary>
        private string moduleType = string.Empty;

        /// <summary>
        /// The main open file dialog.
        /// </summary>
        private OpenFileDialog mainOpenFileDialog = new OpenFileDialog();

        /// <summary>
        /// The marshal object.
        /// </summary>
        private object marshal = null;

        /// <summary>
        /// The marshal paths.
        /// </summary>
        private Dictionary<string, string> marshalPaths = new Dictionary<string, string>();

        /// <summary>
        /// The remove module type list box dictionary.
        /// </summary>
        private Dictionary<string, List<string>> removeModuleTypeListBoxDictionary = new Dictionary<string, List<string>>();

        /// <summary>
        /// The module path without extension.
        /// </summary>
        private Dictionary<string, string> excelSheetsPath = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Excel__Module__Manager.Excel__Module__Manager"/> class.
        /// </summary>
        public Excel__Module__Manager()
        {
            this.loadExcelFileButton = new Button();
            this.setExcelSheetLabel = new Label();
            this.setModuleTypeLabel = new Label();
            this.moduleTypeListBox = new ListBox();
            this.generateModuleLabel = new Label();
            this.compileModuleButton = new Button();
            this.setSheetFormatLabel = new Label();
            this.sheetFormatComboBox = new ComboBox();
            this.removeExcelModuleLabel = new Label();
            this.deleteButton = new Button();
            this.removeModuleTypeListBox = new ListBox();
            this.removeModulesListBox = new ListBox();
            this.SuspendLayout();

            // loadExcelFileButton
            this.loadExcelFileButton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.loadExcelFileButton.Location = new Point(12, 30);
            this.loadExcelFileButton.Name = "loadExcelFileButton";
            this.loadExcelFileButton.Size = new Size(177, 41);
            this.loadExcelFileButton.TabIndex = 0;
            this.loadExcelFileButton.Text = "Load Excel File";
            this.loadExcelFileButton.UseVisualStyleBackColor = true;
            this.loadExcelFileButton.Click += new System.EventHandler(this.LoadExcelFileButtonClick);

            // setExcelSheetLabel
            this.setExcelSheetLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.setExcelSheetLabel.Location = new Point(12, 9);
            this.setExcelSheetLabel.Name = "setExcelSheetLabel";
            this.setExcelSheetLabel.Size = new Size(177, 23);
            this.setExcelSheetLabel.TabIndex = 1;
            this.setExcelSheetLabel.Text = "1) Set Excel Sheet:";

            // setModuleTypeLabel
            this.setModuleTypeLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.setModuleTypeLabel.Location = new Point(12, 136);
            this.setModuleTypeLabel.Name = "setModuleTypeLabel";
            this.setModuleTypeLabel.Size = new Size(177, 23);
            this.setModuleTypeLabel.TabIndex = 1;
            this.setModuleTypeLabel.Text = "3) Set module type:";

            // moduleTypeListBox
            this.moduleTypeListBox.FormattingEnabled = true;
            this.moduleTypeListBox.Items.AddRange(new object[]
                {
                    "Input",
                    "Bet Selection",
                    "Money Management",
                    "Display",
                    "Output"
                });
            this.moduleTypeListBox.Location = new Point(12, 159);
            this.moduleTypeListBox.Name = "moduleTypeListBox";
            this.moduleTypeListBox.Size = new Size(177, 69);
            this.moduleTypeListBox.TabIndex = 2;
            this.moduleTypeListBox.SelectedIndexChanged += new System.EventHandler(this.ModuleTypeListBoxSelectedIndexChanged);

            // generateModuleLabel
            this.generateModuleLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.generateModuleLabel.Location = new Point(12, 241);
            this.generateModuleLabel.Name = "generateModuleLabel";
            this.generateModuleLabel.Size = new Size(177, 23);
            this.generateModuleLabel.TabIndex = 1;
            this.generateModuleLabel.Text = "4) Generate Module";

            // compileModuleButton
            this.compileModuleButton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.compileModuleButton.Location = new Point(12, 265);
            this.compileModuleButton.Name = "compileModuleButton";
            this.compileModuleButton.Size = new Size(177, 41);
            this.compileModuleButton.TabIndex = 3;
            this.compileModuleButton.Text = "Compile Module";
            this.compileModuleButton.UseVisualStyleBackColor = true;
            this.compileModuleButton.Click += new System.EventHandler(this.CompileModuleButtonClick);

            // setSheetFormatLabel
            this.setSheetFormatLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.setSheetFormatLabel.Location = new Point(12, 79);
            this.setSheetFormatLabel.Name = "setSheetFormatLabel";
            this.setSheetFormatLabel.Size = new Size(177, 23);
            this.setSheetFormatLabel.TabIndex = 1;
            this.setSheetFormatLabel.Text = "2) Set sheet format:";

            // sheetFormatComboBox
            this.sheetFormatComboBox.FormattingEnabled = true;
            this.sheetFormatComboBox.Items.AddRange(new object[]
                {
                    "ExcelBot"
                });
            this.sheetFormatComboBox.Location = new Point(13, 103);
            this.sheetFormatComboBox.Name = "sheetFormatComboBox";
            this.sheetFormatComboBox.Size = new Size(176, 21);
            this.sheetFormatComboBox.TabIndex = 1;
            this.sheetFormatComboBox.Text = "ExcelBot";
            this.sheetFormatComboBox.SelectedIndexChanged += new System.EventHandler(this.SheetFormatComboBoxSelectedIndexChanged);

            // removeExcelModuleLabel
            this.removeExcelModuleLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.removeExcelModuleLabel.Location = new Point(217, 9);
            this.removeExcelModuleLabel.Name = "removeExcelModuleLabel";
            this.removeExcelModuleLabel.Size = new Size(177, 23);
            this.removeExcelModuleLabel.TabIndex = 5;
            this.removeExcelModuleLabel.Text = "Remove excel module:";

            // deleteButton
            this.deleteButton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.deleteButton.Location = new Point(217, 264);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new Size(177, 41);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = "Delete Module";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButtonClick);

            // removeModuleTypeListBox
            this.removeModuleTypeListBox.FormattingEnabled = true;
            this.removeModuleTypeListBox.Items.AddRange(new object[]
                {
                    "Input",
                    "Bet Selection",
                    "Money Management",
                    "Display",
                    "Output"
                });
            this.removeModuleTypeListBox.Location = new Point(217, 29);
            this.removeModuleTypeListBox.Name = "removeModuleTypeListBox";
            this.removeModuleTypeListBox.Size = new Size(177, 69);
            this.removeModuleTypeListBox.TabIndex = 10;
            this.removeModuleTypeListBox.SelectedIndexChanged += new System.EventHandler(this.RemoveModuleTypeListBoxSelectedIndexChanged);

            // removeModulesListBox
            this.removeModulesListBox.FormattingEnabled = true;
            this.removeModulesListBox.Location = new Point(217, 120);
            this.removeModulesListBox.Name = "removeModulesListBox";
            this.removeModulesListBox.Size = new Size(177, 134);
            this.removeModulesListBox.TabIndex = 10;
            this.removeModulesListBox.SelectionMode = SelectionMode.MultiExtended;

            // MainForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(406, 313);
            this.Controls.Add(this.sheetFormatComboBox);
            this.Controls.Add(this.removeModuleTypeListBox);
            this.Controls.Add(this.removeModulesListBox);
            this.Controls.Add(this.moduleTypeListBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.compileModuleButton);
            this.Controls.Add(this.loadExcelFileButton);
            this.Controls.Add(this.generateModuleLabel);
            this.Controls.Add(this.setSheetFormatLabel);
            this.Controls.Add(this.setModuleTypeLabel);
            this.Controls.Add(this.removeExcelModuleLabel);
            this.Controls.Add(this.setExcelSheetLabel);
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Excel Module Manager";
            this.ResumeLayout(false);

            // mainOpenFileDialog
            this.mainOpenFileDialog.InitialDirectory = Application.StartupPath;
            this.mainOpenFileDialog.Filter = "Excel files (*.xl*)|*.xl*|All files (*.*)|*.*";
            this.mainOpenFileDialog.RestoreDirectory = true;

            // Add to removeModuleTypeListBox dictionary
            foreach (object item in moduleTypeListBox.Items)
            {
                // Set itemBase
                string itemBase = item.ToString().Replace(" ", string.Empty);

                // Add to dictionary
                removeModuleTypeListBoxDictionary.Add(itemBase, new List<string>());
            }
        }

        /// <summary>
        /// Inits the instance.
        /// </summary>
        /// <param name="passedMarshal">Passed marshal.</param>
        public void Init(object passedMarshal)
        {
            // Set marshal
            this.marshal = passedMarshal;

            // Set icon
            this.Icon = (Icon)this.marshal.GetType().GetProperty("Icon").GetValue(this.marshal, null);

            // Fetch paths from marshal
            this.marshalPaths = (Dictionary<string, string>)this.marshal.GetType().GetProperty("Paths").GetValue(this.marshal, null);

            // Show form
            this.Show();

            // Update module type list
            UpdateModuleTypeListBoxCount();
        }

        /// <summary>
        /// Loads the excel file button click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void LoadExcelFileButtonClick(object sender, EventArgs e)
        {
            // Open file dialog
            if (this.mainOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Set file
                this.excelFile = this.mainOpenFileDialog.FileName;
            }
        }

        /// <summary>
        /// Sheets the format combo box selected index changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void SheetFormatComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO Add more formats (kattila, etc.)
        }

        /// <summary>
        /// Modules the type list box selected index changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void ModuleTypeListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Set module type
            this.moduleType = this.moduleTypeListBox.SelectedItem.ToString();
        }

        /// <summary>
        /// Compiles the module button click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void CompileModuleButtonClick(object sender, EventArgs e)
        {
            // Check there's an excel file
            if (this.excelFile.Length == 0)
            {
                // Advice user
                MessageBox.Show("Please set an excel file.", "Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Halt flow
                return;
            }

            // Check there's a valid module type
            if (this.moduleTypeListBox.SelectedIndex == -1)
            {
                // Advice user
                MessageBox.Show("Please set module type.", "Module type", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Halt flow
                return;
            }

            // Disable compile module button
            compileModuleButton.Enabled = false;

            // Module namespace
            string moduleNamespace = this.DisplayNameToNameSpace(Path.GetFileNameWithoutExtension(this.excelFile));

            // Replace <module_name> with actual name
            this.connectorSourceCode[0] = this.connectorSourceCode[0].Replace("<module_name>", moduleNamespace);

            // Replace <module_extension> with actual excel file extension
            this.connectorSourceCode[0] = this.connectorSourceCode[0].Replace("<module_extension>", Path.GetExtension(this.excelFile));

            // Set source code file name
            string sourceCodeFileName = Path.Combine(this.marshalPaths["Framework"], moduleNamespace + ".cs");

            // Save to disk
            File.WriteAllText(sourceCodeFileName, this.connectorSourceCode[0]);

            // Output assembly file path
            string assemblyFilePath = Path.Combine(Path.Combine(Path.Combine(this.marshalPaths["Framework"], this.moduleTypeListBox.SelectedItem.ToString().Replace(" ", string.Empty)), (string)this.marshal.GetType().GetProperty("Game").GetValue(this.marshal, null)), moduleNamespace + ".dll");

            // Remove existing assembly file if needed
            if (File.Exists(assemblyFilePath))
            {
                // Remove
                File.Delete(assemblyFilePath);
            }

            /* Module compilation code */

            // Code provider
            CSharpCodeProvider cscp = new CSharpCodeProvider();

            // Parameters
            CompilerParameters cp = new CompilerParameters();

            // System reference
            cp.ReferencedAssemblies.Add("System.dll");

            // System.Windows.Forms reference
            cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            // System.Drawing reference
            cp.ReferencedAssemblies.Add("System.Drawing.dll");

            // ExcelApi reference
            cp.ReferencedAssemblies.Add(Path.Combine(marshalPaths["Framework"], "ExcelApi.dll"));

            // OfficeApi reference
            cp.ReferencedAssemblies.Add(Path.Combine(marshalPaths["Framework"], "OfficeApi.dll"));

            // VBIDEApi reference
            cp.ReferencedAssemblies.Add(Path.Combine(marshalPaths["Framework"], "VBIDEApi.dll"));

            // NetOffice reference
            cp.ReferencedAssemblies.Add(Path.Combine(marshalPaths["Framework"], "NetOffice.dll"));

            // Generate .dll module
            cp.GenerateExecutable = false;

            // Module file path
            cp.OutputAssembly = assemblyFilePath;

            // Generate as file
            cp.GenerateInMemory = false;

            // No debug info
            cp.IncludeDebugInformation = false;

            // No errors for warns 
            cp.TreatWarningsAsErrors = false;

            // Compile module.
            CompilerResults cr = cscp.CompileAssemblyFromFile(cp, sourceCodeFileName);

            // Check for errors
            if (cr.Errors.Count > 0)
            {
                // Error string
                string errorString = string.Empty;

                // Iterate errors
                foreach (CompilerError ce in cr.Errors)
                {
                    // Add to error string
                    errorString += ce.ToString();
                }

                // Advise user
                MessageBox.Show("Errors in Module Compilation:" + cr.Errors.Count + Environment.NewLine + "Debug information:" + Environment.MachineName + errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Remove file
                File.Delete(sourceCodeFileName);

                // Enable compile module button
                compileModuleButton.Enabled = true;

                // Halt flow
                return;
            }

            /* Sucessful compilation */

            // Copy renamed excel file to proper directory
            File.Copy(this.excelFile, Path.Combine(Path.Combine(Path.Combine(marshalPaths["Framework"], this.moduleTypeListBox.SelectedItem.ToString().Replace(" ", string.Empty)), (string)this.marshal.GetType().GetProperty("Game").GetValue(this.marshal, null)), moduleNamespace + Path.GetExtension(this.excelFile)), true);

            // Reload modules in framework
            this.marshal.GetType().GetMethod("ReloadModules").Invoke(this.marshal, null);

            // Select tab
            this.marshal.GetType().GetMethod("SelectTab").Invoke(this.marshal, new object[] { this.moduleTypeListBox.SelectedItem.ToString().Replace(" ", string.Empty) });

            // Update module type list box count
            UpdateModuleTypeListBoxCount();

            // Advise user about successful compilation
            MessageBox.Show("Successful Module Compilation", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Remove file
            File.Delete(sourceCodeFileName);

            // Enable compile module button
            compileModuleButton.Enabled = true;
        }

        /// <summary>
        /// Removes the module list box selected index changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void RemoveModuleTypeListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Check there's an index
            if (removeModuleTypeListBox.SelectedIndex == -1)
            {
                // Halt flow
                return;
            }

            // Clear removeModulesListBox
            removeModulesListBox.Items.Clear();

            // Add according to currently selected item
            foreach (object module in removeModuleTypeListBoxDictionary[moduleTypeListBox.Items[removeModuleTypeListBox.SelectedIndex].ToString().Replace(" ", string.Empty)])
            {
                // Add to remove modules listbox
                removeModulesListBox.Items.Add(module);
            }
        }

        /// <summary>
        /// Deletes the button click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            // Check there are files selected
            if (removeModuleTypeListBox.SelectedItems.Count < 1 || removeModulesListBox.SelectedItems.Count < 1)
            {
                // Halt flow
                return;
            }

            try
            {
                // Ask user (as a policy for file removal)
                if (MessageBox.Show("OK to remove files from disk permanently?", "File removal", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    // Set error flag
                    bool fileRemovalError = false;

                    // Remove all selected modules
                    foreach (object item in removeModulesListBox.SelectedItems)
                    {
                        // Get excel sheet path
                        string excelSheetPath = this.excelSheetsPath[moduleTypeListBox.Items[removeModuleTypeListBox.SelectedIndex].ToString().Replace(" ", string.Empty) + "." + DisplayNameToNameSpace(item.ToString())];

                        try
                        {
                            // Remove .dll       
                            File.Delete(Path.Combine(Path.GetDirectoryName(excelSheetPath), Path.GetFileNameWithoutExtension(excelSheetPath) + ".dll"));

                            // Remove excel sheet
                            File.Delete(excelSheetPath);
                        }
                        catch (IOException ex)
                        {
                            // Set flag to true
                            fileRemovalError = true;

                            // TODO Add to a list to show better error message: show exact file(s) with error
                        }
                    }

                    // Update module type list box count
                    UpdateModuleTypeListBoxCount();

                    // Reload modules in framework
                    this.marshal.GetType().GetMethod("ReloadModules").Invoke(this.marshal, null);

                    // Select tab
                    this.marshal.GetType().GetMethod("SelectTab").Invoke(this.marshal, new object[] { this.moduleTypeListBox.Items[this.removeModuleTypeListBox.SelectedIndex].ToString().Replace(" ", string.Empty) });

                    // Check for errors
                    if (fileRemovalError)
                    {
                        // Advise user
                        MessageBox.Show("There was an error when deleting files." + Environment.NewLine + "Please make sure no module is open at the moment of deleting it.", "File removal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show debug info
                MessageBox.Show("There was an error when deleting files:" + Environment.NewLine + ex.Message, "File removal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        //            / <summary>
        /// Updates the module type list box count.
        /// </summary>
        private void UpdateModuleTypeListBoxCount()
        {
            // Save remove module type listbox index
            int removeModuleTypeListBoxSelectedIndex = removeModuleTypeListBox.SelectedIndex;

            // Clear remove module type listbox dictionary
            foreach (string key in removeModuleTypeListBoxDictionary.Keys)
            {
                // Clear list
                removeModuleTypeListBoxDictionary[key].Clear();
            }

            // Clear excel sheets path dictionary
            excelSheetsPath.Clear();

            // Count dictionary for dirs
            Dictionary<string, int> count = new Dictionary<string, int>();

            // Populate count keys
            foreach (object item in moduleTypeListBox.Items)
            {
                // Set itemBase
                string itemBase = item.ToString().Replace(" ", string.Empty);
                
                // Add to count dictionary
                count.Add(itemBase, 0);
            }

            // Find excel files in all directories below framework
            foreach (string excelSheet in Directory.GetFiles(this.marshalPaths["Framework"], "*.xl*", SearchOption.AllDirectories))
            {
                // If it has matching .dll
                if (File.Exists(Path.Combine(Path.GetDirectoryName(excelSheet), Path.GetFileNameWithoutExtension(excelSheet) + ".dll")))
                {
                    // Check against marshalPaths
                    foreach (string dirName in this.marshalPaths.Keys)
                    {
                        // First check key is present in count dictionary
                        if (count.ContainsKey(dirName))
                        {
                            // If contains current dirName
                            if (Path.GetDirectoryName(excelSheet).Contains(dirName))
                            {
                                // Add to remove module type dictionary
                                removeModuleTypeListBoxDictionary[dirName].Add(NameSpaceToDisplayName(Path.GetFileNameWithoutExtension(excelSheet)));

                                // Add to module paths dictionary
                                excelSheetsPath.Add(dirName + "." + Path.GetFileNameWithoutExtension(excelSheet), excelSheet);

                                // Rise count
                                count[dirName]++;
                            }
                        }
                    }
                }
            }

            // Update moduleTypeListBox
            for (int i = 0; i < removeModuleTypeListBox.Items.Count; i++)
            {
                // Set itemBase
                string itemBase = moduleTypeListBox.Items[i].ToString().Replace(" ", string.Empty);

                // Check it's in dictionary
                if (count.ContainsKey(itemBase))
                {
                    // Update item on removeModuleTypeListBox
                    removeModuleTypeListBox.Items[i] = moduleTypeListBox.Items[i].ToString() + " (" + count[itemBase].ToString() + ")";
                }
            }

            // Clear index
            removeModuleTypeListBox.SelectedIndex = -1;

            // Set index
            removeModuleTypeListBox.SelectedIndex = removeModuleTypeListBoxSelectedIndex;
        }

        /// <summary>
        /// Changes passed display name to namespace.
        /// </summary>
        /// <returns>Resulting name space.</returns>
        /// <param name="displayName">Display name.</param>
        private string DisplayNameToNameSpace(string displayName)
        {
            // Check strings are there
            if (displayName.Length > 0)
            {
                // Match with regular expression
                MatchCollection matches = Regex.Matches(displayName, @"[^a-zA-Z0-9_]");

                // Walk reversed
                for (int i = matches.Count - 1; i >= 0; i--)
                {
                    // Handle space
                    if (matches[i].Value == " ")
                    {
                        // Remove original
                        displayName = displayName.Remove(matches[i].Index, 1);

                        // Insert replacement
                        displayName = displayName.Insert(matches[i].Index, "__");

                        // Next iteration
                        continue;
                    }

                    // Set encoding
                    UTF32Encoding encoding = new UTF32Encoding(); 

                    // Get current bytes
                    byte[] bytes = encoding.GetBytes(matches[i].Value.ToCharArray()); 

                    // Remove original
                    displayName = displayName.Remove(matches[i].Index, 1);

                    // Insert replacement
                    displayName = displayName.Insert(matches[i].Index, "_" + BitConverter.ToInt32(bytes, 0).ToString() + "_");
                }

                // Check for initial number
                if (Regex.Matches(displayName.Substring(0, 1), @"[0-9]").Count > 0)
                {
                    // Prepend string
                    displayName = "_0_" + displayName;
                }

                // Return processed display name
                return displayName;
            }

            // Return empty string by default
            return string.Empty;
        }

        /// <summary>
        /// Changes namespace to display name by naming convention
        /// </summary>
        /// <param name="nameSpace">string Passed namespace</param>
        /// <returns>String with replacements</returns>
        private string NameSpaceToDisplayName(string nameSpace)
        {
            // Remove initial _0_
            if (nameSpace.StartsWith("_0_"))
            {
                // Remove it
                nameSpace = nameSpace.Remove(0, 3);
            }

            // Match with regular expression
            MatchCollection matches = Regex.Matches(nameSpace, @"_[0-9]+_");

            // Walk reversed
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                /* Validate odd underscores */

                // Counter
                int count = 0;

                // Get underscores
                for (int j = matches[i].Index; j >= 0; j--)
                {
                    // Check for non-underscore
                    if (nameSpace[j].ToString() != "_")
                    {
                        // Halt flow
                        break;
                    }

                    // Rise counter
                    count++;
                }

                // Check for odd
                if ((count % 2) == 0)
                {
                    // Move to next iteration
                    continue;
                }

                // Convert
                try
                {
                    // Declare
                    int intVal;

                    // Parse from string
                    if (int.TryParse(matches[i].Value.Replace("_", string.Empty), NumberStyles.Integer, null, out intVal))
                    {
                        // Remove original
                        nameSpace = nameSpace.Remove(matches[i].Index, matches[i].Length);

                        // Insert replacement
                        nameSpace = nameSpace.Insert(matches[i].Index, char.ConvertFromUtf32(intVal).ToString());
                    }
                }
                catch (Exception ex)
                {
                    // Let it fall through
                }
            }

            // Replace double-space with single
            nameSpace = nameSpace.Replace("__", " ");

            // Processed namespace back
            return nameSpace;
        }
    }
}