using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Text;

namespace Pisces
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            Editor.Document.Blocks.Clear();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Pisces Text Files (*.pt)|*.pt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(openFileDialog.FileName).ToLower();
                Editor.Document.Blocks.Clear();
                if (extension == ".txt")
                {
                    Editor.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openFileDialog.FileName))));
                }
                else if (extension == ".rtf")
                {
                    TextRange range = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
                    using (FileStream fStream = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        range.Load(fStream, DataFormats.Rtf);
                    }
                }
                else if (extension == ".pt")
                {
                    Editor.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openFileDialog.FileName))));
                }
                // Add additional formats handling here if needed
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Pisces Text Files (*.pt)|*.pt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                if (extension == ".txt" || extension == ".pt")
                {
                    File.WriteAllText(saveFileDialog.FileName, new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd).Text);
                }
                else if (extension == ".rtf")
                {
                    TextRange range = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
                    using (FileStream fStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        range.Save(fStream, DataFormats.Rtf);
                    }
                }
                // Add additional formats handling here if needed
            }
        }

        private void ViewFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pisces Text Files (*.pt)|*.pt|DLL Files (*.dll)|*.dll|Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(openFileDialog.FileName).ToLower();
                Editor.Document.Blocks.Clear();
                if (extension == ".pt")
                {
                    Editor.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openFileDialog.FileName))));
                }
                else if (extension == ".dll" || extension == ".bat" || extension == ".cmd")
                {
                    byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                    string fileContent = Encoding.UTF8.GetString(fileBytes);
                    Editor.Document.Blocks.Add(new Paragraph(new Run(fileContent)));
                }
                // Add additional formats handling here if needed
            }
        }

        private void CombineFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Pisces Text Files (*.pt)|*.pt|DLL Files (*.dll)|*.dll|Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All Files (*.*)|*.*",
                Multiselect = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                StringBuilder combinedContent = new StringBuilder();
                foreach (string file in openFileDialog.FileNames)
                {
                    combinedContent.AppendLine(File.ReadAllText(file));
                }
                Editor.Document.Blocks.Clear();
                Editor.Document.Blocks.Add(new Paragraph(new Run(combinedContent.ToString())));
            }
        }

        private void OpenSystemCode_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DLL Files (*.dll)|*.dll|Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(openFileDialog.FileName).ToLower();
                Editor.Document.Blocks.Clear();
                if (extension == ".dll" || extension == ".bat" || extension == ".cmd")
                {
                    byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                    string fileContent = Encoding.UTF8.GetString(fileBytes);
                    Editor.Document.Blocks.Add(new Paragraph(new Run(fileContent)));
                }
                // Add additional formats handling here if needed
            }
        }
    }
}
