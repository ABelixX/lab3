using System.Security.Cryptography.X509Certificates;


namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
     
        //глобальные инты, классы, подготовка короче
        int countOfTouching = 0;

        public static int x1 = 0;
        public static int y1 = -5;
        public static int x2 = 0;
        public static int y2 = 6;
        public static int k = 20;

        public static int width = 601;
        public static int height = 601;
        public static int new_x = 0, new_y = 0;
        public static int diff_x;
        public static int diff_y;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;

        public class Point1
        {
            public int x; public int y;
            public Point1(int x, int y)
            {
                this.x = x; this.y = y;
            }
        }
        static public List<Point1> listOfPoints = new List<Point1>();
        static public List<Point1> listForRectangles= new List<Point1>();
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
            g.DrawLine(new Pen(Color.Red, 2f), new Point(NormalX(x1), NormalY(y1)), new Point(NormalX(x2), NormalY(y2)));
            if (countOfTouching == 4 || countOfTouching==6||countOfTouching==8||countOfTouching==10)
            {
                for (int i = 0; i < listOfPoints.Count; i++)
                {
                    g.DrawEllipse(new Pen(Color.Blue, 3f), NormalX(listOfPoints[i].x), NormalY(listOfPoints[i].y), 2, 2);
                }  
            }
            if(countOfTouching == 5 || countOfTouching==7||countOfTouching==9||countOfTouching==11)
            {
                for(int i =0;i<listForRectangles.Count;i++)
                {
                    g.FillRectangle(Brushes.Gray, NormalX(listForRectangles[i].x), NormalY(listForRectangles[i].y), 20, 20);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = countOfTouching.ToString();
            if (countOfTouching == 0)
            {
                draw_dude();
                ++countOfTouching;
            }
            else if (countOfTouching == 1)//сдвиг в плоскости где y>0
            {
                if (y1>=y2)  //пусть за минимлаьную точку будут отвечать x1 y1
                {
                    int tmp = y1;
                    y1 = y2; y2 = tmp;
                    tmp = x1;
                    x1 = x2; x2 = tmp;
                }
                diff_x = new_x - x1;
                diff_y = new_y - y1;
                x1 = new_x;
                y1 = new_y;
                x2 += diff_x;
                y2 += diff_y;
                draw_dude();
                ++countOfTouching;
            }
            else if(countOfTouching==2) //отражение если нужно по ќY
            {
                if(x2<0)
                {
                    x2 = reflectionY(x2);
                    draw_dude();
                    flag2 = true;
                }
                ++countOfTouching;
            }
            else if(countOfTouching==3)
            {
                if (!(y2 <= x2)) //отражение y<=x
                {
                    Point1 pp = reflectionXY(x2,y2);
                    x2 = pp.x; 
                    y2 = pp.y;
                    draw_dude();
                    flag3 = true;
                }
                ++countOfTouching;
            }
            else if(countOfTouching==4) //сам алгоритм, рисуем точки
            {
                listOfPoints.Clear();
                int x = x1;
                int y = y1;
                int differenceX = x2 - x1;
                int differenceY = y2 - y1;
                float ee = 2 * differenceY - differenceX;
                for (int i = 0; i <= differenceX; ++i)
                {
                    listOfPoints.Add(new Point1(x, y));
                    while (ee >= 0)
                    {
                        y += 1;
                        ee += -2 * differenceX;
                    }
                    x += 1;
                    ee += 2 * differenceY;
                }
                draw_dude();
                ++countOfTouching;

            }
            else if(countOfTouching==5)
            {
                listForRectangles.Clear();
                for (int i = 0;i<listOfPoints.Count-1;++i)
                {
                    
                    if (listOfPoints[i].y != listOfPoints[i+1].y)  
                        listForRectangles.Add(new Point1(listOfPoints[i].x, listOfPoints[i].y+1));
                    else if (listOfPoints[i].y == listOfPoints[i+1].y && listOfPoints[i].y==0)
                        listForRectangles.Add(new Point1(listOfPoints[i].x, listOfPoints[i].y + 1));
                    else
                        listForRectangles.Add(new Point1(listOfPoints[i].x, listOfPoints[i].y));
                }
                draw_dude();
                ++countOfTouching;

            }
            else if(countOfTouching == 6)
            {
                if(flag3)
                {
                    Point1 pp = reflectionXY(x2, y2);
                    x2 = pp.x;
                    y2 = pp.y;

                    for(int i = 0; i < listOfPoints.Count; ++i)
                    {
                        Point1 p1 = reflectionXY(listOfPoints[i].x, listOfPoints[i].y);
                        listOfPoints[i].x = p1.x;
                        listOfPoints[i].y = p1.y;
                    }
                    draw_dude();
                }
                ++countOfTouching;
            }
            else if(countOfTouching==7)
            {
                if(flag3)
                {
                    for(int i =0;i<listForRectangles.Count; ++i)
                    {
                        Point1 p1 = reflectionXY(listForRectangles[i].x, listForRectangles[i].y);
                        listForRectangles[i].x = p1.x-1;
                        listForRectangles[i].y= p1.y+1;
                    }
                    draw_dude();
                }
                ++countOfTouching;
            }
            else if(countOfTouching==8)
            {
                if(flag2)
                {
                    x2 = reflectionY(x2);
                    for(int i =0;i<listOfPoints.Count; ++i)
                    {
                        listOfPoints[i].x = reflectionY(listOfPoints[i].x);
                    }
                    draw_dude();
                }
                countOfTouching++;
            }
            else if(countOfTouching==9)
            {
                if(flag2)
                {
                    for(int i =0;i<listForRectangles.Count;++i)
                    {
                        listForRectangles[i].x = reflectionY(listForRectangles[i].x)-1;
                    }
                    draw_dude();
                }
                ++countOfTouching;
            }
            else if(countOfTouching==10)
            {
                x1 -= diff_x; y1 -= diff_y;
                x2 -= diff_x; y2 -= diff_y;
                for(int i =0;i<listOfPoints.Count;++i)
                {
                    listOfPoints[i].x -= diff_x;
                    listOfPoints[i].y -= diff_y;
                }
                draw_dude();
                countOfTouching++;
            }
            else if(countOfTouching==11)
            {
                for(int i =0;i<listForRectangles.Count;++i)
                {
                    listForRectangles[i].x -= diff_x;
                    listForRectangles[i].y-=diff_y;
                }
                draw_dude();
                ++countOfTouching;
            }
        }

        int NormalX(int x)
        {
            return x * k + width / 2;
        }
        int NormalY(int y)
        {
            return -(y * k) + height / 2;
        }
        int reflectionY(int x_)
        {
            return -x_;
        }
        Point1 reflectionXY(int x_,int y_)
        {
            return new Point1(y_, x_);
        }
    }
}