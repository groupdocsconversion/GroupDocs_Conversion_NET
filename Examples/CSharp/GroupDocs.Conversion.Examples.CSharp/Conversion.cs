﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GroupDocs.Conversion.Config;
using GroupDocs.Conversion.Converter.Option;
using GroupDocs.Conversion.Handler;

namespace GroupDocs.Conversion.Examples.CSharp
{
    public static class Conversion
    {
        public static string inputGUIDFile = "DOCXsample.docx";

        #region Convert to Spreadsheet Document

        /// <summary>
        /// Convert file  Spreadsheet Document formats and get output as file path
        /// </summary> 
        public static void ConvertToSpreadsheetAsPath()
        {
            //ExStart:ConvertToSpreadsheetAsPath
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted spreadsheet documents.
            // Returns paths to the converted spreadsheet documents.

            var convertedDocumentPath = conversionHandler.Convert<string>(Common.inputGUIDFile, new CellsSaveOptions { OutputType = OutputType.String });
            //ExEnd:ConvertToSpreadsheetAsPath
        }

        /// <summary>
        /// Converts documents to Spreadsheet Document formats and outputs the resulting document to a stream
        /// </summary> 
        public static void ConvertToSpreadsheetStream()
        {
            //ExStart:ConvertToSpreadsheetStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted spreadsheet documents. 
            // Returns the converted spreadsheet documents as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, new CellsSaveOptions());
            //ExEnd:ConvertToSpreadsheetStream
        }

        /// <summary>
        /// In advance options example Convert Password Protected file to Spreadsheet Document formats 
        /// </summary>

        public static void ConvertToSpreadsheetAdvanceOptions()
        {
            //ExStart:ConvertToSpreadsheetAdvanceOptions
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            //Set password to unprotect protected document during loading
            LoadOptions loadOptions = new LoadOptions { Password = "secret" };

            // convert file to Xls, starting from page 2 and convert 2 pages
            SaveOptions saveOptions = new CellsSaveOptions
            {
                ConvertFileType = CellsSaveOptions.CellsFileType.Xls,
                PageNumber = 2,
                NumPagesToConvert = 2
            };

            // Unprotect input document, Convert and save spreadsheet documents using advance options.
            // Returns the converted spreadsheet documents as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, loadOptions, saveOptions);
            //ExEnd:ConvertToSpreadsheetAdvanceOptions
        }


        /// <summary>
        /// Converts stream input documents to Spreadsheet Document formats and outputs the resulting document to a file path
        /// </summary>

        public static void ConvertToSpreadsheetFromStreamToFile()
        {
            //ExStart:ConvertToSpreadsheetFromStreamToFile
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted spreadsheet documents as File Path using stream input.
            var convertedDocumentStream = conversionHandler.Convert<String>(fileStream, new CellsSaveOptions { OutputType = OutputType.String, CustomName = Path.GetFileNameWithoutExtension(Common.inputGUIDFile) });

            fileStream.Close();
            //ExEnd:ConvertToSpreadsheetFromStreamToFile
        }

        /// <summary>
        /// Converts stream input documents to Spreadsheet Document formats and outputs the resulting document to a stream
        /// </summary>

        public static void ConvertToSpreadsheetFromStreamToStream()
        {
            //ExStart:ConvertToSpreadsheetFromStreamToStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted spreadsheet documents as IO Stream using stream input.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(fileStream, new CellsSaveOptions());
            fileStream.Close();
            //ExEnd:ConvertToSpreadsheetFromStreamToStream
        }

        #endregion

        #region Convert to Word Processing Document


        /// <summary>
        /// Convert file to Word Processing Document format and get output as file path
        /// </summary> 

        public static void ConvertToWordDocumentAsPath()
        {
            //ExStart:ConvertToWordDocumentAsPath
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted word processing documents.
            // Returns paths to the converted word processing documents.
            var convertedDocumentPath = conversionHandler.Convert<string>(Common.inputGUIDFile, new WordsSaveOptions { OutputType = OutputType.String });
            //ExEnd:ConvertToWordDocumentAsPath
        }


        /// <summary>
        /// Converts documents to Word Processing Document formats and outputs the resulting document to a stream
        /// </summary>

        public static void ConvertToWordDocumentAsStream()
        {
            //ExStart:ConvertToWordDocumentAsStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted word processing documents. 
            // Returns the converted word processing documents as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, new WordsSaveOptions());
            //ExEnd:ConvertToWordDocumentAsStream
        }

        /// <summary>
        /// In advance options example Convert Password Protected file to Word Processing Document format
        /// </summary>

