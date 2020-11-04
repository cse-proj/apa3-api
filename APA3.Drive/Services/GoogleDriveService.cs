using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using APA3.Drive.Mappers;
using APA3.Drive.Models;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using APA3.GoogleAuth.Services;

namespace APA3.Drive.Services
{
    public class GoogleDriveService
    {
        private const string applicationName = "Active Park Assist";
        private readonly GoogleServiceAccountOauthService _oauthService;
        private readonly DriveService _drive;
        private readonly DriveMapper _driveMapper;

        public GoogleDriveService(GoogleServiceAccountOauthService oauthService, DriveMapper driveMapper)
        {
            _driveMapper = driveMapper;
            _oauthService = oauthService;

            // Auth scopes
            IEnumerable<string> scopes = new List<string>
                {
                    DriveService.Scope.DriveReadonly
                };
                
            _drive = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _oauthService.Credential(scopes),
                    ApplicationName = applicationName
                });
        }

        public async Task<IEnumerable<BaseFileModel>> GetFolders()
        {
            FileList files = await _drive.Files.List().ExecuteAsync();

            Dictionary<string, List<int>> parentChildrenDict = 
                GetParentsWithChildren(files);

            HashSet<int> highLevelFiles = GetHighLevelFiles(parentChildrenDict);

            List<FolderModel> models = new List<FolderModel>();

            foreach (var index in highLevelFiles)
            {
                models.Add(_driveMapper.MapFolder(files.Items[index].Id, parentChildrenDict, files));
            }

            return models;
        }

        public async Task<BaseFileModel> GetFolder(string id)
        {
            var files = await _drive.Files.List().ExecuteAsync();

            Dictionary<string, List<int>> parentChildrenDict = 
                GetParentsWithChildren(files);

            return _driveMapper.Map(id, parentChildrenDict, files);
        }

        public async Task<BaseFileModel> GetFile(string id)
        {
            var files = await _drive.Files.List().ExecuteAsync();

            return _driveMapper.Map(files.Items.FirstOrDefault(file => file.Id == id));
        }

        private Dictionary<string, List<int>> GetParentsWithChildren(FileList files)
        {
            // first child is the index of the file itself
            Dictionary<string, List<int>> parentChildrenDict =
                new Dictionary<string, List<int>>();

            for (int i = 0; i < files.Items.Count; ++i)
            {
                if (!parentChildrenDict.ContainsKey(files.Items[i].Id))
                {
                    parentChildrenDict.Add(files.Items[i].Id, new List<int>{ i });
                }
            }

            for (int i = 0; i < files.Items.Count; ++i)
            {
                if (files.Items[i].Parents.Count != 0)
                {
                    foreach (var parent in files.Items[i].Parents)
                    {
                        parentChildrenDict[parent.Id].Add(i);
                    }
                }
            }

            return parentChildrenDict;
        }

        private HashSet<int> GetHighLevelFiles(Dictionary<string, List<int>> parentChildrenDict)
        {
            HashSet<int> fileIndices = new HashSet<int>();

            if (parentChildrenDict.Count == 0)
            {
                return fileIndices;
            }

            foreach (var entry in parentChildrenDict)
            {
                for (int i = 0; i < entry.Value.Count; ++i)
                {
                    if (fileIndices.Contains(entry.Value[i]))
                    {
                        fileIndices.Remove(entry.Value[i]);
                        continue;
                    }
                    fileIndices.Add(entry.Value[i]);
                }
            }

            return fileIndices;
        }
    }
}