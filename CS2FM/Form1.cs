using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using System.Text;

namespace CS2FM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
        }



        //--[[---------------------------------------------------------
        //	Name: Save, load, read, fetch fonts.
        //	Desc: Self explanatory.
        //-----------------------------------------------------------]]

        private void LoadSettings()
        {
            corepath.Text = Settings.Default.CoreFilePath;
            gamepath.Text = Settings.Default.GameFilePath;

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
        //	Name: Apply selected font.
        //	Desc: Searches for the default "stratum2.uifont" deletes and replaces it with the selected font.
        //  Replaces the "font.conf" and "42-repl-global.conf" accordingly.
        //-----------------------------------------------------------]]

        private void applyfontbtn_Click(object sender, EventArgs e)
        {
            if (fontlist.SelectedItem == null)
            {
                MessageBox.Show("Please select a font from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected font details
            FontDetails selectedFont = (FontDetails)fontlist.SelectedItem;

            // Read the content of the template fonts.conf file from resources as byte[]
            byte[] templateFontsConfBytes = Properties.Resources.fonts_conf;
            byte[] template42ReplGlobalConfBytes = Properties.Resources.global;

            try
            {
                // Convert byte[] content to string
                string templateFontsConfContent = Encoding.UTF8.GetString(templateFontsConfBytes);
                string template42ReplGlobalConfContent = Encoding.UTF8.GetString(template42ReplGlobalConfBytes);

                // Replace placeholders with selected font details
                templateFontsConfContent = templateFontsConfContent.Replace("FONTFILENAME", selectedFont.FileName);
                templateFontsConfContent = templateFontsConfContent.Replace("FONTNAME", selectedFont.FontName);

                // Replace placeholders with selected font details in 42-repl-global.conf
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
                File.Copy(selectedFontPath, destinationPath, true); // The 'true' parameter allows overwriting if the file already exists
                MessageBox.Show("Font applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while applying the font: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
