namespace ImageEditing
{
    public partial class Form1 : Form
    {
        Bitmap Source;
        Bitmap Dest;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Source = new Bitmap(@"..\..\..\tj.jpg");
            pictureBox1.Image = Source;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dest = new Bitmap(Source.Width, Source.Height);
            pictureBox2.Image = Dest;

            for (int i = 0; i < Dest.Width; i++)
            {
                for (int j = 0; j < Dest.Height; j++)
                {
                    Color old = Source.GetPixel(i, j);
                    int c = (old.R + old.B + old.G) / 3;
                    Color t = Color.FromArgb(c, c, c);
                    Dest.SetPixel(i, j, t);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dest = new Bitmap(Source.Width, Source.Height);
            pictureBox2.Image = Dest;
            int k = 80;

            for (int i = 0; i < Dest.Width; i++)
            {
                for (int j = 0; j < Dest.Height; j++)
                {
                    Color old = Source.GetPixel(i, j);
                    int r = Math.Max(0, old.R - k);
                    int b = Math.Max(0, old.B - k);
                    int g = Math.Max(0, old.G - k);
                    Color t = Color.FromArgb(r, g, b);
                    Dest.SetPixel(i, j, t);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dest = new Bitmap(Source.Width, Source.Height);
            pictureBox2.Image = Dest;
            int k = 80;

            for (int i = 0; i < Dest.Width; i++)
            {
                for (int j = 0; j < Dest.Height; j++)
                {
                    Color old = Source.GetPixel(i, j);
                    int r = Math.Min(255, old.R + k);
                    int b = Math.Min(255, old.B + k);
                    int g = Math.Min(255, old.G + k);
                    Color t = Color.FromArgb(r, g, b);
                    Dest.SetPixel(i, j, t);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dest = new Bitmap(Source.Width, Source.Height);
            pictureBox2.Image = Dest;
            int k = 80;

            for (int i = 0; i < Dest.Width; i++)
            {
                for (int j = 0; j < Dest.Height; j++)
                {

                    Color old = Source.GetPixel(i, j);
                    int avg = (old.R + old.B + old.G) / 3;
                    int r, g, b;
                    if (avg > 128)
                    {
                        r = Math.Min(255, old.R + k);
                        b = Math.Min(255, old.B + k);
                        g = Math.Min(255, old.G + k);
                    }
                    else
                    {
                        r = Math.Max(0, old.R - k);
                        b = Math.Max(0, old.B - k);
                        g = Math.Max(0, old.G - k);
                    }

                    Color t = Color.FromArgb(r, g, b);
                    Dest.SetPixel(i, j, t);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dest = new Bitmap(Source.Width, Source.Height);
            pictureBox2.Image = Dest;
            int k = 110;

            for (int i = 0; i < Dest.Width; i++)
            {
                for (int j = 0; j < Dest.Height; j++)
                {
                    Color old = Source.GetPixel(i, j);
                    int r = (old.R + k) % 255;
                    int b = (old.B + k) % 255;
                    int g = (old.G + k) % 255;
                    Color t = Color.FromArgb(r, g, b);
                    Dest.SetPixel(i, j, t);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dest = new Bitmap(Source.Width * 2 - 1, Source.Height * 2 - 1);
            pictureBox2.Image = Dest;
            int k = 110;

            for (int i = 0; i < Source.Width; i++)
            {
                for (int j = 0; j < Source.Height; j++)
                {
                    Color old = Source.GetPixel(i, j);
                    
                    Dest.SetPixel(i*2, j*2, old);
                }
            }
            for (int i = 0; i < Dest.Width; i += 2)
            {
                for (int j = 1; j < Dest.Height; j += 2)
                {
                    Color t1 = Dest.GetPixel(i, j - 1);
                    Color t2 = Dest.GetPixel(i, j + 1);


                    Dest.SetPixel(i, j, Color.FromArgb((t1.R + t2.R)/2, (t1.G + t2.G) / 2, (t1.B + t2.B) / 2));
                }
            }
            for (int i = 1; i < Dest.Width; i += 2)
            {
                for (int j = 0; j < Dest.Height; j++)
                {
                    Color t1 = Dest.GetPixel(i - 1, j);
                    Color t2 = Dest.GetPixel(i + 1, j);


                    Dest.SetPixel(i, j, Color.FromArgb((t1.R + t2.R) / 2, (t1.G + t2.G) / 2, (t1.B + t2.B) / 2));
                }
            }
        }
    }
}