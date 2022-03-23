using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDaoqingZhou.Models
{
    public class TableCellWidth
    {
        /// <summary>
        /// 序号
        /// </summary>
        public double No { get; set; }
        /// <summary>
        /// 工况
        /// </summary>
        public double WorkingCondition { get; set; }
        /// <summary>
        /// 截面
        /// </summary>
        public double Section { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public double Content { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// 时程曲线
        /// </summary>
        public double TimeSeries { get; set; }
    }
}
