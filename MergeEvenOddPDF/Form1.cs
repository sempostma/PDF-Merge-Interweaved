using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Writer;

namespace MergeEvenOddPDF
{
    public partial class MainForm : Form
    {
        BindingList<PDFFile> filesList = new BindingList<PDFFile>();

        public MainForm()
        {
            InitializeComponent();

            filesListBox.DataSource = filesList;
            filesList.ListChanged += OnListChanged;
        }

        private void Credits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/sempostma");
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        protected void OnListChanged(object sender, ListChangedEventArgs args)
        {
            Up.Enabled = filesList.Count > 1;
            Down.Enabled = filesList.Count > 1;
            RemoveSelected.Enabled = filesList.Count > 0;
            Combine.Enabled = filesList.Count > 0;
        }

        private void chooseFilesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var o = new OpenFileDialog();
                o.Multiselect = true;
                o.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if (o.ShowDialog() == DialogResult.OK)
                {
                    o.FileNames.ToList().ForEach(filePath =>
                    {
                        var doc = PdfDocument.Open(filePath);
                        var fileName = Path.GetFileName(filePath);

                        if (doc.IsEncrypted)
                        {
                            MessageBox.Show($"{fileName} is encrypted", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var item = new PDFFile
                        {
                            Name = fileName,
                            FileLocation = filePath,
                            Document = doc,
                            Pages = doc.NumberOfPages,
                            Label = fileName + " (" + doc.NumberOfPages + ")"
                        };

                        filesList.Add(item);
                    });

                    filesListBox.Refresh();
                }

                else
                {
                    MessageBox.Show("Files could not be loaded", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Up_Click(object sender, EventArgs e)
        {
            int index = filesListBox.SelectedIndex;
            if (index == 0 || index == -1) return;
            var file = filesList[index];
            filesList.RemoveAt(index);
            filesList.Insert(index - 1, file);
            filesListBox.SetSelected(index - 1, true);
            filesListBox.Refresh();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            int index = filesListBox.SelectedIndex;
            if (index + 1 == filesList.Count || index == -1) return;
            var file = filesList[index];
            filesList.RemoveAt(index);
            filesList.Insert(index + 1, file);
            filesListBox.SetSelected(index + 1, true);
            filesListBox.Refresh();
        }

        private void RemoveSelected_Click(object sender, EventArgs e)
        {
            int index = filesListBox.SelectedIndex;
            if (index == -1) return;
            filesList.RemoveAt(index);
            filesListBox.ClearSelected();
            filesListBox.Refresh();
        }

        private void Combine_Click(object sender, EventArgs e)
        {
            PdfDocumentBuilder builder = new PdfDocumentBuilder();

            int maxLen = filesList.Select(x => x.Pages).Max();

            if (interweave.Checked)
            {
                for (int i = 1; i <= maxLen; i++)
                {
                    for (int j = 0; j < filesList.Count; j++)
                    {
                        if (i <= filesList[j].Pages)
                        {
                            PdfDocument doc = filesList[j].Document;
                            builder.AddPage(doc, i);
                        }
                    }
                }
            } else
            {
                for (int i = 0; i < filesList.Count; i++)
                {
                    PDFFile file = filesList[i];

                    for (int j = 1; j <= file.Pages; j++)
                    {
                        builder.AddPage(file.Document, j);
                    }
                }
            }

            Stream myStream;
            SaveFileDialog saveResultFileDialog = new SaveFileDialog();

            saveResultFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveResultFileDialog.FilterIndex = 0;
            saveResultFileDialog.RestoreDirectory = true;

            if (saveResultFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveResultFileDialog.OpenFile()) != null)
                {
                    myStream.Write(builder.Build());
                    myStream.Close();
                }
            }
        }
    }
}