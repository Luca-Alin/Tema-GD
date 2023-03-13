namespace GC03._06._2023
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Ex_1(e);
            //Ex_2(e);
            //Ex_3(e);
        }


        //private void Form1_Paint(object sender, PaintEventArgs e) {   Ex_1(e);}

        private const int n = 100;
        private const int d = 100;
        private Point q;
        private List<Point> points = new List<Point>();
        Random random = new Random();
        private double Distance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        private void Ex_1(PaintEventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                int x = random.Next(this.ClientRectangle.Width);
                int y = random.Next(this.ClientRectangle.Height);

                Point point = new Point(x, y);
                points.Add(point);
            }

            q = new Point(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);

            List<Point> pointsWithinDistance = new List<Point>();

            foreach (var point in points)
            {
                double distance = Distance(q, point);

                if (distance <= d)
                {
                    pointsWithinDistance.Add(point);
                }
            }

            // Highlight points within distance
            foreach (var point in pointsWithinDistance)
            {
                int highlightSize = 10;
                Rectangle rect = new Rectangle(
                    point.X - highlightSize / 2,
                    point.Y - highlightSize / 2,
                    highlightSize,
                    highlightSize);
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    this.CreateGraphics().FillEllipse(brush, rect);
                }
            }

            Graphics g = e.Graphics;
            Pen pointPen = new Pen(Color.Black);

            foreach (var point in points)
            {
                g.DrawEllipse(pointPen, new Rectangle(point, new Size(1, 1)));
            }
        }


        private void Ex_2(PaintEventArgs e)
        {
            Random rnd = new Random();
            int n = rnd.Next(5, 10);
            Point[] points = new Point[n];
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Red, 2);

            int x, y;
            for (int i = 0; i < n; i++)
            {
                x = rnd.Next(10, this.ClientSize.Width - 10);
                y = rnd.Next(10, this.ClientSize.Height - 10);
                points[i] = new Point(x, y);
                g.DrawEllipse(p, x, y, 2, 2);
            }




            Point p1 = new Point();
            Point p2 = new Point();
            Point p3 = new Point();
            int maxArea = int.MaxValue;
            for (int i = 0; i < n - 2; i++)
                for (int j = i + 1; j < n - 1; j++)              
                    for (int k = j + 1; k < n; k++)
                    {
                        int area = Math.Abs(points[i].X * (points[j].Y - points[k].Y) + 
                                            points[j].X * (points[k].Y - points[i].Y) + 
                                            points[k].X * (points[i].Y - points[j].Y)) / 2;

                        if (area < maxArea)
                        {
                            maxArea = area;
                            p1 = points[i];
                            p2 = points[j]; 
                            p3 = points[k];
                        }
                    }

            g.DrawPolygon(p, new Point[] { p1, p2, p3 });   
        }



        private void Ex_3(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 2);
            Random rnd = new Random();
            int n = rnd.Next(20, 50);
            int x, y;
            Point[] point = new Point[n];

            int CGx = 0;
            int CGy = 0;


            for (int i = 0; i < n; i++)
            {
                x = rnd.Next(this.ClientSize.Width / 3, this.ClientSize.Width - this.ClientSize.Width / 3);
                y = rnd.Next(this.ClientSize.Height / 3, this.ClientSize.Height - this.ClientSize.Height / 3);
                point[i] = new Point(x, y);
                g.DrawEllipse(p, x, y, 2, 2);

                CGx += x;
                CGy += y;
            }

            CGx /= n;
            CGy /= n;


            int razaMaxima = 0;

            for (int i = 0; i < n; i++)
            {
                int raza = (int)Math.Sqrt((point[i].Y - CGy) * (point[i].Y - CGy) +
                                        (point[i].X - CGx) * (point[i].X - CGx));

                if (raza > razaMaxima) 
                    razaMaxima = raza;
            }

            int startCercx = CGx - razaMaxima;
            int startCercy = CGy - razaMaxima;

            g.DrawEllipse(p, startCercx, startCercy, razaMaxima * 2, razaMaxima * 2);

        }
    }
}