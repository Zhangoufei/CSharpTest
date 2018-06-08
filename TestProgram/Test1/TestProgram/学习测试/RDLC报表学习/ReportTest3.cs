using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试.RDLC报表学习
{
    public partial class ReportTest3 : Form
    {
        public ReportTest3()
        {
            InitializeComponent();
        }

        private void ReportTest3_Load(object sender, EventArgs e)
        {
            using (ReportTest3 demo = new ReportTest3())
            {
                demo.Run();
            }
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private DataTable LoadSalesData()
        {
            // Create a new DataSet and read sales data file 
            //    data.xml into the first DataTable.
           // DataSet dataSet = new DataSet();
          //  dataSet.ReadXml(@"..\..\Report1.rdlc.xml");
            DataTable table = new DataTable();
            table.TableName = "Sales";
            table.Columns.Add("Value1");
            table.Columns.Add("Value2");

            DataRow rows = table.NewRow();
            rows["Value1"] = "3333";
            rows["Value2"] = "4444";
            table.Rows.Add(rows);
            return table;
        }
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        private void Run()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\Report1.rdlc";
            report.DataSources.Add(
               new ReportDataSource("Sales", LoadSalesData()));
            Export(report);
            Print();
        }
       
      
    }
}
