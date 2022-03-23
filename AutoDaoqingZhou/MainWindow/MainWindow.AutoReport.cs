using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;
using AutoDaoqingZhou.Models;
using AutoDaoqingZhou.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoDaoqingZhou
{
	public partial class MainWindow : Window
	{
		private async void AutoProcessing_Click(object sender, RoutedEventArgs e)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			FileInfo fileInfo = new FileInfo(FileService.GetFileName(App.InputFolder, App.DataProcessingFile, ""));   //监测数据读取
			var testingList = new List<Testing>();
			string dataProcessingSheetName = "Sheet1";
			using (var package = new ExcelPackage(fileInfo))
			{
				var sheet = package.Workbook.Worksheets[dataProcessingSheetName];
				int currRow = 2;    //从第2行开始读取
				string currRowString = string.Empty;
				try    //读入数据
				{
					currRowString = sheet.Cells[currRow, 1]?.Value?.ToString() ?? string.Empty;
					while (!string.IsNullOrWhiteSpace(currRowString) && currRow <= App.ExcelMaxRows)
					{		
						//TODO：考虑读取数据转换失败的情况
						testingList.Add(new Testing
						{
							No = Convert.ToInt32(sheet.Cells[currRow, 1].Value)
							,Person=Convert.ToString(sheet.Cells[currRow,2].Value)
							,WorkingCondition= Convert.ToString(sheet.Cells[currRow, 3].Value)
							,Section= Convert.ToString(sheet.Cells[currRow, 4].Value)
							,Content= Convert.ToString(sheet.Cells[currRow, 5].Value)
							,MaxValue=Math.Round(Convert.ToDecimal(sheet.Cells[currRow,6].Value),Convert.ToInt32(DigitsHoldTextBox.Text))
							,MinValue= Math.Round(Convert.ToDecimal(sheet.Cells[currRow, 7].Value), Convert.ToInt32(DigitsHoldTextBox.Text))
						});
						Debug.WriteLine($"当前行{currRow}，当前读取的字符串{currRowString},人员{Convert.ToString(sheet.Cells[currRow, 2].Value)}");
						
						currRow++;
						currRowString = sheet.Cells[currRow, 1]?.Value?.ToString() ?? string.Empty;
					}
					await package.SaveAsAsync(fileInfo);
				}
				catch (Exception)
				{
					Debug.WriteLine($"当前行{currRow}，当前读取的字符串{currRowString}");
					throw;
				}

			}

			Document doc = new Document();
			
			var builder = new DocumentBuilder(doc);
			builder.PageSetup.Orientation = Orientation.Landscape;

            Aspose.Words.Font font = builder.Font;
			font.Size = 12;
			font.Name = "Times New Roman";

			//结果表格
			var resultTable = builder.StartTable();

			var tableCellWidth = new TableCellWidth
			{
				No = 30,
				WorkingCondition = 70,
				Section=100,
				Content=100,
				MaxValue=50,
				MinValue=50,
				TimeSeries=300
			};

			builder.InsertCell();
			CellFormat cellFormat = builder.CellFormat;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.Font.Bold = true;
			cellFormat.Width = tableCellWidth.No;
            builder.Write("序号");

            builder.InsertCell();
			cellFormat.Width = tableCellWidth.WorkingCondition;
			builder.Write("工况");
            
            builder.InsertCell();
			cellFormat.Width = tableCellWidth.Section;
			builder.Write("截面");
           
            builder.InsertCell();
			cellFormat.Width = tableCellWidth.Content;
			builder.Write("内容");

            builder.InsertCell();
			cellFormat.Width = tableCellWidth.MaxValue;
			builder.Write("最大值");
            builder.InsertCell();
			cellFormat.Width = tableCellWidth.MinValue;
			builder.Write("最小值");
			builder.InsertCell();
			cellFormat.Width = tableCellWidth.TimeSeries;
			builder.Write("时程曲线");

			builder.Font.Bold = false;
            builder.EndRow();

			double ImageWidth = 260;
			double ImageHeight = 100;

			for (int i = 0; i < testingList.Count; i++)
            {
                builder.InsertCell(); cellFormat.Width = tableCellWidth.No; builder.Write($"{testingList[i].No}");
				builder.InsertCell(); cellFormat.Width = tableCellWidth.WorkingCondition; builder.Write($"{testingList[i].WorkingCondition}");
				builder.InsertCell(); cellFormat.Width = tableCellWidth.Section; builder.Write($"{testingList[i].Section}");
				builder.InsertCell(); cellFormat.Width = tableCellWidth.Content; builder.Write($"{testingList[i].Content}");
				builder.InsertCell(); cellFormat.Width = tableCellWidth.MaxValue; builder.Write($"{testingList[i].MaxValue}");
				builder.InsertCell(); cellFormat.Width = tableCellWidth.MinValue; builder.Write($"{testingList[i].MinValue}");
				builder.InsertCell(); cellFormat.Width = tableCellWidth.TimeSeries;

				builder.InsertImage($"{App.InputFolder}//{testingList[i].Person}//{testingList[i].WorkingCondition}//{testingList[i].Section}-{testingList[i].Content}.png", RelativeHorizontalPosition.Margin, 0, RelativeVerticalPosition.Margin, 0, ImageWidth, ImageHeight, WrapType.Inline);

				builder.EndRow();
            }


            builder.EndTable();

			// Set a green border around the table but not inside. 
			resultTable.SetBorder(BorderType.Left, LineStyle.Single, 1.5, Color.Black, true);
			resultTable.SetBorder(BorderType.Right, LineStyle.Single, 1.5, Color.Black, true);
			resultTable.SetBorder(BorderType.Top, LineStyle.Single, 1.5, Color.Black, true);
			resultTable.SetBorder(BorderType.Bottom, LineStyle.Single, 1.5, Color.Black, true);

			doc.Save($"{App.OutputFolder}//{App.OutputReportFile}", SaveFormat.Docx);
			MessageBox.Show("数据处理完成！");

		}
	}
}
