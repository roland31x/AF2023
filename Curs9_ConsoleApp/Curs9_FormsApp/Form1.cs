
namespace Curs9_FormsApp
{
    public partial class Form1 : Form
    {
        double XStart = -2;
        double YStart = -2;
        double XEnd = 2;
        double YEnd = 2;
        int ZoomSize = 200;
        bool isLoading = false;
        Label[] labels = new Label[6];
        public Form1()
        {
            InitializeComponent();
            Height = 900;
            Width = 900;
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            //PictureBox hoverbox = new PictureBox();
            //hoverbox.Parent = this;
            //hoverbox.Location = new Point(0, 0);
            //hoverbox.Size = new Size(Height, Width);
            //hoverbox.MouseMove += Hoverbox_MouseMove;
            //hoverbox.BackColor = Color.Transparent;
            //pictureBox1.SendToBack();
        }

        //private void Hoverbox_MouseMove(object? sender, MouseEventArgs e)
        //{
        //    Graphics g = (sender as PictureBox)!.CreateGraphics();
        //    g.Clear(Color.White);
        //    g.DrawRectangle(new Pen(Color.Red, 5), e.X - 50, e.Y - 50, 100, 100);

        //}

        private async void Form1_Load(object sender, EventArgs e)
        {
            
            for(int i=0;i<6;i++)
            {
                labels[i] = new Label()
                {
                    Location = new Point(100 + (i % 3) * 205, (i / 3) * 20),
                    Width = 200,
                    Parent = this,
                };
               
                labels[i].SendToBack();
                
            }
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(50, 50);
            pictureBox1.Width = 800;
            pictureBox1.Height = 800;
            await Task.Delay(100);
            //SierpinskiRectangleFunction(pictureBox1);
            await DrawSet(pictureBox1, XStart, XEnd, YStart, YEnd);
            SetLabelText(labels);
        }
        void SierpinskiRectangleFunction(PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();
            Pen p = new Pen(Color.Black, 1);
            PointF[] rect = new PointF[4]
            {
                new PointF(0,0),
                new PointF(pb.Width, 0),
                new PointF(pb.Width, pb.Height),
                new PointF(0,pb.Height),
            };
            RecursiveSierpinskiRectangle(g, p, rect);
        }
        void RecursiveSierpinskiRectangle(Graphics g, Pen p, PointF[] rect)
        {
            float dist = Dist(rect[0], rect[1]) / 3;
            if (dist < 1)
                return;
            g.DrawPolygon(p,rect);
            for(int i = 0; i < 9; i++)
            {
                if(i == 4)
                {
                    continue;
                }
                PointF[] newRect = new PointF[4];
                newRect[0] = new PointF(rect[0].X + (i % 3) * dist, rect[0].Y + (i / 3) * dist);
                newRect[1] = new PointF(newRect[0].X + dist, newRect[0].Y);
                newRect[2] = new PointF(newRect[0].X + dist, newRect[0].Y + dist);
                newRect[3] = new PointF(newRect[0].X, newRect[0].Y + dist);

                RecursiveSierpinskiRectangle(g, p, newRect);
            }
        }
        void SierpinskiTriangleFunction(PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();
            int offset = 10;
            PointF[] pointarray = new PointF[3];
            pointarray[0] = new PointF(offset, pb.Height - offset); // bottom left
            pointarray[1] = new PointF(pb.Width - offset, pb.Height - offset); // bottom right
            pointarray[2] = new PointF(pb.Width / 2, offset); // top point
            Pen p = new Pen(Color.Black, 1);
            RecursiveSierpinskiTriangle(pb, g, p, pointarray);
        }
        void RecursiveSierpinskiTriangle(PictureBox pb, Graphics g, Pen p, PointF[] pointarray)
        {
            if (Dist(pointarray[0], pointarray[1]) < 2)
                return;
            g.DrawPolygon(p, pointarray);
            PointF[] NewTriangle = new PointF[]
            {
                MiddlePoint(pointarray[0],pointarray[1]), // bottom point
                MiddlePoint(pointarray[0],pointarray[2]), // top left
                MiddlePoint(pointarray[1],pointarray[2]), // top right
            };
            PointF[] UpperTriangle = new PointF[]
            {
                NewTriangle[1], NewTriangle[2], pointarray[2],
            };
            PointF[] BottomLeftTriangle = new PointF[]
            {
                pointarray[0], NewTriangle[0], NewTriangle[1],
            };
            PointF[] BottomRightTriangle = new PointF[]
            {
                NewTriangle[0], pointarray[1], NewTriangle[2],
            };
            RecursiveSierpinskiTriangle(pb, g, p, UpperTriangle);
            RecursiveSierpinskiTriangle(pb, g, p, BottomRightTriangle);
            RecursiveSierpinskiTriangle(pb, g, p, BottomLeftTriangle);
        }
        float Dist(PointF left, PointF right)
        {
            return (float)Math.Sqrt((left.X - right.X) * (left.X - right.X) + (left.Y - right.Y) * (left.Y - right.Y));
        }
        PointF MiddlePoint(PointF left, PointF right)
        {
            float x = Math.Min(left.X, right.X) + Math.Abs(left.X - right.X) / 2;
            float y = Math.Min(left.Y, right.Y) + Math.Abs(left.Y - right.Y) / 2;
            return new PointF(x, y);
        }
        private void SetLabelText(Label[] labels)
        {
            for(int i=0;i<labels.Length;i++)
            {
                switch (i)
                {
                    case 0:
                        labels[i].Text = "X:"; break;
                    case 1:
                        labels[i].Text = XStart.ToString(); break;
                    case 2:
                        labels[i].Text = XEnd.ToString(); break;
                    case 3:
                        labels[i].Text = "Y:"; break;
                    case 4:
                        labels[i].Text = (-YEnd).ToString(); break;
                    case 5:
                        labels[i].Text = (-YStart).ToString(); break;
                }
            }
        }

