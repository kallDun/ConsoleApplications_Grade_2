using Lab_5.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Figure> figures = new();

        public MainWindow() 
        { 
            InitializeComponent();
            StartRender();
        }

        private bool renderWorks = true;
        public async void StartRender()
        {
            while (renderWorks)
            {
                var bitmap = new RenderTargetBitmap((int)Width, (int)Height, 96, 96, PixelFormats.Pbgra32);

                var drawingvisual = new DrawingVisual();
                using (DrawingContext dc = drawingvisual.RenderOpen()) RenderFrame(dc);

                bitmap.Render(drawingvisual);
                Canvas.Source = bitmap;

                await Task.Delay(20);
            }
        }
        private void RenderFrame(DrawingContext dc) => figures.ForEach(x => x?.Draw(dc));

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            var init_position = new Point(80, 150);

            Figure figure = (sender as Button).Tag switch
            {
                "Circle" => new Circle(init_position),
                "Square" => new Square(init_position),
                "Rhomb" => new Rhomb(init_position),
                _ => new Circle(init_position)
            };

            figure.Color = Brushes.Black;
            figure.MoveRight();

            figures.Add(figure);
        }
    }
}
