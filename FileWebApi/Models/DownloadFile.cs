namespace FileWebApi.Models
{
    /// <summary>
    /// General downloading file class.
    /// </summary>
    public class DownloadFile
    {
        /// <summary>
        /// Gets or sets the File Name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        public byte[] Content { get; set; }
    }
}
