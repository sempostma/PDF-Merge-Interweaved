using System.ComponentModel;
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
            System.Diagnostics.Process.Start("https://github.com/sempostma");
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
            filesListBox.Refresh();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            int index = filesListBox.SelectedIndex;
            if (index == filesList.Count + 1 || index == -1) return;
            var file = filesList[index];
            filesList.RemoveAt(index);
            filesList.Insert(index + 1, file);
            filesListBox.Refresh();
        }

        private void RemoveSelected_Click(object sender, EventArgs e)
        {
            int index = filesListBox.SelectedIndex;
            if (index == -1) return;
            filesList.RemoveAt(index);
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