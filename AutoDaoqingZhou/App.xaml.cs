using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AutoDaoqingZhou
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static double ScreenWidth = SystemParameters.PrimaryScreenWidth;
        public static double ScreenHeight = SystemParameters.PrimaryScreenHeight;
        public static int ExcelMaxRows = 1000_000;    //excel最大读取行数
        public static string InputFolder = "数据处理";
        public static string OutputFolder = "Output";
        public static string DataProcessingFile = "道庆洲数据处理-输入.xlsx";
        public static string OutputReportFile = "道庆洲数据处理计算书.docx";
    }
}
