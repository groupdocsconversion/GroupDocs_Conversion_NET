using GroupDocs.Conversion.Contracts;
using GroupDocs.Conversion.FileTypes;
using GroupDocs.Conversion.MVC.Products.Conversion.Entity.Web.Request;
using GroupDocs.Conversion.Options.Convert;
using System.Collections.Generic;
using System.IO;

namespace GroupDocs.Conversion.MVC.Products.Conversion.Manager
{
    public static class ConversionManager
    {
        public static void Convert(ConversionPostedData postedData, Common.Config.GlobalConfiguration globalConfiguration, List<string> supportedImageFormats)
        {
            string destinationType = postedData.GetDestinationType();
            string documentGuid = postedData.guid;
            string filesDirectory = globalConfiguration.GetConversionConfiguration().GetResultDirectory();
            string outputFile = Path.Combine(filesDirectory, Path.GetFileNameWithoutExtension(documentGuid) + "." + postedData.GetDestinationType());
            string destDocumentType = supportedImageFormats.Contains(Path.GetExtension("." + destinationType).ToLowerInvariant()) ? "image" : postedData.GetDestDocumentType();
            string fileNameWoExt = Path.GetFileNameWithoutExtension(postedData.guid);

            using (Converter converter = new Converter(postedData.guid))
            {
                var convertOptions = GetConvertOptions(destDocumentType, destinationType);

                var documentInfo = converter.GetDocumentInfo();

                if (convertOptions is ImageConvertOptions)
                {
                    string outputFileTemplate = Path.Combine(filesDirectory, fileNameWoExt + "-{0}." + destinationType);

                    if ((documentInfo is SpreadsheetDocumentInfo && ((SpreadsheetDocumentInfo)documentInfo).WorksheetsCount == 1)
                        || documentInfo.PagesCount == 1)
                    {
                        outputFileTemplate = Path.Combine(filesDirectory, fileNameWoExt + "." + destinationType);
                    }

                    SavePageStream getPageStream = page => new FileStream(string.Format(outputFileTemplate, page), FileMode.Create);
                    converter.Convert(getPageStream, convertOptions);
                }
                else
                {
                    converter.Convert(outputFile, convertOptions);
                }
            }
        }

        private static ConvertOptions GetConvertOptions(string destDocumentType, string destinationType)
        {
            ConvertOptions convertOptions;

            switch (destDocumentType)
            {
                case "Portable Document Format":
                    convertOptions = new PdfConvertOptions();
                    break;
                case "Microsoft Word":
                    convertOptions = new WordProcessingConvertOptions();
                    break;
                case "Microsoft PowerPoint":
                    convertOptions = new PresentationConvertOptions();
                    break;
                case "image":
                    convertOptions = new ImageConvertOptions();
                    break;
                case "Comma-Separated Values":
                    convertOptions = new SpreadsheetConvertOptions();
                    break;
                case "Microsoft Excel":
                    convertOptions = new SpreadsheetConvertOptions();
                    break;
                default:
                    convertOptions = new WordProcessingConvertOptions();
                    break;
            }

            convertOptions.Format = FileType.FromExtension(destinationType);

            return convertOptions;
        }
    }
}