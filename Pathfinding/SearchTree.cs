/***************************************************************************
 * 
 * 创建时间：   2018/3/26 星期一 14:04:19
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
    /// 搜索树
    /// </summary>
    class SearchTree
    {
        public SearchTree(PointInt cur, int _depth)
        {
            this.Curent = cur;
            this._depth = _depth;
        }

        public SearchTree Parent { get; private set; }
        public PointInt Curent
        {
            get; private set;
        }

        private SearchTree _left;
        private SearchTree _right;
        private SearchTree _up;
        private SearchTree _down;
        private int _depth = 0;
        public SearchStatus SearchStatus { get; set; }




        public int Depth => _depth;

        public SearchTree Left
        {
            get
            {
                if (_left == null)
                {
                    _left = new SearchTree(Curent + new PointInt(-1, 0), _depth++);
                    _left.Parent = this;
                    _left.Direction = Direction.W;
                }
                return _left;

            }
        }
        public SearchTree Right
        {
            get
            {
                if (_right == null)
                {
                    _right = new SearchTree(Curent + new PointInt(1, 0), _depth++);
                    _right.Parent = this;
                    _right.Direction = Direction.E;
                }
                return _right;

            }
        }
        public SearchTree Up
        {
            get
            {
                if (_up == null)
                {
                    _up = new SearchTree(Curent + new PointInt(0, -1), _depth++);
                    _up.Parent = this;
                    _up.Direction = Direction.N;
                }
                return _up;

            }
        }
        public SearchTree Down
        {
            get
            {
                if (_down == null)
                {
                    _down = new SearchTree(Curent + new PointInt(0, 1), _depth++);
                    _down.Parent = this;
                    _down.Direction = Direction.S;
                }
                return _down;

            }
        }

        public Direction Direction { get; private set; }

        public void Remove()
        {
            this.Parent = null;
        }

       public IEnumerator<SearchTree> Yiled(int status)
        {
            switch (status)
            {
                case 1:
                    yield return Right;
                    yield return Down;
                    yield return Up;
                    yield return Left;
                    break;
                case 2:
                    yield return Down;
                    yield return Right;
                    yield return Left;
                    yield return Up;
                    break;
                default:
                    yield return Right;
                    yield return Down;
                    yield return Left;
                    yield return Up;
                    break;
            }
        }

        private IEnumerator<SearchTree> em;
        public SearchTree YiledNext(int status)
        {
            if (em == null)
            {
                em = Yiled(status);
            }

            if (em.MoveNext())
            {
                return em.Current;
            }

            return null;

        }

        public override string ToString()
        {
            return $"{{{Curent.X},{Curent.Y}}}";
        }
    }
    enum SearchStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unkonown = 0,
        /// <summary>
        /// 已探索
        /// </summary>
        Fail,
        /// <summary>
        /// 通路
        /// </summary>
        Success
    }

}
