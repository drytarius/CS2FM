using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace CS2FM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
        }

        private PrivateFontCollection privateFonts = new PrivateFontCollection();


        //--[[---------------------------------------------------------
        //	Name: Save, load, read, fetch fonts.
        //	Desc: Self explanatory.
        //-----------------------------------------------------------]]

        private void LoadSettings()
        {
            // Temporarily detach event handlers to prevent triggering during LoadSettings
            corepath.TextChanged -= corepath_TextChanged;
            gamepath.TextChanged -= gamepath_TextChanged;

            corepath.Text = Settings.Default.CoreFilePath;
            gamepath.Text = Settings.Default.GameFilePath;

            float scaledValue = sliderFontSize.Value * 0.01f;

            fontSizeLabel.Text = "Font Size: " + scaledValue.ToString("0.00");

            // Initialize font list
            fontlist.Items.Clear();

            // Load font details from settings
            StringCollection serializedFontDetails = Settings.Default.FontList;
            if (serializedFontDetails != null)
            {
                foreach (string serializedFont in serializedFontDetails)
                {
                    // Deserialize the serialized font details string into a FontDetails object
                    string[] fontParts = serializedFont.Split(',');
                    if (fontParts.Length == 3)
                    {
                        FontDetails fontDetails = new FontDetails(fontParts[0], fontParts[1], fontParts[2]);
                        fontlist.Items.Add(fontDetails);
                    }
                }
            }

            // Reattach event handlers after settings have been loaded
            corepath.TextChanged += corepath_TextChanged;
            gamepath.TextChanged += gamepath_TextChanged;

            // Enable drag-and-drop on the fontlist
            fontlist.AllowDrop = true;

            // Attach event handlers for drag-and-drop
            fontlist.DragEnter += fontlist_DragEnter;
            fontlist.DragDrop += fontlist_DragDrop;
        }


        private void SaveSettings()
        {
            Settings.Default.CoreFilePath = corepath.Text;
            Settings.Default.GameFilePath = gamepath.Text;

            // Initialize a new StringCollection for font details
            StringCollection serializedFontDetails = new StringCollection();
            foreach (FontDetails fontDetails in fontlist.Items)
            {
                // Serialize FontDetails object into a string and add it to the StringCollection
                string serializedFont = $"{fontDetails.Path},{fontDetails.FileName},{fontDetails.FontName}";
                serializedFontDetails.Add(serializedFont);
            }

            // Save serialized font details to settings
            Settings.Default.FontList = serializedFontDetails;
            Settings.Default.Save();
        }

        private string GetFontName(string filePath)
        {
            // Load the font file
            using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
            {
                privateFontCollection.AddFontFile(filePath);

                // Get the font family name
                if (privateFontCollection.Families.Length > 0)
                {
                    return privateFontCollection.Families[0].Name;
                }
            }

            return null;
        }

        // Define a class to hold font details
        public class FontDetails
        {
            public string Path { get; }
            public string FileName { get; }
            public string FontName { get; }

            public FontDetails(string path, string fileName, string fontName)
            {
                Path = path;
                FileName = fileName;
                FontName = fontName;
            }

            public override string ToString()
            {
                return $"{FontName} ({FileName}) - {Path}";
            }
        }



        //--[[---------------------------------------------------------
        //	Name: Select directories and display the selected directory as label content.
        //	Desc: Self explanatory.
        //-----------------------------------------------------------]]

        private void corebtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select Core Directory";

                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
                    corepath.Text = selectedFolderPath;
                    SaveSettings();
                }
            }
        }

        private void gamebtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select Game Directory";

                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
                    gamepath.Text = selectedFolderPath;
                    SaveSettings();
                }
            }
        }

        private void corepath_TextChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void gamepath_TextChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        //--[[---------------------------------------------------------
        //	Name: Automatic path detection.
        //	Desc: Detects both "core" and "game" paths automatically.
        //-----------------------------------------------------------]]

        private void autodetectbtn_Click(object sender, EventArgs e)
        {
            string steamPath = FindSteamPath();
            if (string.IsNullOrEmpty(steamPath))
            {
                MessageBox.Show("Steam is not installed or the Steam directory could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check for CS:GO installation directory
            string csgoPath = Path.Combine(steamPath, "steamapps", "common", "Counter-Strike Global Offensive");
            if (!Directory.Exists(csgoPath))
            {
                MessageBox.Show("Counter-Strike Global Offensive is not installed or the installation path could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Detect the core path
            string corePath = Path.Combine(csgoPath, "game", "core", "panorama", "fonts", "conf.d");
            if (!Directory.Exists(corePath))
            {
                MessageBox.Show("Core path could not be found in CS:GO installation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Detect the game path
            string gamePath = Path.Combine(csgoPath, "game", "csgo", "panorama", "fonts");
            if (!Directory.Exists(gamePath))
            {
                MessageBox.Show("Game path could not be found in CS:GO installation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set the detected paths
            corepath.Text = corePath;
            gamepath.Text = gamePath;

            MessageBox.Show("Paths have been successfully autodetected.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string FindSteamPath()
        {
            // Get all logical drives on the machine
            string[] drives = Environment.GetLogicalDrives();

            // Iterate through each drive and check for Steam installation
            foreach (var drive in drives)
            {
                string potentialSteamPath = Path.Combine(drive, "Program Files (x86)", "Steam");
                if (Directory.Exists(potentialSteamPath))
                {
                    return potentialSteamPath;
                }

                potentialSteamPath = Path.Combine(drive, "Program Files", "Steam");
                if (Directory.Exists(potentialSteamPath))
                {
                    return potentialSteamPath;
                }
            }

            return null; // Steam directory not found
        }

        //--[[---------------------------------------------------------
        //	Name: Open related paths.
        //	Desc: Self explanatory.
        //-----------------------------------------------------------]]

        private void OpenPath(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("The path is empty. Please specify a valid path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to open the path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void opencorepathbtn_Click(object sender, EventArgs e)
        {
            OpenPath(corepath.Text);
        }

        private void opengamepathtbn_Click(object sender, EventArgs e)
        {
            OpenPath(gamepath.Text);
        }


        //--[[---------------------------------------------------------
        //	Name: Add or remove fonts.
        //	Desc: Set of buttons.
        //-----------------------------------------------------------]]

        private void addfontbtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Font Files (*.ttf, *.otf, *.fon, *.fnt, *.ttc, *.woff, *.woff2, *.eot)|*.ttf;*.otf;*.fon;*.fnt;*.ttc;*.woff;*.woff2;*.eot";
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Add Font File(s)";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        // Extract font name from font file
                        string fontName = GetFontName(filePath);

                        // Add font details to the font list
                        fontlist.Items.Add(new FontDetails(filePath, Path.GetFileName(filePath), fontName));
                    }

                    SaveSettings();
                }
            }
        }

        private void removefontbtn_Click(object sender, EventArgs e)
        {
            // Remove selected font from the list
            if (fontlist.SelectedItem != null)
            {
                fontlist.Items.Remove(fontlist.SelectedItem);
                SaveSettings();
            }
            else
            {
                MessageBox.Show("Please select a font to remove from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //--[[---------------------------------------------------------
        //	Name: Font list drag && drop.
        //	Desc: Accepts font files alternatively by dragging and dropping.
        //-----------------------------------------------------------]]

        private void fontlist_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the data being dragged is a file or files
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void fontlist_DragDrop(object sender, DragEventArgs e)
        {
            // Get the file paths being dropped
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string filePath in filePaths)
            {
                if (IsValidFontFile(filePath))
                {
                    string fontName = GetFontName(filePath);

                    fontlist.Items.Add(new FontDetails(filePath, Path.GetFileName(filePath), fontName));
                }
            }

            SaveSettings();
        }

        // Method to check if the file is a valid font file
        private bool IsValidFontFile(string filePath)
        {
            string[] validExtensions = { ".ttf", ".otf", ".fon", ".fnt", ".ttc", ".woff", ".woff2", ".eot" };
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return validExtensions.Contains(fileExtension);
        }



        //--[[---------------------------------------------------------
        //	Name: Move selected font list item.
        //	Desc: Moves the selected font in the list up or down.
        //-----------------------------------------------------------]]

        private void upbtn_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void downbtn_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        private void MoveItem(int direction)
        {
            // Check if an item is selected and it's not the first or last item
            if (fontlist.SelectedItem != null && fontlist.SelectedIndex >= 0 && fontlist.SelectedIndex < fontlist.Items.Count)
            {
                // Get the index of the selected item
                int index = fontlist.SelectedIndex;

                // Calculate the new index after moving the item
                int newIndex = index + direction;

                // Ensure the new index is within bounds
                if (newIndex >= 0 && newIndex < fontlist.Items.Count)
                {
                    // Remove the selected item from the ListBox
                    object selectedItem = fontlist.SelectedItem;
                    fontlist.Items.Remove(selectedItem);

                    // Insert the item at the new position
                    fontlist.Items.Insert(newIndex, selectedItem);

                    // Select the item at the new position
                    fontlist.SetSelected(newIndex, true);
                }
            }
        }














        //--[[---------------------------------------------------------
        //	Name: Font-size slider.
        //	Desc: Changes the font-size of the font.
        //-----------------------------------------------------------]]

        private void sliderFontSize_Scroll(object sender, EventArgs e)
        {
            float scaledValue = sliderFontSize.Value * 0.01f;

            fontSizeLabel.Text = "Font Size: " + scaledValue.ToString("0.00");

            float dpiAdjustedFontSize = scaledValue * (72.0f / 96.0f) * 10;
            fontPreviewTextBox.Font = new Font(fontPreviewTextBox.Font.FontFamily, dpiAdjustedFontSize);
        }

        //--[[---------------------------------------------------------
        //	Name: Font preview.
        //	Desc: Previews the font.
        //-----------------------------------------------------------]]

        private void fontlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (fontlist.SelectedItem is FontDetails selectedFontDetails)
                {
                    string fontFilePath = selectedFontDetails.Path;

                    //Console.WriteLine($"Selected Font Path: {fontFilePath}");

                    privateFonts.Dispose();
                    privateFonts = new PrivateFontCollection();

                    privateFonts.AddFontFile(fontFilePath);

                    FontFamily fontFamily = privateFonts.Families[0];

                    Font previewFont = new Font(fontFamily, fontPreviewTextBox.Font.Size);

                    fontPreviewTextBox.Font = previewFont;

                    Console.WriteLine(selectedFontDetails);
                }
                else
                {
                    Font defaultFont = SystemFonts.DefaultFont;
                    fontPreviewTextBox.Font = defaultFont;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--[[---------------------------------------------------------
        //	Name: Apply selected font.
        //	Desc: Searches for the default "stratum2.uifont" deletes and replaces it with the selected font.
        //  Replaces the "font.conf" and "42-repl-global.conf" accordingly.
        //-----------------------------------------------------------]]

        public class FontSizeValues
        {
            public float FONTSIZE100 { get; set; }
            public float FONTSIZE90 { get; set; }
            public float FONTSIZE80 { get; set; }
            public float FONTSIZE70 { get; set; }
            public float FONTSIZE60 { get; set; }
            public float FONTSIZE50 { get; set; }
            public float FONTSIZE40 { get; set; }
            public float FONTSIZE30 { get; set; }
            public float FONTSIZE20 { get; set; }
            public float FONTSIZE10 { get; set; }

            public FontSizeValues(float sliderValue)
            {
                FONTSIZE100 = sliderValue * 1.00f;
                FONTSIZE90 = sliderValue * 0.90f;
                FONTSIZE80 = sliderValue * 0.80f;
                FONTSIZE70 = sliderValue * 0.70f;
                FONTSIZE60 = sliderValue * 0.60f;
                FONTSIZE50 = sliderValue * 0.50f;
                FONTSIZE40 = sliderValue * 0.40f;
                FONTSIZE30 = sliderValue * 0.30f;
                FONTSIZE20 = sliderValue * 0.20f;
                FONTSIZE10 = sliderValue * 0.10f;
            }
        }


        private void applyfontbtn_Click(object sender, EventArgs e)
        {
            if (fontlist.SelectedItem == null)
            {
                MessageBox.Show("Please select a font from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected font details
            FontDetails selectedFont = (FontDetails)fontlist.SelectedItem;

            // Get the current value of the slider (Slider value is multiplied by 0.01 to scale it to the range of 0.25 - 3.00)
            float sliderValue = sliderFontSize.Value * 0.01f;

            // Create the FontSizeValues object based on the slider value
            FontSizeValues fontSizeValues = new FontSizeValues(sliderValue);

            // Read the content of the template fonts.conf file from resources as byte[]
            byte[] templateFontsConfBytes = Properties.Resources.fonts_conf;
            byte[] template42ReplGlobalConfBytes = Properties.Resources.global;

            try
            {
                // Convert byte[] content to string
                string templateFontsConfContent = Encoding.UTF8.GetString(templateFontsConfBytes);
                string template42ReplGlobalConfContent = Encoding.UTF8.GetString(template42ReplGlobalConfBytes);

                // Replace font size placeholders dynamically based on the fontSizeValues object
                var fontSizeMappings = new Dictionary<string, string>
                {
                    { "FONTSIZE100", fontSizeValues.FONTSIZE100.ToString("F2") },
                    { "FONTSIZE90", fontSizeValues.FONTSIZE90.ToString("F2") },
                    { "FONTSIZE80", fontSizeValues.FONTSIZE80.ToString("F2") },
                    { "FONTSIZE70", fontSizeValues.FONTSIZE70.ToString("F2") },
                    { "FONTSIZE60", fontSizeValues.FONTSIZE60.ToString("F2") },
                    { "FONTSIZE50", fontSizeValues.FONTSIZE50.ToString("F2") },
                    { "FONTSIZE40", fontSizeValues.FONTSIZE40.ToString("F2") },
                    { "FONTSIZE30", fontSizeValues.FONTSIZE30.ToString("F2") },
                    { "FONTSIZE20", fontSizeValues.FONTSIZE20.ToString("F2") },
                    { "FONTSIZE10", fontSizeValues.FONTSIZE10.ToString("F2") }
                };

                // Perform font size replacements in the fonts.conf content
                foreach (var kvp in fontSizeMappings)
                {
                    templateFontsConfContent = templateFontsConfContent.Replace(kvp.Key, kvp.Value);
                }

                // Replace font filename and font name placeholders
                templateFontsConfContent = templateFontsConfContent.Replace("FONTFILENAME", selectedFont.FileName);
                templateFontsConfContent = templateFontsConfContent.Replace("FONTNAME", selectedFont.FontName);

                // Do the same for the 42-repl-global.conf content
                template42ReplGlobalConfContent = template42ReplGlobalConfContent.Replace("FONTFILENAME", selectedFont.FileName);
                template42ReplGlobalConfContent = template42ReplGlobalConfContent.Replace("FONTNAME", selectedFont.FontName);

                // Get the game and core directory paths
                string gameDirectory = gamepath.Text;
                string coreDirectory = corepath.Text;

                string selectedFontPath = selectedFont.Path;

                string stratum2FontPath = Path.Combine(gameDirectory, "stratum2.uifont");
                string fontsConfPath = Path.Combine(gameDirectory, "fonts.conf");
                string replGlobalConfPath = Path.Combine(coreDirectory, "42-repl-global.conf");

                // Delete existing stratum2.uifont file if it exists
                if (File.Exists(stratum2FontPath))
                {
                    File.Delete(stratum2FontPath);
                }

                // Write the modified content to the fonts.conf file in the game directory
                File.WriteAllText(fontsConfPath, templateFontsConfContent);

                // Write the modified content to the 42-repl-global.conf file in the core directory
                File.WriteAllText(replGlobalConfPath, template42ReplGlobalConfContent);

                // Copy the selected font to the game directory
                string selectedFontFileName = Path.GetFileName(selectedFontPath);
                string destinationPath = Path.Combine(gameDirectory, selectedFontFileName);
                File.Copy(selectedFontPath, destinationPath, true);

                MessageBox.Show("Font applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while applying the font: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--[[---------------------------------------------------------
        //	Name: Font reset.
        //	Desc: Resets the font, replacing "fonts.conf", "42-repl-global.conf" with default ones.
        //  Adds "stratum2.uifont" back to the core directory.
        //-----------------------------------------------------------]]

        private void resetfontbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Read the default resource files
                byte[] defaultFontsConfBytes = Properties.Resources.fonts_default;
                byte[] defaultGlobalConfBytes = Properties.Resources.global_default;
                byte[] defaultStratumFontBytes = Properties.Resources.stratum2;
                // Get the game and core directory paths
                string gameDirectory = gamepath.Text;
                string coreDirectory = corepath.Text;

                // Paths to the files to be replaced
                string fontsConfPath = Path.Combine(gameDirectory, "fonts.conf");
                string replGlobalConfPath = Path.Combine(coreDirectory, "42-repl-global.conf");
                string stratumFontPath = Path.Combine(gameDirectory, "stratum2.uifont");

                // Write the default content to the respective configuration files
                File.WriteAllBytes(fontsConfPath, defaultFontsConfBytes);
                File.WriteAllBytes(replGlobalConfPath, defaultGlobalConfBytes);

                // Copy the default stratum2 font to the game directory
                File.WriteAllBytes(stratumFontPath, defaultStratumFontBytes);

                MessageBox.Show("Font configuration has been reset to default.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while resetting the font configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