        public static void ConvertToWordDocumentAdvanceOptions()
        {
            //ExStart:ConvertToWordDocumentAdvanceOptions
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            //Set password to unprotect protected document during loading
            LoadOptions loadOptions = new LoadOptions { Password = "secret" };

            // convert file to Doc, starting from page 2 and convert 2 pages,
            SaveOptions saveOptions = new WordsSaveOptions
            {
                ConvertFileType = WordsSaveOptions.WordsFileType.Doc,
                PageNumber = 2,
                NumPagesToConvert = 2,
            };

            // Unprotect input document, Convert and save word processing documents using advance options.
            // Returns the converted word processing documents as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, loadOptions, saveOptions);
            //ExEnd:ConvertToWordDocumentAdvanceOptions
        }

        /// <summary>
        /// Converts stream input documents to Word Processing Document formats and outputs the resulting document to a file path
        /// </summary>
        public static void ConvertToWordFromStreamToFile()
        {
            //ExStart:ConvertToWordFromStreamToFile
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted Word Processing Documents as File Path using stream input.
            var convertedDocumentStream = conversionHandler.Convert<String>(fileStream, new WordsSaveOptions { OutputType = OutputType.String, CustomName = Path.GetFileNameWithoutExtension(Common.inputGUIDFile) });

            fileStream.Close();
            //ExEnd:ConvertToWordFromStreamToFile
        }

        /// <summary>
        /// Converts stream input documents to Word Proccessing Document formats and outputs the resulting document to a stream
        /// </summary>
        public static void ConvertToWordFromStreamToStream()
        {
            //ExStart:ConvertToWordFromStreamToStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted Word Processing Documents as IO Stream using stream input.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(fileStream, new WordsSaveOptions());
            fileStream.Close();
            //ExEnd:ConvertToWordFromStreamToStream
        }

        #endregion

        #region Convert to Pdf

        /// <summary>
        /// Convert file to Pdf format and get output as file path
        /// </summary>

        public static void ConvertToPdfAsPath()
        {
            //ExStart:ConvertToPdfAsPath
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted Pdf documents.
            // Returns paths to the converted Pdf documents.
            var convertedDocumentPath = conversionHandler.Convert<string>(Common.inputGUIDFile, new PdfSaveOptions { OutputType = OutputType.String });
            //ExEnd:ConvertToPdfAsPath
        }

        /// <summary>
        /// Converts documents to Pdf Document formats and outputs the resulting document to a stream
        /// </summary>

        public static void ConvertToPdfAsStream()
        {
            //ExStart:ConvertToPdfAsStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted Pdf documents. 
            // Returns the converted spreadsheet Pdf as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, new PdfSaveOptions());
            //ExEnd:ConvertToPdfAsStream
        }

        /// <summary>
        /// In advance options example Convert Password Protected file to Pdf format
        /// </summary>        
        public static void ConvertToPdfAdvanceOptions()
        {
            //ExStart:ConvertToPdfAdvanceOptions
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            //Set password to unprotect protected document during loading
            LoadOptions loadOptions = new LoadOptions { Password = "secret" };

            // convert starting from page 2 and convert 2 pages,
            // use DPI 300, page width 1024, page height 768
            SaveOptions saveOptions = new PdfSaveOptions
            {
                PageNumber = 2,
                NumPagesToConvert = 2,
                Dpi = 300,
                Width = 1024,
                Height = 768
            };

            // Unprotect input document, Convert and save Pdf documents using advance options.
            // Returns the converted spreadsheet Pdf as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, loadOptions, saveOptions);
            //ExEnd:ConvertToPdfAdvanceOptions
        }

        /// <summary>
        /// Converts stream input documents to pdf Document formats and outputs the resulting document to a file path
        /// </summary>
        public static void ConvertToPdfFromStreamToFile()
        {
            //ExStart:ConverToPdfFromStreamToFile
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted pdf documents as File Path using stream input.
            var convertedDocumentStream = conversionHandler.Convert<String>(fileStream, new PdfSaveOptions { OutputType = OutputType.String, CustomName = Path.GetFileNameWithoutExtension(Common.inputGUIDFile) });

            fileStream.Close();
            //ExEnd:ConverToPdfFromStreamToFile
        }

        /// <summary>
        /// Converts stream input documents to pdf Document formats and outputs the resulting document to a stream
        /// </summary>
        public static void ConvertToPdfFromStreamToStream()
        {
            //ExStart:ConvertToPdfFromStreamToStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted pdf documents as IO Stream using stream input.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(fileStream, new PdfSaveOptions());
            fileStream.Close();
            //ExEnd:ConvertToPdfFromStreamToStream
        }

        #endregion

        #region Convert to Presentation Document

        /// <summary>
        /// Convert file to Presentation Document format and get output as file path
        /// </summary>
        public static void ConvertToPresentationAsPath()
        {
            //ExStart:ConvertToPresentationAsPath
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted presentation documents.
            // Returns paths to the converted presentation documents.
            var convertedDocumentPath = conversionHandler.Convert<string>(Common.inputGUIDFile, new SlidesSaveOptions { OutputType = OutputType.String });
            //ExEnd:ConvertToPresentationAsPath
        }

