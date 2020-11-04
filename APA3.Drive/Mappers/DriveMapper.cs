using System;
using System.Collections.Generic;
using APA3.Drive.Models;
using Google.Apis.Drive.v2.Data;

namespace APA3.Drive.Mappers
{
    public class DriveMapper
    {
        private readonly FileMapper _fileMapper;
        private readonly FolderMapper _folderMapper;

        public DriveMapper(FileMapper fileMapper, FolderMapper folderMapper)
        {
            _fileMapper = fileMapper;
            _folderMapper = folderMapper;
        }

        public FileModel Map(File file)
        {
            return _fileMapper.Map(file);
        }

        public BaseFileModel Map(string fileId, Dictionary<string, List<int>> childrenIndices, FileList files)
        {
            if (!IsFolder(files.Items[childrenIndices[fileId][0]]))
            {
                return Map(files.Items[childrenIndices[fileId][0]]);
            }

            return MapFolder(fileId, childrenIndices, files);
        }

        public FolderModel MapFolder(string folderId, Dictionary<string, List<int>> childrenIndicesDict, FileList files)
        {
            List<FileModel> subfiles = new List<FileModel>();
            List<FolderModel> subfolders = new List<FolderModel>();

            List<int> childrenIndices = childrenIndicesDict[folderId];

            for (int i = 1; i < childrenIndices.Count; ++i)
            {
                if (IsFolder(files.Items[childrenIndices[i]]))
                {
                    subfolders.Add(MapFolder(files.Items[childrenIndices[i]].Id, childrenIndicesDict, files));
                    continue;
                }

                subfiles.Add(Map(files.Items[childrenIndices[i]]));
            }

            return _folderMapper.Map(files.Items[childrenIndices[0]], subfiles, subfolders);
        }

        private bool IsFolder(File file)
        {
            var folderMimeType = "application/vnd.google-apps.folder";

            return file.MimeType.Equals(folderMimeType, StringComparison.OrdinalIgnoreCase);
        }
    }
}