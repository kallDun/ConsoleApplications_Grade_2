using CalcMethodLab4.Logic;
using CalcMethodLab4.Logic.Functions.Abstract;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalcMethodLab4
{
    public delegate Point GetPointValue();

    public partial class MainWindow : Window
    {
        SplineType splineType;
        int points_count = 5;
        GetPointValue[] getPointValues = new GetPointValue[5]
        {
            () => new Point(0, 1),
            () => new Point(4, 3),
            () => new Point(7, 7),
            () => new Point(10, 5),
            () => new Point(12, 6)
        };

        public MainWindow()
        {
            InitializeComponent();
            SplineType_ComboBox.ItemsSource = Enum.GetNames(typeof(SplineType)).Select(x => new ComboBoxItem { Content = x });
            SplineType_ComboBox.SelectionChanged += (s, e) => splineType = (SplineType)Enum.Parse(typeof(SplineType), (SplineType_ComboBox.SelectedItem as ComboBoxItem).Content as string);
            SplineType_ComboBox.SelectedIndex = 0;
            FillInputGrid();
        }
        private void FillInputGrid()
        {
            try
            {
                InputGrid.Children.Clear();
                InputGrid.RowDefinitions.Clear();
                Array.Resize(ref getPointValues, points_count);
                for (int i = 0; i < points_count; i++)
                {
                    InputGrid.RowDefinitions.Add(new RowDefinition());
                    TextBox textBox_x = new TextBox { Width = 50, Height = 30, TextAlignment = TextAlignment.Center, Text = "0" };
                    TextBox textBox_y = new TextBox { Width = 50, Height = 30, TextAlignment = TextAlignment.Center, Text = "0" };
                    InputGrid.Children.Add(textBox_x);
                    InputGrid.Children.Add(textBox_y);
                    Grid.SetRow(textBox_x, i);
                    Grid.SetRow(textBox_y, i);
                    Grid.SetColumn(textBox_y, 1);
                    if (getPointValues[i] != null)
                    {
                        var point = getPointValues[i]();
                        textBox_x.Text = point.X.ToString();
                        textBox_y.Text = point.Y.ToString();
                    }
                    getPointValues[i] = () => new Point(double.Parse(textBox_x.Text), double.Parse(textBox_y.Text));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Some error occured in program! Message: " + e.Message);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (points_count >= 10) return;
            points_count++;
            FillInputGrid();
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (points_count <= 2) return;
            points_count--;
            FillInputGrid();
        }


        private void CreateSpline_Button_Click(object sender, RoutedEventArgs e)
        {
            var points = getPointValues.Select(x => x()).ToArray();
            {
                ISplineFunctionBuilder polynomBuilder = new LagrangePolynomBuilder();
                var formula = polynomBuilder.Interpolate(points);

                GraphicPainter graphicPainter = new GraphicPainter();
                var image = graphicPainter.DrawImage(formula, points, new Size(RenderColumn.ActualWidth, RenderRow_1.ActualHeight));
                image.Margin = new Thickness(5);
                SplineGrid_1.Children.Clear();
                SplineGrid_1.Children.Add(image);

                Button button = CreateButton(formula);
                SplineGrid_1.Children.Add(button);
            }
            {
                ISplineFunctionBuilder polynomBuilder = new CubicSplineBuilder(splineType);
                var formula = polynomBuilder.Interpolate(points);

                GraphicPainter graphicPainter = new GraphicPainter();
                var image = graphicPainter.DrawImage(formula, points, new Size(RenderColumn.ActualWidth, RenderRow_2.ActualHeight));
                image.Margin = new Thickness(5);
                SplineGrid_2.Children.Clear();
                SplineGrid_2.Children.Add(image);

                Button button = CreateButton(formula);
                SplineGrid_2.Children.Add(button);
            }
        }

        private Button CreateButton(Function formula)
        {
            Button button = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 5, 0),
                Style = FindResource(/*"MaterialDesignFloatingActionMiniSecondaryButton"*/"MaterialDesignFloatingActionLightButton") as Style,
                Width = 30,
                Height = 30,
                Content = "!"
            };
            button.Click += (s, e) =>
            {
                var window = new InfoWindow(formula.ToString());
                window.Show();
            };
            return button;
        }
    }
}