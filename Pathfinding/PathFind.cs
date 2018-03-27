/***************************************************************************
 * 
 * 创建时间：   2018/3/24 星期六 14:55:23
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
    /// 类PathFind的注释信息
    /// </summary>
    internal class PathFind
    {
        public static bool Mark = true;

        /// <summary>
        /// 对一个矩阵地址进行寻路算法求解
        /// </summary>
        /// <param name="origin">待求解寻路的</param>
        /// <returns></returns>

        public static byte[][] GetMatrixPath(byte[][] origin, Action<byte[][]> callback)
        {

            return GetMatrixPath(origin, new PointInt(0, 0), new PointInt(origin.Length - 1, origin[0].Length - 1), callback);
            //return GetMatrixPath(origin, new PointInt(0, 0), new PointInt(25, 75), callback);
        }
        public static byte[][] GetMatrixPath(byte[][] origin, PointInt loc, PointInt des, Action<byte[][]> callback)
        {
            int wait = 100;
            if (callback == null)
            {
                wait = 0;
                callback = (o) => { };
            }

            var buffer = CopyData(origin);
            var max = new PointInt(buffer.Length - 1, buffer[0].Length - 1);

            if (!CalcPath(buffer, loc, des, max, callback, wait))
            {
                return buffer;
            }

            return buffer;
        }


        private static bool CalcPath(byte[][] buffer, PointInt loc, PointInt des, PointInt max, Action<byte[][]> callback, int wait)
        {

            var root = new SearchTree(loc, 0);
            SearchTree opt = root;
            var next = opt;
            while (true)
            {
                if (next == null)
                {
                    return false;
                }
                loc = next.Curent;

                if (!loc.Vaild(max))
                {
                    //遇到墙壁，处理区域点忽略

                    next = next.Parent;
                    goto endLab;
                }

                int i = des.X - loc.X;
                int j = des.Y - loc.Y;
                int status = i >= j ? 1 : 2;
                if (i == j && j == 0)
                {
                    buffer[loc.X][loc.Y] = 4;
                    DoCallback(callback, buffer, wait);
                    //到达终点寻址完毕
                    return true;
                }
                if (buffer[loc.X][loc.Y] != 0)
                {
                    //寻址失败上层回溯 
                    next = next.Parent;
                    goto endLab;
                }
                else
                {
                    //检查当前点的四个方向是否有通路
                    if (Checkff(buffer, next, max))
                    {
                        buffer[loc.X][loc.Y] = 4;
                        DoCallback(callback, buffer, wait);

                        buffer[loc.X][loc.Y] = 0;
                        DoCallback(callback, buffer, wait);

                        next = next.Parent;
                        //如果有则表示出现闭塞回路
                        goto endLab;
                    } 
                    buffer[loc.X][loc.Y] = 4;
                    DoCallback(callback, buffer, wait); 
                    next = next.YiledNext(status);
                    continue;
                }

                endLab:
                if (next == null)
                    return false;
                loc = next.Curent;

                i = des.X - loc.X;
                j = des.Y - loc.Y;
                status = i >= j ? 1 : 2;

                doYiledNext:
                var n = next.YiledNext(status);
                if (n == null)
                {
                    buffer[loc.X][loc.Y] = 2;
                    DoCallback(callback, buffer, wait);
                    next = next.Parent;
                    goto endLab;
                }




                next = n;
                continue;
            }
        }
        static void DoCallback(Action<byte[][]> callback, byte[][] buffer, int wait)
        {
            callback(buffer);
            System.Threading.Thread.Sleep(wait);

        }

        static bool Checkff(byte[][] buffer, SearchTree tree, PointInt max)
        {
            if (!Mark) return false;

            int c = 0;
            var em = tree.Yiled(0);
            while (em.MoveNext())
            {
                var cur = em.Current;
                var loc = cur.Curent;
                if (!loc.Vaild(max)) continue;
                if (buffer[loc.X][loc.Y] == 4)
                {
                    c++;
                    if (c >= 2) return true;
                } 
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        static byte[][] CopyData(byte[][] origin)
        {
            var x = origin.Length;
            var y = origin.Max(p => p.Length);

            var copy = new byte[x][];

            for (int i = 0; i < x; i++)
            {
                var c = new byte[y];
                Buffer.BlockCopy(origin[i], 0, c, 0, origin[i].Length);
                copy[i] = c;
            }
            return copy;
        }
    }


}
