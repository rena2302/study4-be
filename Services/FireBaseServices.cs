using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using NuGet.Protocol;
using study4_be.Models;

namespace study4_be.Services
{
    public class FireBaseServices
    {
        private readonly IConfiguration _config;
        private readonly FirebaseServiceAccountKey _serviceAccountKey;
        private readonly string _firebaseBucketName;

        public FireBaseServices(IConfiguration config)
        {
            _config = config;
            _serviceAccountKey = _config.GetSection("Firebase:ServiceAccountKey").Get<FirebaseServiceAccountKey>();
            _firebaseBucketName = _config["Firebase:StorageBucket"];

            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(_serviceAccountKey.ToJson())
            });
        }

        public string GetFirebaseBucketName()
        {
            return _firebaseBucketName;
        }
    }
}