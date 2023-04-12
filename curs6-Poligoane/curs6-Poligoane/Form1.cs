namespace curs6_Poligoane
{
    public partial class Form1 : Form
    {
        MyGraphics g;
        Poligon[] test;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int amount = 1;
            test = new Poligon[amount];
            g = new MyGraphics(pictureBox1);
            test[0] = new Poligon(@"..\..\..\p.txt");
            test[0].Draw(g.gfx);
            for (int i = 1; i < test.Length; i++)
            {
                test[i] = new Poligon(8, g.ResX, g.ResY);
                test[i].Draw(g.gfx);
            }
            g.Refresh();
            Matrix test1 = new Matrix(3, 3, 0, 2);
            Matrix test2 = new Matrix(3, 3, 0, 2);
            Matrix test3 = test1 * test2;
            ViewMat(test1);
            ViewMat(test2);
            ViewMat(test3);
        }
        public void ViewMat(Matrix mat)
        {
            foreach(string s in mat.View())
            {
                listBox1.Items.Add(s);
            }
            listBox1.Items.Add("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(test[0].Perimeter().ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PointF G = test[0].Gpoint();
            g.gfx.DrawEllipse(Pens.Black, G.X, G.Y, 3, 3);
            g.Refresh();
        }
    }
}