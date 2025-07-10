using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;

namespace GatewayAPI.Services;

public class AzureStorageService
{
    public async Task <string> UploadFileAsync(IFormFile file, string containerName, CancellationToken cancellationToken)
    {
        if (file.Length == 0)
            throw new ArgumentException("File is empty", nameof(file));

        var storageAccount = new CloudStorageAccount(new StorageCredentials("your_account_name", "your_account_key"), true);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(containerName);
        await container.CreateIfNotExistsAsync(cancellationToken);

        var blob = container.GetBlockBlobReference(file.FileName);
        using (var stream = file.OpenReadStream())
        {
            await blob.UploadFromStreamAsync(stream, cancellationToken);
        }

        return blob.Uri.ToString();
    }
}