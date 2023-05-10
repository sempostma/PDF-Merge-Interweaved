using UglyToad.PdfPig;

namespace MergeEvenOddPDF
{
    internal class PDFFile
    {
        public string Name { get; set; }
        public string FileLocation { get; set; }
        public int Pages { get; set; }
        public PdfDocument Document { get; set; }
        public string Label { get; set; }
    }
}