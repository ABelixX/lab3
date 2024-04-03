using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using static lab3.Form1;

namespace lab3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        static public List<Point1> listOfPoints = new List<Point1>();
        static public List<Point1> listForRectangles = new List<Point1>();
        public static int k = 20;
        public static int width = 601;
        public static int height = 601;
        public static int radius = 8;
        public static int countOfTouching=0;

        public static int x = -3;
        public static int y = 3;

        public static int diff_x = x;
        public static int diff_y = y;
        
        void draw_dude()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 1);
            //отрисовка координат
            for (int i = 0; i < width; i += 20)
            {
                g.DrawLine(pen, new Point(i, 0), new Point(i, height));
                g.DrawLine(pen, new Point(0, i), new Point(width, i));
            }
            g.DrawLine(new Pen(Color.Black, 2f), new Point(width / 2, 0), new Point(width / 2, height));
            g.DrawLine(new Pen(Color.Black, 2f), new Point(0, height / 2), new Point(width, height / 2));
            //квадрат от -15;15
            //рисуем отрезок
            //g.DrawLine(new Pen(Color.Red, 2f), new Point(NormalX(x1), NormalY(y1)), new Point(NormalX(x2), NormalY(y2)));
            g.DrawEllipse(new Pen(Color.Red),NormalX(x)-radius*k,NormalY(y)-radius*k,radius*2*k,radius*2*k);
            if (countOfTouching == 2 || countOfTouching==4||countOfTouching==6 || countOfTouching==8)
            {
                for (int i = 0; i < listOfPoints.Count; i++)
                {
                    g.DrawEllipse(new Pen(Color.Blue, 3f), NormalX(listOfPoints[i].x), NormalY(listOfPoints[i].y), 2, 2);
                }
            }
            if (countOfTouching == 3||countOfTouching==5||countOfTouching==7 || countOfTouching==9)
            {
                for (int i = 0; i < listForRectangles.Count; i++)
                {
                    g.FillRectangle(Brushes.Gray, NormalX(listForRectangles[i].x) - 10, NormalY(listForRectangles[i].y)+10, 20, 20);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text=countOfTouching.ToString();
            if(countOfTouching==0)
            {
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==1)
            {
                x = 0;
                y = 0;
                draw_dude();
                countOfTouching++;
            }
            else if (countOfTouching == 2)
            {
                algorithm();
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==3)
            {
                for(int i =0;i< listOfPoints.Count;i++)
                {
                    listForRectangles.Add(new Point1(listOfPoints[i].x, listOfPoints[i].y+1));
                }
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==4)
            {
                int countt = listOfPoints.Count;
                for(int i =0;i<countt;i++)
                {
                    listOfPoints.Add(new Point1(reflectionX(listOfPoints[i].x), listOfPoints[i].y));
                }
                for(int i =countt;i<listOfPoints.Count;i++)
                {
                    listForRectangles.Add(new Point1(listOfPoints[i].x , listOfPoints[i].y+1));
                }
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==5)
            {
                
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==6)
            {
                int countt = listOfPoints.Count;
                for (int i = 0; i < countt; i++)
                {
                    listOfPoints.Add(new Point1(listOfPoints[i].x, reflectionY(listOfPoints[i].y)));
                }
                for (int i = countt; i < listOfPoints.Count; i++)
                {
                    listForRectangles.Add(new Point1(listOfPoints[i].x , listOfPoints[i].y+1));
                }

                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==7) {
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==8)
            {
                x=diff_x; y=diff_y;
                for(int i =0;i< listOfPoints.Count;i++) 
                {
                    listOfPoints[i].x += diff_x;
                    listOfPoints[i].y += diff_y;
                }
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==9)
            {
                for(int i =0;i<listForRectangles.Count;i++)
                {
                    listForRectangles[i].x += diff_x;
                    listForRectangles[i].y += diff_y;
                }
                draw_dude();
                countOfTouching++;
            }

        }
        int reflectionY(int x_)
        {
            return -x_;
        }
        int reflectionX(int y_)
        {
            return -y_;
        }
        int NormalX(int x)
        {
            return x * k + width / 2;
        }
        int NormalY(int y)
        {
            return -(y * k) + height / 2;
        }
        void algorithm()
        {
            listOfPoints.Clear();
            int x_ = 0;
            int y_ = radius;
            int delf = 2 * (1 - radius);
            int lim = 0;
            int number = 1;
            switch (number)
            {
                case 1:
                    listOfPoints.Add(new Point1(x_, y_));
                    if (y_ <= lim)
                        goto case 4;
                    if (delf < 0)
                        goto case 2;
                    if (delf > 0) 
                        goto case 3;
                    if (delf == 0) 
                        goto case 20;
                    break;
                case 2:
                    int b_ = 2 * delf + 2 * y_ - 1;
                    if (b_ <= 0) goto case 10;
                    if (b_ > 0) goto case 20;
                    break;
                case 3:
                    int q_ = 2 * delf - 2 * x_ - 1;
                    if (q_ <= 0) goto case 20;
                    if (q_ > 0) goto case 30;
                    break;
                case 10:
                    x_++;
                    delf += 2 * x_ + 1;
                    goto case 1;
                case 20:
                    x_++;
                    y_--;
                    delf += 2 * x_ - 2 * y_ + 2;
                    goto case 1;
                case 30:
                    y_--;
                    delf += -2 * y_ + 1;
                    goto case 1;
                case 4:
                    number = 0;
                    break;

            }
        }
    }
   

}
