namespace Factory
{
    public class DocumentFactory
    {
        public static Document CreateDocument(string type)
        {
            if (type.ToLower() == "pdf")
                return new PDFDocument();
            else if (type.ToLower() == "word")
                return new WordDocument();
            else
                return null;
        }
    }
}
