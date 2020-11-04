using System.Collections.Generic;

namespace APA3.Drive.Models
{
    public class FolderModel : BaseFileModel
    {
        public IEnumerable<FileModel> Subfiles { get; set; }
        public IEnumerable<FolderModel> Subfolders { get; set; }
    }
}