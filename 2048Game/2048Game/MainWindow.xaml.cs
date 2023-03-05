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
        readonly Random rng = new Random();
        TextBlock[,] Mat = new TextBlock[4, 4];
        public MainWindow()
        {
            InitializeComponent();
            Height = 800;
            Width = 800;
            Draw();
            StartGame();
            KeyDown += Play;
        }

        private void Play(object sender, KeyEventArgs e)
        {
            Key pressed = e.Key;
            switch (pressed)
            {
                case Key.Left:
                    MoveLeft();
                    break;
                case Key.Right:
                    MoveRight();
                    break;
                case Key.Up:

                    break;
                case Key.Down:

                    break;
                default: break;
            }
            GenerateBox();
            ReDraw();
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
                        Background = new SolidColorBrush(Colors.Black),
                        Tag = "0",
                        FontSize = ((Area.Height) / Size ) / 1.5,
                        Foreground = new SolidColorBrush(Colors.White),
                        TextAlignment = TextAlignment.Center,
                    };
                    Area.Children.Add(t);
                    Grid.SetRow(t, i);
                    Grid.SetColumn(t, j);
                    Mat[i, j] = t;
                }
            }
        }
        public void StartGame()
        {
            GenerateBox();
            GenerateBox();
            ReDraw();
        }
        public void GenerateBox()
        {
            int i = rng.Next(0, Size);
            int j = rng.Next(0, Size);
            while(!OK(i, j))
            {
                i = rng.Next(0, Size);
                j = rng.Next(0, Size);
            }
            TextBlock newBox = Mat[i, j];
            newBox.Tag = "1";
        }
        void MoveRight()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = Size - 1; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (Convert.ToInt32(Mat[i, j].Tag) != 0 && Convert.ToInt32(Mat[i, j].Tag) == Convert.ToInt32(Mat[i, k].Tag))
                        {
                            Mat[i, j].Tag = Convert.ToInt32(Mat[i, j].Tag) + 1;
                            Mat[i, k].Tag = "0";
                        }
                    }
                }
                SortRight(i);
            }
        }
        void SortRight(int i)
        {
            for (int k = Size - 1; k >= 0; k--)
            {
                if (Convert.ToInt16(Mat[i, k].Tag) == 0)
                {
                    for (int j = k - 1 ; j >= 0; j--)
                    {
                        if (Convert.ToInt16(Mat[i, j].Tag) != 0)
                        {
                            (Mat[i, k].Tag, Mat[i, j].Tag) = (Mat[i, j].Tag, Mat[i, k].Tag);
                        }
                    }
                }
            }
        }
        void MoveLeft()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    for(int k = j + 1; k < Size; k++)
                    {
                        if (Convert.ToInt32(Mat[i, j].Tag) != 0 && Convert.ToInt32(Mat[i, j].Tag) == Convert.ToInt32(Mat[i, k].Tag))
                        {
                            Mat[i, j].Tag = Convert.ToInt32(Mat[i, j].Tag) + 1;
                            Mat[i, k].Tag = "0";
                            break;
                        }
                        else if (Convert.ToInt32(Mat[i, k].Tag) == 0)
                        {
                            continue;
                        }                        
                        else break;
                    }
                }
                SortLeft(i);
            }
        }
        void SortLeft(int i)
        {
            for (int k = 0; k < Size; k++)
            {
                if(Convert.ToInt16(Mat[i, k].Tag) == 0)
                {
                    for(int j = k + 1; j < Size; j++)
                    {
                        if(Convert.ToInt16(Mat[i, j].Tag) != 0)
                        {
                            (Mat[i, k].Tag, Mat[i, j].Tag) = (Mat[i, j].Tag, Mat[i, k].Tag);
                        }
                    }
                }
                
            }
        }
        void ReDraw()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    TextBlockSet(Convert.ToInt16(Mat[i, j].Tag), Mat[i, j]);                   
                }
            }
        }
        void TextBlockSet(int i, TextBlock t)
        {
            if(i == 0)
            {
                t.Background = new SolidColorBrush(Colors.Black);
                t.Text = "";
            }
            if (i == 1)
            {
                t.Background = new SolidColorBrush(Colors.Tan);
                t.Text = "2";
            }
            if (i == 2)
            {
                t.Background = new SolidColorBrush(Colors.Yellow);
                t.Text = "4";
            }
            if (i == 3)
            {
                t.Background = new SolidColorBrush(Colors.Orange);
                t.Text = "8";
            }
            if (i == 4)
            {
                t.Background = new SolidColorBrush(Colors.OrangeRed);
                t.Text = "16";
            }

        }
        void Reset(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    Mat[i, j].Tag = "0";
                }
            }
            ReDraw();
            StartGame();
        }
        bool OK(int i, int j)
        {
            if (Convert.ToInt16(Mat[i, j].Tag) == 0)
            {
                return true;
            }
            else return false;
        }
    }
}
