using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    public class FileAttachment
    {
        public FileAttachment(string fileName, string fileUrl)
        {
            FileName = fileName;
            FileUrl = fileUrl;
        }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("file_size")]
        public int FileSize { get; internal set; }

        [JsonProperty("file_type")]
        public string FileType { get; internal set; }

        [JsonProperty("file_url")]
        public string FileUrl { get; set; }

        [JsonProperty("upload_state")]
        public string UploadState { get; internal set; }
    }
}
