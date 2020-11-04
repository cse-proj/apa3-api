using APA3.Drive.Models;
using Google.Apis.Drive.v2.Data;

namespace APA3.Drive.Mappers
{
    public class FileMapper
    {
        public FileModel Map(File file)
        {
            return new FileModel()
            {
                Id = file.Id,
                Title = file.Title,
                EmbedLink = file.EmbedLink
            };
        }
    }
}