using Aspose.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HtmlToPDFController : Controller
    {
        // GET: HtmlToPDF
        public ActionResult Index()
        {
            return View();
        }
        public string RenderViewAsString(string viewName, object model)
        {
            StringWriter stringWriter = new StringWriter();
            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    new ViewDataDictionary(model),
                    new TempDataDictionary(),
                    stringWriter
                    );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult ConvertThisPageToPdf(string GridHtml)
        //{
        //    // get the HTML code of this view
        //    //string htmlToConvert = RenderViewAsString("~/Views/ReporteoClima/EstructuraReporte/no1_Portada.cshtml", null);
        //    string htmlValido = validHTML(GridHtml);

        //    // the base URL to resolve relative images and css
        //    String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
        //    String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - "Home/ConvertThisPageToPdf".Length);

        //    // instantiate the HiQPdf HTML to PDF converter
        //    HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

        //    // hide the button in the created PDF
        //    //htmlToPdfConverter.HiddenHtmlElements = new string[] { "#convertThisPageButtonDiv" };
            
        //    // render the HTML code as PDF in memory
        //    byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlValido, baseUrl);

        //    // send the PDF file to browser
        //    FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
        //    fileResult.FileDownloadName = "ThisMvcViewToPdf.pdf";

        //    return fileResult;
        //}

        /**/
        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string GridHtml)
        //{
        //    //string GridHtml = RenderViewAsString("~/Views/ReporteoClima/EstructuraReporte/no1_Portada.cshtml", null);
        //    Document document = new Document(PageSize.A4);
        //    byte[] result;
        //    string htmlValido = validHTML(GridHtml);

        //    List<string> cssFiles = new List<string>();
        //    cssFiles.Add("~/css/ReporteoClima/custom-administrator.css");
        //    cssFiles.Add("~/css/ReporteoClima/all.min.css");
        //    cssFiles.Add("~/css/ReporteoClima/animate.min.css");
        //    cssFiles.Add("~/css/ReporteoClima/bootstrap.min.css");
        //    cssFiles.Add("~/css/ReporteoClima/lightslider.min.css");
        //    cssFiles.Add("~/css/ReporteoClima/summernote.min.css");
        //    cssFiles.Add("~/css/ReporteoClima/swiper.min.css");
        //    cssFiles.Add("~/css/all.min.css");
        //    cssFiles.Add("~/css/animate.min.css");
        //    cssFiles.Add("~/css/bootstrap.min.css");
        //    cssFiles.Add("~/css/custom-administrator.css");
        //    cssFiles.Add("~/css/swiper.min.css");
        //    cssFiles.Add("~/css/TextRich/font-awesome.min.css");




        //    using (MemoryStream stream = new System.IO.MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(document, stream);
        //        writer.CloseStream = false;
        //        document.Open();

        //        HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
        //        htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

        //        ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
        //        cssFiles.ForEach(i => cssResolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath(i), true));

                

        //        IPipeline pipeline = new CssResolverPipeline(cssResolver,
        //            new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));

        //        XMLWorker worker = new XMLWorker(pipeline, true);
        //        XMLParser xmlParser = new XMLParser(worker);
        //        xmlParser.Parse(new MemoryStream(Encoding.UTF8.GetBytes(htmlValido)));
        //        document.Close();
        //        result = stream.GetBuffer();
        //        //StringReader sr = new StringReader(htmlValido);
        //        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //        //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

        //        //pdfDoc.Open();
        //        //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr, Stream);
        //        //pdfDoc.Close();
        //        //return File(stream.ToArray(), "application/pdf", "Grid.pdf");
        //    }
        //    var pdfDoc = result;
        //    string mimeType = "application/pdf";
        //    Response.AppendHeader("Content-Disposition", "inline; filename=" + "Reporte.pdf");
        //    return File(pdfDoc, mimeType);
        //}

        public static string validHTML(string htmlCode)
        {
            for (int i = 0; i < 50; i++)
            {
                htmlCode = htmlCode.Replace("<br>", "<br />");
                //<img src="~/img/ReporteoClima/clima-portada.jpg" class="img-fluid img-minwhite" />
                htmlCode = htmlCode.Replace("data-close=\"\"", "/");
            }
            return htmlCode;
        }


        /*3er opcion*/
        //public FileResult getPDF(string GridHtml)
        //{
        //    HtmlLoadOptions htmloptions = new HtmlLoadOptions();
          
        //    // Cargar archivo HTML
        //    Aspose.Pdf.Document doc = new Aspose.Pdf.Document(" HTML-Document.html ", htmloptions);
        //    // Convertir archivo HTML a PDF
        //    doc.Save(" HTML a PDF.pdf ");
        //    return File(doc);
        //}

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConvertHTMLtoPDF(string GridHtml)
        {
            string validHtmsl = validHTML(GridHtml);
            string _dataDir = @"\\10.5.2.101\Demos\dashboard-sd2\demoexportPDF\";
            //HtmlLoadOptions options = new HtmlLoadOptions();
            ////Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document("~/Views/ReporteoClima/EstructuraReporte/no1_Portada.cshtml", options);
            //Aspose.Pdf.HtmlFragment html = new HtmlFragment(validHtmsl);
            //html.IsInNewPage = true;

            //Aspose.Pdf.Document doc = new Aspose.Pdf.Document()
            ////pdfDocument.Save(_dataDir + "html_test.PDF");
            Aspose.Pdf.Document pdf = new Aspose.Pdf.Document();
            Aspose.Pdf.Page pagina = pdf.Pages.Add();
            var html = new HtmlFragment(validHtmsl);

            pagina.Paragraphs.Add(html);
            pdf.Save(_dataDir + "Demo01.pdf", SaveFormat.Pdf);

            return Json("");
        }

        /*pptx*/
        public ActionResult data()
        {
            // Load the HTML file to be converted
            var converter = new GroupDocs.Conversion.Converter("My File.html");
            var f = new GroupDocs.Conversion.License();
            // Set the convert options for PPT format
            var convertOptions = converter.GetPossibleConversions()["ppt"].ConvertOptions;
            // Convert to PPT format
            converter.Convert("Saved File.ppt", convertOptions);
            return Json("");
        }

        public ActionResult toppt()
        {
            string usuario = Convert.ToString(Session["AdminLog"]);
            // Load PDF document
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(@"\\10.5.2.101\Demos\dashboard-sd2\demoexportPDF\example.pdf");
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pptxOptions.SlidesAsImages = false; pptxOptions.SeparateImages = true; 
            // Save output file
            pdfDocument.Save("\\\\10.5.2.101\\Demos\\dashboard-sd2\\demoexportPDF\\PDF to PPT.ppt", pptxOptions);



            // Load PDF document
            Aspose.Pdf.Document pdfDocument2 = new Aspose.Pdf.Document(@"\\10.5.2.101\Demos\dashboard-sd2\demoexportPDF\example.pdf");
            // Initialize ExcelSaveOptions
            ExcelSaveOptions options = new ExcelSaveOptions();
            // Set output format
            options.Format = ExcelSaveOptions.ExcelFormat.XLSX;
            // Save output file
            pdfDocument2.Save("\\\\10.5.2.101\\Demos\\dashboard-sd2\\demoexportPDF\\Excel.xlsx", options);

            return Json("Saved", JsonRequestBehavior.AllowGet);
        }


    }
}