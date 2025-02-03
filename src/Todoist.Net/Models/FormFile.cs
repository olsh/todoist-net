namespace Todoist.Net.Models
{
    internal class FormFile
    {
        public FormFile(byte[] content, string filename, string mimeType = null)
        {
            Content  = content;
            Filename = filename;
            MimeType = mimeType;
        }

        public byte[] Content;
        public string Filename;
        public string MimeType;
    }
}
