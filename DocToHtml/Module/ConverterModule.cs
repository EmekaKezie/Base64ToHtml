using DocToHtml.Model;
//using Syncfusion.DocIO;
//using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SautinSoft.Document;
using Aspose.Words;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;

namespace DocToHtml.Module
{
    public class ConverterModule
    {
        //public static async Task<List<string>> Converter(Converter m)
        public static async Task<List<string>> Converter()
        {
            List<string> list = null;
            List<Stream> streams = null;
            string items = String.Empty;
            try
            {

                string filePath = @"C:\Users\emeka\Desktop\docfile.docx";
                string folderPath = @"docx";
                DocumentCore dc = DocumentCore.Load(filePath);
                DocumentPaginator dp = dc.GetPaginator();
                int pageCount = dp.Pages.Count;

                streams = new List<Stream>();
                list = new List<string>();
               
                for (int i = 0; i < pageCount; i++)
                {
                    DocumentPage page = dp.Pages[i];
                    MemoryStream outputStream = new MemoryStream();
                    var html = string.Empty;
                    page.Save(outputStream, SautinSoft.Document.SaveOptions.DocxDefault);
                    streams.Add(outputStream);
                }

                foreach (var item in streams)
                {
                    using (WordDocument mainDocument = new WordDocument(item, FormatType.Automatic))
                    {
                        string _html = null;
                        MemoryStream stream = new MemoryStream();

                        //Sets the style sheet type
                        mainDocument.SaveOptions.HtmlExportOmitXmlDeclaration = true;
                        mainDocument.SaveOptions.HtmlExportHeadersFooters = true;
                        mainDocument.Save(stream, FormatType.Html);

                        stream.Position = 0;
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            _html = reader.ReadToEnd();
                        }

                        list.Add(_html);
                    }
                }

               // if (!string.IsNullOrEmpty(m.Base64))
                //{
                    //var bytes = await Base64ToByteArray(m.Base64);
                    //Stream _contents = new MemoryStream(bytes);

                    //DocumentCore dc = DocumentCore.Load(_contents, LoadOptions.DocxDefault);
                    //DocumentPaginator dp = dc.GetPaginator();
                    //int pageCount = dp.Pages.Count;

                    //list = new List<string>();
                    //for (int i = 0; i < pageCount; i++)
                    //{
                    //    DocumentPage page = dp.Pages[i];

                    //    list.Add(page.Content.ToString());
                    //}



                    //using (WordDocument mainDocument = new WordDocument(_contents, FormatType.Docx))
                    //{
                    //    //string rtf = null;
                    //    MemoryStream stream = new MemoryStream();
                    //    mainDocument.SaveOptions.HtmlExportOmitXmlDeclaration = true;
                    //    mainDocument.SaveOptions.HtmlExportHeadersFooters = true;
                    //    mainDocument.SaveOptions.OptimizeRtfFileSize = true;

                    //    mainDocument.Save(stream, FormatType.Docx);


                    //    //DocumentCore dc = DocumentCore.Load(_contents, LoadOptions.DocxDefault);
                    //    //DocumentPaginator dp = dc.GetPaginator();
                    //    //int pageCount = dp.Pages.Count;
                    //    //for (int i = 0; i < pageCount; i++)
                    //    //{
                    //    //    DocumentPage page = dp.Pages[i];
                    //    //}

                    //    //stream.Position = 0;
                    //    //using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    //    //{
                    //    //    rtf = reader.ReadToEnd();
                    //    //}

                    //    //SautinSoft.RtfToHtml r = new SautinSoft.RtfToHtml();



                    //    //string rtfString = File.ReadAllText(rtf);

                    //    // Let's store all images inside the HTML document.
                    //    //r.ImageStyle.IncludeImageInHtml = true;

                    //    //string htmlString = r.ConvertString(rtf);

                    //    //list = htmlString;
                    //}
                //}




            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }






        public static bool CheckFileType(string base64String)
        {
            bool withType;

            try
            {
                if (base64String.Contains(FileTypes.IMAGE))
                    withType = true;
                else if (base64String.Contains(FileTypes.AUDIO))
                    withType = true;
                else if (base64String.Contains(FileTypes.VIDEO))
                    withType = true;
                else if (base64String.Contains(FileTypes.DOCX))
                    withType = true;
                else
                    withType = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return withType;
        }


        public static async Task<byte[]> Base64ToByteArray(string base64String)
        {
            byte[] converter;

            try
            {
                if (CheckFileType(base64String))
                {
                    string[] split = base64String.Split(',');
                    if (split.Length > 1)
                    {
                        split = split.Skip(1).ToArray();
                    }
                    base64String = string.Join(",", split);
                }

                converter = Convert.FromBase64String(base64String);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return converter;
        }
    }
}
