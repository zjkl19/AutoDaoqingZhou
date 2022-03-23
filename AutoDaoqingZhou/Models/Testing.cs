using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDaoqingZhou.Models
{
    /// <summary>
    /// 各个测试信息
    /// </summary>
    public class Testing
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 数据采集人员
        /// </summary>
        public string Person { get; set; }
        /// <summary>
        /// 工况
        /// </summary>
        public string WorkingCondition { get; set; }
        /// <summary>
        /// 截面
        /// </summary>
        public string Section { get; set; }
        /// <summary>
        /// 工况内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxValue { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public decimal MinValue { get; set; }
    }
}
