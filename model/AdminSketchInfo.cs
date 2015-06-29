using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class AdminSketchInfo
    {
        public SumSketch NewRecord { get; set; }
        public SumSketch DRecord { get; set; }
        public SumSketch AdDrawRecord { get; set; }
        public SumSketch DueDrawRecord { get; set; }
        public SumSketch RemainRecord { get; set; }
        public SumSketch SysPayfee { get; set; }
        public SumSketch CalcPayfee { get; set; }
        public SumSketch MarginPayfee { get; set; }
    }
}
