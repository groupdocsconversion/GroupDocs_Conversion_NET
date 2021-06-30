using GroupDocs.Conversion.Contracts;
using GroupDocs.Conversion.MVC.Products.Common.Entity.Web;
using GroupDocs.Conversion.MVC.Products.Common.Resources;
using GroupDocs.Conversion.MVC.Products.Common.Util.Comparator;
using GroupDocs.Conversion.MVC.Products.Conversion.Config;
using GroupDocs.Conversion.MVC.Products.Conversion.Entity.Web.Request;
using GroupDocs.Conversion.MVC.Products.Conversion.Entity.Web.Response;
using GroupDocs.Conversion.MVC.Products.Conversion.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GroupDocs.Conversion.MVC.Products.Conversion.Controllers
{
    /// <summary>
    /// ViewerApiController
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConversionApiController : ApiController
    {
        private readonly Common.Config.GlobalConfiguration GlobalConfiguration = new Common.Config.GlobalConfiguration();       
        private readonly List<string> SupportedImageFormats = new List<string> { ".jp2", ".ico", ".psd", ".svg", ".bmp", ".jpeg", ".jpg", ".tiff", ".tif", ".png", ".gif", ".emf", ".wmf", ".dwg", ".dicom", ".dxf", ".jpe", ".jfif", ".webp" };

        /// <summary>
        /// Load Conversion configuration
        /// </summary>
        /// <returns>Conversion configuration</returns>
        [HttpGet]
        [Route("loadConfig")]
        public ConversionConfiguration LoadConfig()
        {
            return GlobalConfiguration.GetConversionConfiguration();
        }

        /// <summary>
        /// Get all files and directories from storage
        /// </summary>
        /// <param name="postedData">Post data</param>
        /// <returns>List of files and directories</returns>
        [HttpPost]
        [Route("loadFileTree")]
        public HttpResponseMessage loadFileTree(PostedDataEntity postedData)
        {
            // get request body
            string relDirPath = postedData.path;
            // get file list from storage path
            try
            {
                // get all the files from a directory
                if (string.IsNullOrEmpty(relDirPath))
                {
                    relDirPath = GlobalConfiguration.GetConversionConfiguration().GetFilesDirectory();
                }
                else
                {
                    relDirPath = Path.Combine(GlobalConfiguration.GetConversionConfiguration().GetFilesDirectory(), relDirPath);
                }

                List<string> allFiles = new List<string>(Directory.GetFiles(relDirPath));
                allFiles.AddRange(Directory.GetDirectories(relDirPath));
                List<ConversionTypesEntity> fileList = new List<ConversionTypesEntity>();

                allFiles.Sort(new FileNameComparator());
                allFiles.Sort(new FileTypeComparator());

                foreach (string file in allFiles)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    // check if current file/folder is hidden
                    if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ||
                        Path.GetFileName(file).Equals(Path.GetFileName(GlobalConfiguration.GetConversionConfiguration().GetFilesDirectory())) ||
                        Path.GetFileName(file).Equals(Path.GetFileName(GlobalConfiguration.GetConversionConfiguration().GetResultDirectory())) ||
                        Path.GetFileName(file).Equals(".gitkeep"))
                    {
                        // ignore current file and skip to next one
                        continue;
                    }
                    else
                    {
                        ConversionTypesEntity fileDescription = new ConversionTypesEntity
                        {
                            conversionTypes = new List<string>(),
                            guid = Path.GetFullPath(file),
                            name = Path.GetFileName(file),
                            // set is directory true/false
                            isDirectory = fileInfo.Attributes.HasFlag(FileAttributes.Directory)
                        };
                        // set file size
                        if (!fileDescription.isDirectory)
                        {
                            fileDescription.size = fileInfo.Length;
                        }

                        string documentExtension = Path.GetExtension(fileDescription.name).TrimStart('.');
                        if (!string.IsNullOrEmpty(documentExtension))
                        {
                            string[] availableConversions = GetPosibleConversions(documentExtension);
                            //list all available conversions
                            foreach (string name in availableConversions)
                            {
                                fileDescription.conversionTypes.Add(name);
                            }
                        }
                        // add object to array list
                        fileList.Add(fileDescription);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, fileList);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Resources.GenerateException(ex));
            }
        }

        private static string[] GetPosibleConversions(string documentExtension)
        {
            PossibleConversions conversions = Converter.GetPossibleConversions(documentExtension);
            var conversionsFormats = new List<string>();

            foreach (var conversion in conversions.All)
            {
                conversionsFormats.Add(conversion.Format.Extension);
            }

            conversionsFormats.Sort();

            return conversionsFormats.ToArray();
        }

        /// <summary>
        /// Upload document
        /// </summary>
        /// <param name="postedData">Post data</param>
        /// <returns>Uploaded document object</returns>
        [HttpPost]
        [Route("uploadDocument")]
        public HttpResponseMessage UploadDocument()
        {
            try
            {
                string url = HttpContext.Current.Request.Form["url"];
                // get documents storage path
                string documentStoragePath = GlobalConfiguration.GetConversionConfiguration().GetFilesDirectory();
                bool rewrite = bool.Parse(HttpContext.Current.Request.Form["rewrite"]);
                string fileSavePath = "";
                if (string.IsNullOrEmpty(url))
                {
                    if (HttpContext.Current.Request.Files.AllKeys != null)
                    {
                        // Get the uploaded document from the Files collection
                        var httpPostedFile = HttpContext.Current.Request.Files["file"];
                        if (httpPostedFile != null)
                        {
                            if (rewrite)
                            {
                                // Get the complete file path
                                fileSavePath = Path.Combine(documentStoragePath, httpPostedFile.FileName);
                            }
                            else
                            {
                                fileSavePath = Resources.GetFreeFileName(documentStoragePath, httpPostedFile.FileName);
                            }

                            // Save the uploaded file to "UploadedFiles" folder
                            httpPostedFile.SaveAs(fileSavePath);
                        }
                    }
                }
                else
                {
                    using (WebClient client = new WebClient())
                    {
                        // get file name from the URL
                        Uri uri = new Uri(url);
                        string fileName = Path.GetFileName(uri.LocalPath);
                        if (rewrite)
                        {
                            // Get the complete file path
                            fileSavePath = Path.Combine(documentStoragePath, fileName);
                        }
                        else
                        {
                            fileSavePath = Resources.GetFreeFileName(documentStoragePath, fileName);
                        }
                        // Download the Web resource and save it into the current filesystem folder.
                        client.DownloadFile(url, fileSavePath);
                    }
                }
                UploadedDocumentEntity uploadedDocument = new UploadedDocumentEntity();
                uploadedDocument.guid = fileSavePath;
                return Request.CreateResponse(HttpStatusCode.OK, uploadedDocument);
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Resources.GenerateException(ex));
            }
        }

        /// <summary>
        /// Get all files and directories from storage
        /// </summary>
        /// <param name="postedData">Post data</param>
        /// <returns>List of files and directories</returns>
        [HttpPost]
        [Route("convert")]
        public HttpResponseMessage Convert(ConversionPostedData postedData)
        {
            try
            {
                ConversionManager.Convert(postedData, GlobalConfiguration, SupportedImageFormats);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Resources.GenerateException(ex));
            }
        }

        /// <summary>
        /// Download curerntly viewed document
        /// </summary>
        /// <param name="path">Path of the document to download</param>
        /// <returns>Document stream as attachement</returns>
        [HttpGet]
        [Route("downloadDocument")]
        public HttpResponseMessage DownloadDocument(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string destinationPath = Path.Combine(GlobalConfiguration.GetConversionConfiguration().GetResultDirectory(), path);


                if (SupportedImageFormats.Contains(Path.GetExtension(destinationPath)))
                {
                    string zipName = Path.GetFileNameWithoutExtension(destinationPath) + ".zip";
                    string zipPath = Path.Combine(GlobalConfiguration.GetConversionConfiguration().GetResultDirectory(), zipName);
                    string[] files = Directory.GetFiles(GlobalConfiguration.GetConversionConfiguration().GetResultDirectory(),
                        Path.GetFileNameWithoutExtension(destinationPath) + "*" + Path.GetExtension(destinationPath));
                    if (File.Exists(zipPath))
                    {
                        File.Delete(zipPath);
                    }
                    using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                    {
                        foreach (string file in files)
                        {
                            zip.CreateEntryFromFile(file, Path.GetFileName(file));
                        }
                    }
                    destinationPath = zipPath;
                }
                if (File.Exists(destinationPath))
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = new FileStream(destinationPath, FileMode.Open);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(destinationPath);
                    return response;
                }
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
