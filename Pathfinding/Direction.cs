/***************************************************************************
 * 
 * 创建时间：   2018/3/26 星期一 17:06:33
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
    /// <summary>
    /// 提供路径方向的标识
    /// </summary> 
    [Flags]
    enum Direction
    {
        Unkonwn = 0,

        /// <summary>
        /// 北
        /// </summary>
        N = 2,
        /// <summary>
        /// 东
        /// </summary>
        E = 4,
        /// <summary>
        /// 南
        /// </summary>
        S = 8,
        /// <summary>
        /// 西
        /// </summary>
        W = 16,
        /// <summary>
        /// 东北
        /// </summary>
        NE = N | E,
        /// <summary>
        /// 西北
        /// </summary>
        NW = N | W,
        /// <summary>
        /// 东南
        /// </summary>
        SE = N | W,
        /// <summary>
        /// 西南
        /// </summary>
        SW = N | W,
    }

    class DirectionHelper
    {
        /// <summary>
        /// 确定 x点所在y点的方向
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Direction Calc(PointInt x, PointInt y)
        {
            int i = x.X - y.X;
            int j = x.Y - y.Y;
            Direction d = Direction.Unkonwn;

            if (i < 0) d = d | Direction.W;
            if (i > 0) d = d | Direction.E;
            if (j < 0) d = d | Direction.N;
            if (j > 0) d = d | Direction.S;

            return d;
        }
    }
}
