using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.Tasks;
using Library.Tools.Thread;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Azure
{
    public enum BlobContainerPublicAccessEnum
    {
        Blob,
        Container,
        Off
    }

    public class BlobContainer : ProgressableCancellable
    {
        #region Public CONSTRUCTORS

        /// <summary>
        /// Création blob si risque de déconnection avec le blob
        /// </summary>
        public BlobContainer(ProgressCancelNotifier iNotifier, bool iWithSafeNetwork)
            : base(iNotifier)
        {
            WithSafeNetwork = iWithSafeNetwork;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Création blob si pas de risque de déconnection avec le Blob
        /// </summary>
        public BlobContainer()
            : base(new ProgressCancelNotifier())
        {
            WithSafeNetwork = false;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        #endregion

        #region Public METHODS

        /// <summary>
        /// Supprime le fichier dans le container, Case insensitive. Avec extension du fichier
        /// </summary>
        public async Task<bool> DeleteFileAsync(string iConnectionString, string iContainerName, string iFullPathFileName)
        {
            if (iConnectionString.IsNullOrEmpty() ||
                iContainerName.IsNullOrEmpty() ||
                iFullPathFileName.IsNullOrEmpty()) throw new ArgumentNullException();

            iFullPathFileName = iFullPathFileName.ToLower();

            if (!Path.HasExtension(iFullPathFileName)) throw new Exception("Le nom du fichier doit contenir l'extension du fichier");

            Func<Task<bool>> action = async () =>
            {
                CloudBlockBlob blockBlob = GetBlobContainer(iConnectionString, iContainerName).GetBlockBlobReference(iFullPathFileName);
                await blockBlob.DeleteAsync();
                return true;
            };
            return await SafeNetwork(action);
        }

        /// <summary>
        /// Sauvegarde un fichier depuis un stream dans le container d'azure, Case insensitive. Ecrase le fichier si déjà existant.
        /// </summary>
        public async Task<Uri> SaveFileAsync(string iConnectionString, string iContainerName, Stream iFileStream, string iFullPathFileName)
        {
            if (iConnectionString.IsNullOrEmpty() ||
                iContainerName.IsNullOrEmpty() ||
                iFullPathFileName.IsNullOrEmpty() ||
                iFileStream == null) throw new ArgumentNullException();

            iFullPathFileName = iFullPathFileName.ToLower();
            if (!Path.HasExtension(iFullPathFileName)) throw new Exception("Le nom du fichier doit contenir l'extension du fichier");

            try
            {
                Func<Task<Uri>> action = async () =>
                {
                    CloudBlockBlob blockBlob = GetBlobContainer(iConnectionString, iContainerName).GetBlockBlobReference(iFullPathFileName);
                    await blockBlob.UploadFromStreamAsync(iFileStream);
                    return blockBlob.Uri;
                };
                return await SafeNetwork(action);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Sauvegarde un fichier depuis tableau byte dans le container d'azure, Case insensitive. Ecrase le fichier si déjà existant.
        /// </summary>
        public async Task<Uri> SaveFileAsync(string iConnectionString, string iContainerName, byte[] iFileBytes, string iFullPathFileName)
        {
            if (iConnectionString.IsNullOrEmpty() ||
                iContainerName.IsNullOrEmpty() ||
                iFullPathFileName.IsNullOrEmpty() ||
                iFileBytes == null) throw new ArgumentNullException();

            iFullPathFileName = iFullPathFileName.ToLower();
            if (!Path.HasExtension(iFullPathFileName)) throw new Exception("Le nom du fichier doit contenir l'extension du fichier");

            try
            {
                Func<Task<Uri>> action = async () =>
                {
                    try
                    {
                        CloudBlockBlob blockBlob = GetBlobContainer(iConnectionString, iContainerName).GetBlockBlobReference(iFullPathFileName);
                        Stream stream = new MemoryStream(iFileBytes);
                        await blockBlob.UploadFromStreamAsync(stream);
                        return blockBlob.Uri;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                };
                return await SafeNetwork(action);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Sauvegarde un fichier depuis tableau byte dans le container d'azure, Case insensitive. Ecrase le fichier si déjà existant.
        /// </summary>
        public async Task<Uri> SaveFile(string iConnectionString, string iContainerName, byte[] iFileBytes, string iFullPathFileName)
        {
            if (iConnectionString.IsNullOrEmpty() ||
                iContainerName.IsNullOrEmpty() ||
                iFullPathFileName.IsNullOrEmpty() ||
                iFileBytes == null) throw new ArgumentNullException();

            iFullPathFileName = iFullPathFileName.ToLower();
            if (!Path.HasExtension(iFullPathFileName)) throw new Exception("Le nom du fichier doit contenir l'extension du fichier");

            try
            {
                try
                {
                    CloudBlockBlob blockBlob = GetBlobContainer(iConnectionString, iContainerName).GetBlockBlobReference(iFullPathFileName);
                    Stream stream = new MemoryStream(iFileBytes);
                    await blockBlob.UploadFromStreamAsync(stream);
                    return blockBlob.Uri;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtient un fichier situé dans Azure, Case insensitive.
        /// </summary>
        public async Task<byte[]> GetFileAsync(string iConnectionString, string iContainerName, string iFullPathFileName)
        {
            if (iConnectionString.IsNullOrEmpty() ||
                iContainerName.IsNullOrEmpty() ||
                iFullPathFileName.IsNullOrEmpty()) throw new ArgumentNullException();

            iFullPathFileName = iFullPathFileName.ToLower();
            if (!Path.HasExtension(iFullPathFileName)) throw new Exception("Le nom du fichier doit contenir l'extension du fichier");

            Func<Task<byte[]>> action = async () =>
            {
                CloudBlockBlob blockBlob = GetBlobContainer(iConnectionString, iContainerName).GetBlockBlobReference(iFullPathFileName);

                try
                {
                    blockBlob.FetchAttributes();
                    long fileByteLength = blockBlob.Properties.Length;
                    byte[] fileContent = new byte[fileByteLength];
                    for (int i = 0; i < fileByteLength; i++)
                        fileContent[i] = 0x20;

                    await blockBlob.DownloadToByteArrayAsync(fileContent, 0);
                    return fileContent;
                }
                catch (StorageException ex)
                {
                    //Cas où le fichier n'existe pas
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.GetType() == typeof(System.Net.WebException))
                        {
                            if ((ex.InnerException as System.Net.WebException).Status == System.Net.WebExceptionStatus.ProtocolError)
                                return null;
                        }
                    }
                    throw;
                }
            };
            return await SafeNetwork(action);
        }

        /// <summary>
        /// Création d'un container
        /// </summary>
        public async Task CreateContainerAsync(string iConnectionString, string iContainerName, BlobContainerPublicAccessEnum iAccess)
        {
            if (iConnectionString.IsNullOrEmpty() ||
                   iContainerName.IsNullOrEmpty()) throw new ArgumentNullException();

            Func<Task> action = async () =>
            {
                CloudBlobContainer container = GetBlobClient(iConnectionString).GetContainerReference(iContainerName);
                BlobContainerPermissions blobPermissions = new BlobContainerPermissions();

                switch (iAccess)
                {
                    case BlobContainerPublicAccessEnum.Blob:
                        blobPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                        break;

                    case BlobContainerPublicAccessEnum.Container:
                        blobPermissions.PublicAccess = BlobContainerPublicAccessType.Container;
                        break;

                    case BlobContainerPublicAccessEnum.Off:
                        blobPermissions.PublicAccess = BlobContainerPublicAccessType.Off;
                        break;

                    default:
                        throw new Exception("non supporté");
                }
                await container.CreateIfNotExistsAsync();
                container.SetPermissions(blobPermissions);
            };
            await SafeNetwork(action);
        }

        /// <summary>
        /// Suppression d'un container
        /// </summary>
        public async Task DeleteContainerAsync(string iConnectionString, string iContainerName)
        {
            if (iConnectionString.IsNullOrEmpty() ||
                   iContainerName.IsNullOrEmpty()) throw new ArgumentNullException();

            Func<Task> action = async () =>
            {
                CloudBlobContainer container = GetBlobClient(iConnectionString).GetContainerReference(iContainerName);
                await container.DeleteIfExistsAsync();
            };
            await SafeNetwork(action);
        }

        #endregion

        #region Private FIELDS

        private const int RETRYWAITINGTIMESECONDS = 10;

        private const int RETRYMAXCOUNT = 3;

        private readonly bool WithSafeNetwork;

        #endregion

        #region Private METHODS

        private CloudBlobContainer GetBlobContainer(string iConnectionString, string iContainerName)
        {
            return GetBlobClient(iConnectionString).GetContainerReference(iContainerName);
        }

        private CloudBlobClient GetBlobClient(string iConnectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(iConnectionString);
            return storageAccount.CreateCloudBlobClient();
        }

        private void SafeNetwork(Action iAction)
        {
            SafeNetwork(() => { iAction(); return 0; });
        }

        private T SafeNetwork<T>(Func<T> iAction)
        {
            int tryCounter = 0;
            while (true)
            {
                Notifier.ThrowIfCancellationRequested();
                try
                {
                    return iAction();
                }
                catch (StorageException ex)
                {
                    if (Notifier.IsCanceled)
                        throw;

                    if (!WithSafeNetwork) throw;

                    MyDebug.PrintError("Le BlobContainer n'est pas connnecté", ex);
                    new SleepWithStopCheck(Notifier).RunSecond(RETRYWAITINGTIMESECONDS);
                    continue;
                }
                catch (Exception)
                {
                    if (Notifier.IsCanceled)
                        throw;

                    if (!WithSafeNetwork) throw;

                    if (tryCounter >= RETRYMAXCOUNT)
                        throw;
                    else
                    {
                        tryCounter++;
                        new SleepWithStopCheck(Notifier).RunSecond(RETRYWAITINGTIMESECONDS);
                    }
                }
            }
        }

        #endregion
    }
}