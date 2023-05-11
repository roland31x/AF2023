
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
            await DrawSet(pictureBox1, XStart, XEnd, YStart, YEnd);
            SetLabelText(labels);
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
                        result = Rainbow(((float)n / (float)Complex.MaxIterations));
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
            //Thread.Sleep(10000);
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