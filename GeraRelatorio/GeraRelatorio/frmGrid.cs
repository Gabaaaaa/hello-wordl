using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace GeraRelatorio
{
	public partial class frmGrid : Form
	{
		public frmGrid()
		{
			InitializeComponent();
		}

		private void FrmGrid_Load(object sender, EventArgs e)
		{

		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Gerando um arquivo Excel, isso pode demorar alguns minutos");

			Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
			Workbook wb = Excel.Workbooks.Add(XlSheetType.xlWorksheet);
			Worksheet ws = (Worksheet)Excel.ActiveSheet;
			Excel.Visible = false;

			Excel.Columns.ColumnWidth = 15;

			ws.Name = lblConexao.Text;

			for (int i = 1; i < grdRelatorio.Columns.Count + 1; i++)
			{
				ws.Cells[1, i] = grdRelatorio.Columns[i - 1].HeaderText;
			}

			for (int i = 0; i < grdRelatorio.Rows.Count - 1; i++)
			{
				for (int j = 0; j < grdRelatorio.Columns.Count ; j++)
				{
					ws.Cells[i + 2, j + 1] = grdRelatorio.Rows[i].Cells[j].Value.ToString();
				}
			}


			string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			if (!Directory.Exists(path + @"\Arquivos_Excel"))
			{
				Directory.CreateDirectory(path + @"\Arquivos_Excel");
			}

			Excel.ActiveWorkbook.SaveAs(string.Format(path + @"\Arquivos_Excel\" + lblConexao.Text + DateTime.Now.ToString("yyyyMMddHHmmss")));
			Excel.ActiveWorkbook.Saved = true;
			MessageBox.Show("Arquivo Excel criado com sucesso");
			Excel.Quit();
		}

		private void btnPdf_Click(object sender, EventArgs e)
		{
			PdfPTable pdfTable = new PdfPTable(grdRelatorio.ColumnCount);
			pdfTable.DefaultCell.Padding = 15;
			pdfTable.WidthPercentage = 100;
			pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
			pdfTable.DefaultCell.BorderWidth = 1;
			//var font = iTextSharp.text.FontFactory.GetFont("Arial", 14);

			var Pfont = iTextSharp.text.FontFactory.GetFont("Arial", 32);

			



			foreach (DataGridViewColumn column in grdRelatorio.Columns)
			{
				PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
				cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
				pdfTable.AddCell(cell);
			}

			foreach (DataGridViewRow row in grdRelatorio.Rows)
			{
				foreach (DataGridViewCell cell in row.Cells)
				{
					try
					{
						pdfTable.AddCell(cell.Value.ToString());
					}
					catch { }

				}
			}

			/*string caminho = @"C:\Arquivos Excel\";
			if (Directory.Exists(caminho))
			{
				Directory.CreateDirectory(caminho);
			}*/

			string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			if (!Directory.Exists(path + @"\Arquivos_Pdf"))
			{
				Directory.CreateDirectory(path + @"\Arquivos_Pdf");
			}


			using (FileStream stream = new FileStream(path + @"\Arquivos_Pdf\" + (lblConexao.Text + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"), FileMode.Create))
			{
				Document pdfDoc = new Document(PageSize.A1, 10f, 10f, 10f, 0f);
				PdfWriter.GetInstance(pdfDoc, stream);
				pdfDoc.Open();

				Paragraph paragrafo = new Paragraph(lblConexao.Text.ToUpper(),Pfont);
				paragrafo.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
				pdfDoc.Add(paragrafo);

				pdfDoc.Add(pdfTable);
				MessageBox.Show("Arquivo Pdf criado com sucesso");
				pdfDoc.Close();
				stream.Close();
			}
		}

		private void grdRelatorio_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
