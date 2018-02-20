using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Library.Tools.Network;

namespace Library.Azure.Tests
{
    [TestClass]
    public class BlobContainerTest
    {
        #region Public METHODS

        [TestMethod]
        public async Task SaveImage_GetImage_DeleteFile()
        {
            try
            {


                var notifier = new ProgressCancelNotifier();
                string connectionString = ConfigurationManager.AppSettings["AzureStorageConnectionString"];
                string containerName = Guid.NewGuid().ToString();

                var blobContainer = new BlobContainer(notifier, false);
                await blobContainer.CreateContainerAsync(connectionString, containerName, BlobContainerPublicAccessEnum.Off);

                var theImage = Image.FromFile(ImagePath1);
                theImage = theImage.ScaleImage(500, 500);

                string fileName = "imagetest.jpeg";
                string fileName2 = "imagetest2.jpeg";
                string fakeName = "imagetest241.jpeg";

                //save
                if (await blobContainer.SaveFileAsync(connectionString, containerName, theImage.ToStream(), fileName) == null) throw new Exception();
                if (await blobContainer.SaveFileAsync(connectionString, containerName, theImage.ToStream(), fileName2) == null) throw new Exception();

                //get
                var getImage = await blobContainer.GetFileAsync(connectionString, containerName, fileName);
                if (getImage == null) throw new Exception();

                var getImage2 = await blobContainer.GetFileAsync(connectionString, containerName, fakeName);
                if (getImage2 != null) throw new Exception();

                //delete
                if (!await blobContainer.DeleteFileAsync(connectionString, containerName, fileName)) throw new Exception();
                if (!await blobContainer.DeleteFileAsync(connectionString, containerName, fileName2)) throw new Exception();

                await blobContainer.DeleteContainerAsync(connectionString, containerName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [TestMethod]
        public async Task SavePDF_GetImage_DeleteFile()
        {
            var notifier = new ProgressCancelNotifier();
            string connectionString = ConfigurationManager.AppSettings["AzureStorageConnectionString"];
            string containerName = Guid.NewGuid().ToString();

            var blobContainer = new BlobContainer(notifier, false);
            await blobContainer.CreateContainerAsync(connectionString, containerName, BlobContainerPublicAccessEnum.Off);

            var theFile = File.ReadAllBytes(pdfPath);

            string fileName = "pdf1.pdf";
            string fileName2 = "pdf2.pdf";
            string fakeName = "pdf1001.pdf";

            //save
            if (await blobContainer.SaveFileAsync(connectionString, containerName, theFile, fileName) == null) throw new Exception();
            if (await blobContainer.SaveFileAsync(connectionString, containerName, theFile, fileName2) == null) throw new Exception();

            //get
            var getImage = await blobContainer.GetFileAsync(connectionString, containerName, fileName);
            if (getImage == null) throw new Exception();

            var getImage2 = await blobContainer.GetFileAsync(connectionString, containerName, fakeName);
            if (getImage2 != null) throw new Exception();

            //delete
            if (!await blobContainer.DeleteFileAsync(connectionString, containerName, fileName)) throw new Exception();
            if (!await blobContainer.DeleteFileAsync(connectionString, containerName, fileName2)) throw new Exception();

            await blobContainer.DeleteContainerAsync(connectionString, containerName);
        }

        [TestMethod]
        public async Task CreateContainer()
        {
            var notifier = new ProgressCancelNotifier();
            string connectionString = ConfigurationManager.AppSettings["AzureStorageConnectionString"];
            string privateContainer = Guid.NewGuid().ToString();
            string publicContainer = Guid.NewGuid().ToString();
            var blobContainer = new BlobContainer(notifier, false);

            await blobContainer.CreateContainerAsync(connectionString, privateContainer, BlobContainerPublicAccessEnum.Off);

            var theImage = Image.FromFile(ImagePath1);
            Uri thePrivateUrl = await blobContainer.SaveFileAsync(connectionString, privateContainer, theImage.ToStream(), Guid.NewGuid().ToString() + ".jpg");
            try
            {
                using (var webClient = new WebClient())
                {
                    Stream stream = webClient.OpenRead(thePrivateUrl);
                    Image.FromStream(stream);
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
            }

            await blobContainer.CreateContainerAsync(connectionString, publicContainer, BlobContainerPublicAccessEnum.Blob);
            var theImage2 = Image.FromFile(ImagePath1);
            Uri thePublicUrl = await blobContainer.SaveFileAsync(connectionString, publicContainer, theImage.ToStream(), Guid.NewGuid().ToString() + ".jpg");

            using (var webClient = new WebClient())
            {
                Stream stream = webClient.OpenRead(thePublicUrl);
                var img = Image.FromStream(stream);
                if (img == null) throw new Exception();
            }
        }

        //[TestMethod]
        //public async Task SafeNetwork()
        //{
        //    var notifier = new ProgressCancelNotifier();

        //    string connectionString = ConfigurationManager.AppSettings["AzureStorageConnectionString"];
        //    string containerName = Guid.NewGuid().ToString();

        //    var blobContainer = new BlobContainer(notifier, false);
        //    await blobContainer.CreateContainerAsync(connectionString, containerName, BlobContainerPublicAccessEnum.Off);

        //    var theImage = Image.FromFile(ImagePath1);
        //    theImage = theImage.ScaleImage(500, 500);

        //    //sans coupure
        //    string fileName = "imagetest.jpeg";
        //    string fakeName = "imagetest241.jpeg";
        //    if (await blobContainer.SaveFileAsync(connectionString, containerName, theImage.ToStream(), fileName) == null) throw new Exception();

        //    var TheNetworkHelper = new NetworkHelper();

        //    Func<Task> saveAction = async () =>
        //    {
        //        if (await blobContainer.SaveFileAsync(connectionString, containerName, theImage.ToStream(), fileName) == null) throw new Exception();
        //    };

        //    Func<Task> getAction = async () =>
        //    {
        //        var getImage = await blobContainer.GetFileAsync(connectionString, containerName, fileName);
        //        if (getImage == null) throw new Exception();
        //    };

        //    Func<Task> getAction2 = async () =>
        //    {
        //        var getImage2 = await blobContainer.GetFileAsync(connectionString, containerName, fakeName);
        //        if (getImage2 != null) throw new Exception();
        //    };

        //    Func<Task> deleteAction = async () =>
        //    {
        //        if (!await blobContainer.DeleteFileAsync(connectionString, containerName, fileName)) throw new Exception();
        //    };

        //    Task disconnectionTask;
        //    disconnectionTask = TheNetworkHelper.DisconnectionTemporaryAsync(DISCONNECTTIMESECOND);
        //    Task saveTask = Task.Run(() => { saveAction(); });
        //    await Task.WhenAll(disconnectionTask, saveTask);

        //    MyDebug.PrintInformation("Save ok");

        //    disconnectionTask = TheNetworkHelper.DisconnectionTemporaryAsync(DISCONNECTTIMESECOND);
        //    Task getTask = Task.Run(() => { getAction(); });
        //    await Task.WhenAll(disconnectionTask, getTask);

        //    MyDebug.PrintInformation("get ok");

        //    disconnectionTask = TheNetworkHelper.DisconnectionTemporaryAsync(DISCONNECTTIMESECOND);
        //    Task getTask2 = Task.Run(() => { getAction2(); });
        //    await Task.WhenAll(disconnectionTask, getTask2);

        //    MyDebug.PrintInformation("get fake ok");

        //    disconnectionTask = TheNetworkHelper.DisconnectionTemporaryAsync(DISCONNECTTIMESECOND);
        //    Task deleteTask = Task.Run(() => { deleteAction(); });
        //    await Task.WhenAll(disconnectionTask, deleteTask);

        //    MyDebug.PrintInformation("delete ok");
        //}

        #endregion

        //[TestMethod]
        //public void SaveParallele()
        //{
        //    var notifier = new ProgressCancelNotifier();
        //    string connectionString = ConfigurationManager.AppSettings["AzureStorageConnectionString"];
        //    string containerName = Guid.NewGuid().ToString();

        //    var blobContainer = new BlobContainer(notifier, false);
        //    blobContainer.CreateContainer(connectionString, containerName, BlobContainerPublicAccessEnum.Off);

        //    var theImage = Image.FromFile(ImagePath1);
        //    theImage = theImage.ScaleImage(500, 500);

        //    var streamLocker = new object();

        //    using (var timer = new MyTimer(true))
        //    {
        //        Parallel.For(0, 10, new ParallelOptions { MaxDegreeOfParallelism = 100 }, i =>
        //        {
        //            Stream thestream;
        //            lock (streamLocker)
        //            {
        //                thestream = theImage.ToStream();
        //            }
        //            var blobContainer2 = new BlobContainer(notifier, false);
        //            blobContainer2.SaveFile(connectionString, containerName, thestream, Guid.NewGuid().ToString() + ".jpeg");

        //            MyDebug.PrintInformation("ajout");
        //        });
        //    }
        //    MyDebug.PrintInformation("fin");
        //}

        #region Private FIELDS

        private const int DISCONNECTTIMESECOND = 30;
        private string ImagePath1 = @"E:\Développements\Logiciels\Library\_UnitTestData\o1.jpeg";
        private string pdfPath = @"E:\Développements\Logiciels\Library\_UnitTestData\report1.pdf";

        #endregion
    }
}