namespace curs1wfa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // se dau 5 valori intregi aleatoare din intervalul [2, ... ,14] se cere sa se afiseze 1 din cazurile:
        // a) daca exista 5 val identice
        // b) daca exista 4 val identice
        // c) daca exista 3 val identice si 2 identicee
        // d) daca exista 3 val identice
        // e) daca exista 2 cate 2 identice
        // f) daca exista 2 val identice
        // g) nimic
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rng = new Random();
            int[] v = new int[5];
            int[] fq = new int[15];
            for (int i = 0; i < 5; i++)
            {
                v[i] = rng.Next(2, 15);
                listBox1.Items.Add(v[i]);
                fq[v[i]]++;
            }
            int max = fq[0];
            int min = fq[0];
            int cont2 = 0;
            for (int i = 0; i < 15; i++)
            {
                if (fq[i] > max)
                {
                    max = fq[i];
                }
                if (fq[i] < min)
                {
                    min = fq[i];
                }
                if (fq[i] == 2)
                {
                    cont2++;
                }

            }
            string buffer = "";
            for (int i = 0; i < 15; i++)
            {
                buffer += fq[i] + " ";
            }
            listBox1.Items.Add(buffer);

            label1.Text = "1";
            if (max == 5)
            {
                label1.Text = "5";
            }
            if(max == 4)
            {
                label1.Text = "4";
            }
            if(max == 3 && cont2 == 2)
            {
                label1.Text = "3 & 2";
            }
            else
            {
                label1.Text = "3";
            }
            if (max == 2 && cont2 == 2)
            {
                label1.Text = "2 & 2";
            }
            else
            {
                label1.Text = "2";
            }
            
        }
    }
}