
namespace Curs9_FormsApp
{
    public partial class Form1 : Form
    {
        double XStart = -2;
        double YStart = -2;
        double XEnd = 2;
        double YEnd = 2;
        int ZoomSize = 200;
        public Form1()
        {
            InitializeComponent();
            Height = 900;
            Width = 900;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(50, 50);
            pictureBox1.Width = 800;
            pictureBox1.Height = 800;
            DrawSet(pictureBox1, XStart, XEnd, YStart, YEnd);
        }
        void DrawSet(PictureBox pb, double XStart = -2, double XEnd = 2, double YStart = -2, double YEnd = 2)
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
                        result = Rainbow(((float)n / (float)Complex.MaxIterations));
                    }
                    bmp.SetPixel(i, j, result);
                }
            }
            pb.BackgroundImage = bmp;
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

        private void PB_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox clicked = (PictureBox)sender;
            Graphics g = clicked.CreateGraphics();
            double newXstart = (((double)e.X - (double)ZoomSize / 2) / (double)clicked.Width) * (XEnd - XStart) + XStart;
            double newXend = (((double)e.X + (double)ZoomSize / 2) / (double)clicked.Width) * (XEnd - XStart) + XStart;
            double newYstart = (((double)e.Y - (double)ZoomSize / 2) / (double)clicked.Height) * (YEnd - YStart) + YStart;
            double newYend = (((double)e.Y + (double)ZoomSize / 2) / (double)clicked.Height) * (YEnd - YStart) + YStart;
            g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle((int)(e.X - ZoomSize / 2), (int)(e.Y - ZoomSize / 2), ZoomSize, ZoomSize));
            //Thread.Sleep(10000);
            XStart = newXstart;
            XEnd = newXend;
            YStart = newYstart;
            YEnd = newYend;

            DrawSet(pictureBox1, XStart, XEnd, YStart, YEnd);
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