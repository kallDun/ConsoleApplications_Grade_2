using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalcMethodLab2
{
    public partial class MainWindow : Window
    {
        private delegate double GetDoubleValue();
        private delegate void SetDoubleValue(double value);
        private int matrix_size;
        private GetDoubleValue[][] matrix;
        private SetDoubleValue[] setter_matrix;
        private double[] roots;
        private bool has_results;
        private readonly IMatrixCalculationMethod calculationMethod;

        public MainWindow()
        {
            InitializeComponent();
            matrix_size = int.Parse(TextBox_MatrixSize.Text);
            InitializeMatrix();
            calculationMethod = new SeidelMatrixCalcMethod(epsilon: 1e-2);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TextBox_MatrixSize.Text, out int size) && size > 0 && size < 10)
            {
                if (matrix_size != size)
                {
                    matrix_size = size;
                    InitializeMatrix();
                }
            }
            else
            {
                TextBox_MatrixSize.Text = matrix_size.ToString();
            }
        }
        private void InitializeMatrix()
        {
            has_results = false;
            MatrixGrid.Children.Clear();
            OutputGrid.Children.Clear();
            MatrixGrid = new Grid();
            OutputGrid = new Grid();
            OutputGrid.SetValue(Grid.ColumnProperty, 1);
            MainGrid.Children.Add(MatrixGrid);
            MainGrid.Children.Add(OutputGrid);

            matrix = new GetDoubleValue[matrix_size][];
            setter_matrix = new SetDoubleValue[matrix_size];

            for (int i = 0; i < matrix_size; i++)
            {
                MatrixGrid.RowDefinitions.Add(new RowDefinition());
                MatrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
                OutputGrid.RowDefinitions.Add(new RowDefinition());
                matrix[i] = new GetDoubleValue[matrix_size + 1];
            }
            MatrixGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < matrix_size; i++)
            {
                for (int j = 0; j < matrix_size + 1; j++)
                {
                    var textbox = new TextBox
                    {
                        Width = 70,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Text = "0",
                        FontSize = 25,
                        BorderThickness = new Thickness(0),
                        TextAlignment = TextAlignment.Right
                    };
                    var textblock = new TextBlock
                    {
                        Width = 20,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Text = $"x{j + 1}",
                        FontSize = 16
                    };
                    if (j == matrix_size)
                    {
                        textbox.HorizontalAlignment = HorizontalAlignment.Right;
                        textblock.HorizontalAlignment = HorizontalAlignment.Left;
                        textblock.Text = "=";
                    }
                    var grid = new Grid()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 90,
                        Height = 50
                    };
                    grid.SetValue(Grid.ColumnProperty, j);
                    grid.SetValue(Grid.RowProperty, i);
                    grid.Children.Add(textbox);
                    grid.Children.Add(textblock);
                    MatrixGrid.Children.Add(grid);

                    textbox.TextChanged += TextBox_Validation;
                    matrix[i][j] = () => double.Parse(textbox.Text);
                }
            }

            for (int i = 0; i < matrix_size; i++)
            {
                var textbox = new TextBox
                {
                    IsReadOnly = true,
                    Width = 140,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Text = ". . .",
                    FontSize = 25,
                    BorderThickness = new Thickness(0),
                    TextAlignment = TextAlignment.Left
                };
                var textblock = new TextBlock
                {
                    Width = 30,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Text = $"x{i + 1} =",
                    FontSize = 16
                };
                var grid = new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Height = 50,
                    Width = 170
                };
                grid.SetValue(Grid.RowProperty, i);
                grid.Children.Add(textbox);
                grid.Children.Add(textblock);
                OutputGrid.Children.Add(grid);

                setter_matrix[i] = (s) => textbox.Text = string.Format("{0:0.########}", s);
            }
        }
        private void TextBox_Validation(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!double.TryParse(textBox.Text, out double num)) textBox.Text = "0";
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            var results = calculationMethod.FindRoots(matrix.Select(x => x.Select(s => s()).ToArray()).ToArray());
            for (int i = 0; i < setter_matrix.Length; i++)
            {
                setter_matrix[i](results[i]);
            }
            has_results = true;
            roots = results;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!has_results) return;
            var deltas = matrix.Select((item_i, i) => item_i
                .Select((item_i_j, j) => 
                {
                    if (j == matrix_size) return -item_i_j();
                    else return item_i_j() * roots[j];
                }).Sum());
            var text = string.Join("\n", deltas.Select((item, index) => string.Format("Δx{0:0} = {1:0.##########}", index + 1, item)));
            MessageBox.Show(text);
        }
    }
}