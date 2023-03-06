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
        int Size = App.GameSize;
        int Steps { get; set; }
        int Score { get; set; }
        readonly double Percent = 0.7;
        bool GameWon = false;
        bool Classic = App.IsClassic;
        readonly Random rng = new Random();
        Label[,] Mat;
        public MainWindow()
        {
            InitializeComponent();
            Mat = new Label[Size, Size];
            Height = 800;
            Width = 800;
            Draw();
            StartGame();
            KeyDown += Play;
            ResizerEnabler.FocusVisualStyle = null;
            GameModeButton.FocusVisualStyle = null;
            ResetButton.FocusVisualStyle = null;
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
                    MoveUp();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                default: return;
            }
            Steps++;
            GenerateNewBox();
            ReDraw();
            if (GameWon)
            {
                if (Classic)
                {
                    MessageBox.Show($"You win! Your score is: {Score}. You won in {Steps} steps.");
                    StartGame();
                }
                else
                {
                    MessageBox.Show($"You win! Endless mode is enabled you can continue playing.");
                }
            }
        }

        public void Draw()
        {
            Area.Background = new SolidColorBrush(Colors.LightGray);
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
                    Label t = new Label
                    {
                        Width = (Area.Width) / Size - 2,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = (Area.Width) / Size - 2,
                        Background = new SolidColorBrush(Colors.Black),
                        Tag = "0",
                        FontSize = (Area.Width / Size) / 1.5,
                        Foreground = new SolidColorBrush(Colors.White),
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        FontWeight = FontWeights.Bold,                       
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
            GameWon = false;
            Steps = 0;
            Score = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Mat[i, j].Tag = "0";
                }
            }
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
            Label newBox = Mat[i, j];
            newBox.Tag = "1";
        }
        public void GenerateNewBox()
        {
            int i = rng.Next(0, Size);
            int j = rng.Next(0, Size);
            try
            {
                while (!OK(i, j))
                {
                    i = rng.Next(0, Size);
                    j = rng.Next(0, Size);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                StartGame();
                return;
            }
            Label newBox = Mat[i, j];
            int chance = rng.Next(0, 3);
            if(chance == 2)
            {
                newBox.Tag = "2";
            }
            else newBox.Tag = "1";
        }
        void MoveUp()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int k = j + 1; k < Size; k++)
                    {
                        if (Convert.ToInt32(Mat[j, i].Tag) != 0 && Convert.ToInt32(Mat[j, i].Tag) == Convert.ToInt32(Mat[k, i].Tag))
                        {
                            Mat[j, i].Tag = Convert.ToInt32(Mat[j, i].Tag) + 1;
                            Mat[k, i].Tag = "0";
                            Score += (int)Math.Pow(2, Convert.ToDouble(Mat[j, i].Tag));
                            break;
                        }
                        else if (Convert.ToInt32(Mat[k, i].Tag) == 0)
                        {
                            continue;
                        }
                        else break;
                    }
                }
                SortUp(i);
            }
        }
        void SortUp(int i)
        {
            for (int k = 0; k < Size; k++)
            {
                if (Convert.ToInt16(Mat[k, i].Tag) == 0)
                {
                    for (int j = k + 1; j < Size; j++)
                    {
                        if (Convert.ToInt16(Mat[j, i].Tag) != 0)
                        {
                            (Mat[k, i].Tag, Mat[j, i].Tag) = (Mat[j, i].Tag, Mat[k, i].Tag);
                            break;
                        }
                    }
                }
            }
        }
        void MoveDown()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = Size - 1; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (Convert.ToInt32(Mat[j, i].Tag) != 0 && Convert.ToInt32(Mat[j, i].Tag) == Convert.ToInt32(Mat[k, i].Tag))
                        {
                            Mat[j, i].Tag = Convert.ToInt32(Mat[j, i].Tag) + 1;
                            Mat[k, i].Tag = "0";
                            Score += (int)Math.Pow(2, Convert.ToDouble(Mat[j, i].Tag));
                            break;
                        }
                        else if (Convert.ToInt32(Mat[k, i].Tag) == 0)
                        {
                            continue;
                        }
                        else break;
                    }
                }
                SortDown(i);
            }
        }
        void SortDown(int i)
        {
            for (int k = Size - 1; k >= 0; k--)
            {
                if (Convert.ToInt16(Mat[k, i].Tag) == 0)
                {
                    for (int j = k - 1; j >= 0; j--)
                    {
                        if (Convert.ToInt16(Mat[j, i].Tag) != 0)
                        {
                            (Mat[k, i].Tag, Mat[j, i].Tag) = (Mat[j, i].Tag, Mat[k, i].Tag);
                            break;
                        }
                    }
                }
            }
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
                            Score += (int)Math.Pow(2, Convert.ToDouble(Mat[i, j].Tag));
                            break;
                        }
                        else if (Convert.ToInt32(Mat[i, k].Tag) == 0)
                        {
                            continue;
                        }
                        else break;
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
                            break;
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
                            Score += (int)Math.Pow(2, Convert.ToDouble(Mat[i, j].Tag));
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
                            break;
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
            StepsBox.Text = "Steps: " + Convert.ToString(Steps);
            ScoreBox.Text = "Score: " + Convert.ToString(Score);
        }
        void TextBlockSet(int i, Label t)
        {
            if(i == 0)
            {
                t.Content = "";
            }
            else
            {
                t.Content = Convert.ToString(Math.Pow(2, i));
            }
            if(i >= 7 && i < 10)
            {
                t.FontSize = ((Area.Width / Size) / 1.5) / 1.5;
            }
            else if (i >= 10)
            {
                t.FontSize = (((Area.Width / Size) / 1.5) / 1.5 ) / 1.5;
            }
            else
            {
                t.FontSize = (Area.Width / Size) / 1.5;
            }
            if (i == 11)
            {
                GameWon = true;
            }
            t.Background = GetColor(i);
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
            bool canGenerate = false;
            for(int i1 = 0; i1 < Size; i1++)
            {
                for(int j1 = 0; j1 < Size; j1++)
                {
                    if (Convert.ToInt16(Mat[i1, j1].Tag) == 0)
                    {
                        canGenerate = true;
                    }
                }
            }
            if (canGenerate)
            {
                if (Convert.ToInt16(Mat[i, j].Tag) == 0)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                throw new Exception($"You lose! You achieved a score of {Score}! Try again!");
            }
            
        }
        static SolidColorBrush GetColor(int i)
        {
            switch (i)
            {
                case 1:
                    return new SolidColorBrush(Colors.Tan);
                case 2:
                    return new SolidColorBrush(Colors.SandyBrown);
                case 3:
                    return new SolidColorBrush(Colors.RosyBrown);
                case 4:
                    return new SolidColorBrush(Colors.Orange);
                case 5:
                    return new SolidColorBrush(Colors.Red);
                case 6: 
                    return new SolidColorBrush(Colors.Yellow);
                case 7:
                    return new SolidColorBrush(Colors.Teal);
                case 8:
                    return new SolidColorBrush(Colors.LawnGreen);
                case 9:
                    return new SolidColorBrush(Colors.LightBlue);
                case 10:
                    return new SolidColorBrush(Colors.Purple);
                case 11:
                    return new SolidColorBrush(Colors.Pink);
                default:
                    return new SolidColorBrush(Colors.Black);

            }
        }

        private void GameSwitch(object sender, RoutedEventArgs e)
        {
            if (Classic)
            {
                Button b = sender as Button;
                b.Content = "Endless";
                b.Background = new SolidColorBrush(Colors.Goldenrod);
                Classic = false;
                Resizer.Visibility = Visibility.Visible;
                ResizerButton.Visibility = Visibility.Visible;
                ResizeTBlock.Visibility = Visibility.Visible;
                ResizerEnabler.Visibility = Visibility.Visible;
                StartGame();

            }
            else
            {
                Button b = sender as Button;
                b.Content = "Classic";
                b.Background = new SolidColorBrush(Color.FromRgb(255,233,124));
                Classic = true;
                Resizer.Visibility = Visibility.Collapsed;
                ResizerButton.Visibility = Visibility.Collapsed;
                ResizeTBlock.Visibility = Visibility.Collapsed;
                ResizerEnabler.Visibility = Visibility.Collapsed;
                Size = 4;
                Area = new Grid();
                MainCanvas.Children.Add(Area);
                Mat = new Label[Size, Size];
                Draw();
                StartGame();
            }
        }

        private void Resizer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResizerButton.IsEnabled = false;
            if (int.TryParse(Resizer.Text, out int value))
            {
                if (value < 4 || value > 32)
                {
                    ResizerButton.IsEnabled = false;
                    return;
                }
                else
                {
                    ResizerButton.IsEnabled = true;
                }
            }
        }

        private void ResizerButton_Click(object sender, RoutedEventArgs e)
        {
            Size = int.Parse(Resizer.Text);
            Area = new Grid();
            MainCanvas.Children.Add(Area);
            Mat = new Label[Size, Size];
            Draw();
            StartGame();
            
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Resizer.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Resizer.IsEnabled = false;
        }
    }
}