        Task DrawSet(PictureBox pb, double XStart = -2, double XEnd = 2, double YStart = -2, double YEnd = 2)
        {
            
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    int n = Complex.NumberOfIterations(i, j, bmp.Width, bmp.Height, XStart, XEnd, YStart, YEnd);
                    Color result;
                    if (n == Complex.MaxIterations)
                    {
                        result = Color.Black;
                    }
                    else
                    {
                        result = Rainbow(((float)n / ((float)Complex.MaxIterations / 5)));
                    }
                    bmp.SetPixel(i, j, result);
                }
            }
            pb.BackgroundImage = bmp;
            return Task.CompletedTask;
        }
        public static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private async void PB_MouseDown(object sender, MouseEventArgs e)
        {
            if (isLoading) 
                return;
            isLoading = true;
            PictureBox clicked = (PictureBox)sender;
            Graphics g = clicked.CreateGraphics();
            double newXstart = (((double)e.X - (double)ZoomSize / 2) / (double)clicked.Width) * (XEnd - XStart) + XStart;
            double newXend = (((double)e.X + (double)ZoomSize / 2) / (double)clicked.Width) * (XEnd - XStart) + XStart;
            double newYstart = (((double)e.Y - (double)ZoomSize / 2) / (double)clicked.Height) * (YEnd - YStart) + YStart;
            double newYend = (((double)e.Y + (double)ZoomSize / 2) / (double)clicked.Height) * (YEnd - YStart) + YStart;
            g.DrawRectangle(new Pen(Color.White, 5), new Rectangle((int)(e.X - ZoomSize / 2), (int)(e.Y - ZoomSize / 2), ZoomSize, ZoomSize));

            XStart = newXstart;
            XEnd = newXend;
            YStart = newYstart;
            YEnd = newYend;

            await Task.Run(() => DrawSet(pictureBox1, XStart, XEnd, YStart, YEnd));
            SetLabelText(labels);
            isLoading = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}