        /// <summary>
        /// Converts documents to Presentation Document format and outputs the resulting document to a stream
        /// </summary>
        public static void ConvertToPresentationAsStream()
        {
            //ExStart:ConvertToPresentationAsStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // Convert and save converted presentation documents. 
            // Returns the converted presentation documents as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, new SlidesSaveOptions());
            //ExEnd:ConvertToPresentationAsStream
        }

        /// <summary>
        /// In advance options example Convert Password Protected file to Presentation Document format
        /// </summary>

        public static void ConvertToPresentationAdvanceOptions()
        {
            //ExStart:ConvertToPresentationAdvanceOptions
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            //Set password to unprotect protected document during loading
            LoadOptions loadOptions = new LoadOptions { Password = "secret" };

            // convert file to Ppt, starting from page 2 and convert 2 pages,
            // use DPI 300, image width 1024, image height 768
            SaveOptions saveOptions = new SlidesSaveOptions
            {
                ConvertFileType = SlidesSaveOptions.SlidesFileType.Ppt,
                PageNumber = 2,
                NumPagesToConvert = 2,
            };

            // Unprotect input document, Convert and save presentation documents using advance options.
            // Returns the converted presentation documents as IO Stream.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(Common.inputGUIDFile, loadOptions, saveOptions);
            //ExEnd:ConvertToPresentationAdvanceOptions
        }

        /// <summary>
        /// Converts stream input documents to Presentation Document formats and outputs the resulting document to a file path
        /// </summary>
        public static void ConvertToPresentationFromStreamToFile()
        {
            //ExStart:ConvertToPresentationFromStreamToFile
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted presentation documents as File Path using stream input.
            var convertedDocumentStream = conversionHandler.Convert<String>(fileStream, new SlidesSaveOptions { OutputType = OutputType.String, CustomName = Path.GetFileNameWithoutExtension(Common.inputGUIDFile) });

            fileStream.Close();
            //ExEnd:ConvertToPresentationFromStreamToFile
        }

        /// <summary>
        /// Converts stream input documents to Presentation Document formats and outputs the resulting document to a stream
        /// </summary>
        public static void ConvertToPresentationFromStreamToStream()
        {
            //ExStart:ConvertToPresentationFromStreamToStream
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // read input document as a stream
            FileStream fileStream = new FileStream(Path.Combine(Common.storagePath, Common.inputGUIDFile), FileMode.Open, FileAccess.Read);

            // Returns the converted presentation documents as IO Stream using stream input.
            var convertedDocumentStream = conversionHandler.Convert<Stream>(fileStream, new SlidesSaveOptions());
            fileStream.Close();
            //ExEnd:ConvertToPresentationFromStreamToStream
        }

        #endregion

        #region Convert and Get Processing Progress

        /// <summary>
        /// Convert file to Pdf format and get output as file path and get processing progress
        /// </summary>

        //ExStart:ConvertToPdfWithProgressAsPath
        public static void ConvertToPdfWithProgressAsPath()
        {
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            // attach Conversion Progress Handler
            conversionHandler.ConversionProgress += ConversionProgressHandler;

            // Convert and save converted Pdf documents.
            // Returns paths to the converted Pdf documents.
            var convertedDocumentPath = conversionHandler.Convert<string>(Common.inputGUIDFile, new PdfSaveOptions { OutputType = OutputType.String });

            Console.WriteLine("The conversion finished. The result can be located here: {0}. Press <<ENTER>> to exit.", convertedDocumentPath);
            Console.ReadLine();
        }

        private static void ConversionProgressHandler(object sender, ConversionProgressEventArgs args)
        {
            Console.WriteLine("Conversion progress: {0}", args.Progress);
        }

        //ExEnd:ConvertToPdfWithProgressAsPath
        #endregion


        #region Get Available Save Options for a Document by Extenssion

        /// <summary>
        /// Get Available Save Options for a Document by Extenssion
        /// </summary>

        public static void GetAvailableSaveOptionsByExtenssion()
        {
            //ExStart:GetAvailableSaveOptionsByExtenssion
            // Instantiating the conversion handler from custom common class
            ConversionHandler conversionHandler = Common.getConversionHandler();

            var documentExtension = Path.GetExtension(Common.inputGUIDFile).TrimStart('.');
            var availableConversions = conversionHandler.GetSaveOptions(documentExtension);  //returns IDictionary<string, SaveOptions>

            //list all available conversions
            Console.WriteLine("*** Available Save Options ***");
            foreach (var name in availableConversions.Keys)
            {
                Console.WriteLine(name);
            }
            //use prepared save option for ToPdf conversion
            var result = conversionHandler.Convert<string>(Common.inputGUIDFile, availableConversions["pdf"]);
            //ExEnd:ConvertToPdfWithProgressAsPath
        }

        #endregion

    }
}
