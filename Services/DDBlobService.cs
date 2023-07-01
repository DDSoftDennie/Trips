namespace Services
{
    using Azure.Storage.Blobs;
    using System.Collections.Generic;

    public class DDBlobService
    {
        private BlobContainerClient? _blobContainer;
        private BlobServiceClient _blobService;
       // private string _connectionString;

        public DDBlobService(string connectionString)
        {
            _blobService = new BlobServiceClient(connectionString);
        }

        public void CreateContainer(string containerName)
        {
            _blobContainer = _blobService.GetBlobContainerClient(containerName);
            _blobContainer.CreateIfNotExists();
        }

        public void UploadBlob(string fileName)
        {
			//only preform if _blobContainer != null
			if (_blobContainer != null)
			{
				var blob = _blobContainer.GetBlobClient(fileName);
				blob.Upload(fileName);
            }
		}

		public void DeleteBlob(string fileName)
		{
			if (_blobContainer != null)
			{
				var blob = _blobContainer.GetBlobClient(fileName);
				blob.DeleteIfExists();
			}
		}

		public IEnumerable<string> ListBlobs()
		{
			if (_blobContainer != null)
			{
				var blobs = _blobContainer.GetBlobs();
				foreach (var blob in blobs)
				{
					yield return blob.Name;
				}
			}
		}
	
        public void DeleteContainer(string containerName)
        {
            _blobContainer = _blobService.GetBlobContainerClient(containerName);
            _blobContainer.Delete();
        }

         public List<string> GetInfoFromBlobsInContainer()
         {
            if (_blobContainer == null)
            {
                throw new Exception("Container not found!");
            }
		
            var blobs = _blobContainer.GetBlobs();
            List<string> blobInfos = new List<string>();
            foreach(var blob in blobs)
            {
                blobInfos.Add($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss} Size: {blob.Properties.ContentLength}");
            }
            return blobInfos;
		}
	}
}