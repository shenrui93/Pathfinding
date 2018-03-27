using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixPathFind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[][] origin;
        const int P = 100;
        bool isStop = true;


        private void button1_Click(object sender, EventArgs e)
        {
            isStop = true;
            origin = new byte[P][];
            Random r = new Random();

            for (int i = 0; i < P; i++)
            {
                var b = new byte[P];

                for (int j = 0; j < P; j++)
                {
                    b[j] = (byte)(r.Next(0, 100) >= 70 ? 1 : 0);
                }
                origin[i] = b;
            }
            origin[0][0] = 0;
            origin[P - 1][P - 1] = 0;

            PrintDD(origin);

        }
        private void PrintDD(byte[][] origin)
        {

            Bitmap map = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            float pi = (float)(map.Width - 1) / origin[0].Length;
            float pj = (float)(map.Height - 1) / origin.Length;

            using (Graphics g = Graphics.FromImage(map))
            {
                for (int line = 0; line < origin.Length; line++)
                {
                    for (int cell = 0; cell < origin[line].Length; cell++)
                    {
                        Brush color = Brushes.White;

                        switch (origin[line][cell])
                        {
                            case 1: color = Brushes.Black; break;
                            case 2: color = Brushes.Yellow; break;
                            case 4: color = Brushes.Red; break;
                            default: color = Brushes.Green; break;
                        }

                        g.FillRectangle(color, new RectangleF(line * pj, pi * cell, pj, pi));
                    }
                }
                g.Save();
            };

            pictureBox1.Image = map;

        }
        private void btn_Calc_Click(object sender, EventArgs e)
        {
            isStop = true;

            var result = PathFind.GetMatrixPath(origin, null);
            PrintDD(result);
        }
        private void ShowBox(string msg)
        {
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button2_Click(object sender, EventArgs e)
        {

            isStop = false;
            System.Threading.ThreadPool.UnsafeQueueUserWorkItem(o =>
            {
                try
                {
                    PathFind.GetMatrixPath(origin, result =>
                    {
                        if (isStop)
                            System.Threading.Thread.CurrentThread.Abort();
                        this.Invoke(() =>
                        {
                            PrintDD(result);
                        });
                    });
                }
                catch (Exception)
                {
                }
            }, null);



            //var result = PathFind.GetMatrixPath(origin, null);
            //if (result == null)
            //{
            //    ShowBox("当前地图无解"); 
            //    return;
            //}
            //else
            //{
            //    PrintDD(origin);
            //    ShowBox("当前地图有解");
            //}

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        public virtual void Invoke(Action callback)
        {
            base.Invoke(callback);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isStop = true;
            PrintDD(origin);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
