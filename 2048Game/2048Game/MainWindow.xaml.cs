using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Size = 4;
        double Percent = 0.7;
        TextBlock[,] Mat = new TextBlock[4, 4];
        public MainWindow()
        {
            InitializeComponent();
            Height = 800;
            Width = 800;
            Draw();
        }
        public void Draw()
        {
            Area.Background = new SolidColorBrush(Colors.Black);
            Area.Height = Height * Percent;
            Area.Width = Width * Percent;
            Canvas.SetLeft(Area, (Width - Width * Percent) / 2);
            Canvas.SetTop(Area, (Height - Height * Percent) / 2);
            for (int i = 0; i < Size; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength((Area.Width) / Size);
                Area.ColumnDefinitions.Add(c);
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength((Area.Height) / Size);
                Area.RowDefinitions.Add(r);
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TextBlock t = new TextBlock
                    {
                        Width = (Area.Width) / Size - 2,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = (Area.Width) / Size - 2,
                        Background = new SolidColorBrush(Colors.Tan),
                        Tag = "0",
                    };
                    Area.Children.Add(t);
                    Grid.SetRow(t, i);
                    Grid.SetColumn(t, j);
                    Mat[i, j] = t;
                }
            }
        }
    }
}
