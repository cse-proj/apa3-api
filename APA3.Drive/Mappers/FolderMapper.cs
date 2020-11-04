using System.Collections.Generic;
using APA3.Drive.Models;
using Google.Apis.Drive.v2.Data;

namespace APA3.Drive.Mappers
{
    public class FolderMapper
    {
        public FolderModel Map(File folder, IEnumerable<FileModel> subfiles, IEnumerable<FolderModel> subfolders)
        {
            return new FolderModel()
            {
                Id = folder.Id,
                Title = folder.Title,
                Subfiles = subfiles,
                Subfolders = subfolders
            };
        }
    }
}