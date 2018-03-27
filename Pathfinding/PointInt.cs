/***************************************************************************
 * 
 * 创建时间：   2018/3/26 星期一 14:04:58
 * 创建人员：   沈瑞
 * CLR版本号：  4.0.30319.42000
 * 备注信息：   未填写备注信息
 * 
 * *************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;

namespace MatrixPathFind
{
    struct PointInt
    {
        public PointInt(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X;
        public int Y;


        public bool Vaild(PointInt d)
        {
            if (this.X < 0 || this.X > d.X || this.Y < 0 || Y > d.Y) return false;
            return true;
        }


        public static PointInt operator +(PointInt x, PointInt y)
        {
            return new PointInt(x.X + y.X, x.Y + y.Y);
        }
        public static PointInt operator -(PointInt x, PointInt y)
        {
            return new PointInt(x.X - y.X, x.Y - y.Y);
        }
    }

}
