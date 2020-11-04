using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Json;

namespace APA3.Drive.Services
{
    public class GoogleServiceAccountOauthService
    {
        private const string jsonCredPath = "apa3-account-cred.json";

        public ServiceAccountCredential Credential(IEnumerable<string> scopes)
        {
            // Load and deserialize credential parameters from the specified JSON file.
            JsonCredentialParameters credentialParameters;
            using (var stream = new FileStream(jsonCredPath, FileMode.Open, FileAccess.Read))
            {
                credentialParameters = NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
            }

            // Service account credential initializer    
            ServiceAccountCredential.Initializer initializer = 
                new ServiceAccountCredential.Initializer(credentialParameters.ClientEmail)
                {
                    Scopes = scopes
                };

            // create credential
            ServiceAccountCredential credential = 
                new ServiceAccountCredential(initializer.FromPrivateKey(credentialParameters.PrivateKey));

            return credential;
        }
    }
}