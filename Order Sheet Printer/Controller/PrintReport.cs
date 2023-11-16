using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Controller
{
    public static class PrintReport
    {
        private static int m_currentPageIndex;
        private static IList<Stream> m_streams;

        public static Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void Export(LocalReport report, bool print, decimal width, decimal height, string impressora)
        {
            string deviceInfo = $@"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{width}mm</PageWidth>
                <PageHeight>{height}mm</PageHeight>
                <MarginTop>0mm</MarginTop>
                <MarginLeft>0mm</MarginLeft>
                <MarginRight>0mm</MarginRight>
                <MarginBottom>0mm</MarginBottom>
            </DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();

            report.Render("Image", deviceInfo, CreateStream,
              out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
                Print(impressora);
        }

        // Handler for PrintPageEvents
        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
            Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width, ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void Print(string impressora)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");


            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            pd.PrinterSettings.PrinterName = impressora;
            pd.DefaultPageSettings.Landscape = false;
            pd.PrintController = new StandardPrintController();
            m_currentPageIndex = 0;
            pd.Print();
        }

        public static void PrintToPrinter(this LocalReport report, decimal width, decimal height, string impressora)
        {
            Export(report, true, width, height, impressora);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();

                m_streams = null;
            }
        }
    }
}
