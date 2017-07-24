using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;
using System.Runtime.InteropServices;
using OxyPlot;
using OxyPlot.Series;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        string testData;
        int[,] matrix;

        DispatcherTimer dataTimer1;
        DispatcherTimer dataTimer2;
        DispatcherTimer dataTimer3;

        public PlotModel MyModel { get; private set; }

        [DllImport("testDLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetRandomValue();

        [DllImport("testDLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetRandomTable(int m, int n, int[] table);

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.TestData = "hi";
            Trace.WriteLine(this.TestData);

            this.MyModel = new PlotModel {Title = "Example 1" };
            this.MyModel.Series.Add(new ScatterSeries());

            this.UpdateTable(null, null);
            this.ChangeText(null, null);
            this.UpdatePlot(null, null);
        }

        public void UpdatePlot(object sender, EventArgs e)
        {
            int x = GetRandomValue();
            int y = (int)Math.Pow(x, 2);
            (this.MyModel.Series[0] as ScatterSeries).Points.Add(new ScatterPoint(x, y));
            MyModel.InvalidatePlot(true);
            // this.TestData = new Random().Next().ToString();
            //Trace.WriteLine(this.TestData);
            if (this.dataTimer3 == null)
            {
                this.dataTimer3 = new DispatcherTimer();
                this.dataTimer3.Tick += new EventHandler(this.UpdatePlot);
                this.dataTimer3.Interval = TimeSpan.FromMilliseconds(100);
            }
            this.dataTimer3.Stop();
            this.dataTimer3.Start();
        }

        public void ChangeText(object sender, EventArgs e)
        {
            this.TestData = GetRandomValue().ToString();
            // this.TestData = new Random().Next().ToString();
            //Trace.WriteLine(this.TestData);
            if (this.dataTimer1 == null)
            {
                this.dataTimer1 = new DispatcherTimer();
                this.dataTimer1.Tick += new EventHandler(this.ChangeText);
                this.dataTimer1.Interval = TimeSpan.FromMilliseconds(1000);
            }
            this.dataTimer1.Stop();
            this.dataTimer1.Start();
        }

        public void UpdateTable(object sender, EventArgs e)
        {
            int m = 10;
            int n = 2;
            int[] values = new int[m*n];
            GetRandomTable(m, n, values);
            int[,] _matrix = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    _matrix[i, j] = values[2 * i + j];
                }
            }
            this.Matrix = _matrix;
            //Trace.WriteLine(this.Matrix.Length);
            if (this.dataTimer2 == null)
            {
                this.dataTimer2 = new DispatcherTimer();
                this.dataTimer2.Tick += new EventHandler(this.UpdateTable);
                this.dataTimer2.Interval = TimeSpan.FromMilliseconds(1000);
            }
            this.dataTimer2.Stop();
            this.dataTimer2.Start();
        }

        public string TestData
        {
            get => testData;
            set
            {
                testData = value;
                NotifyPropertyChanged("TestData");
            }
        }

        public int[,] Matrix
        {
            get => matrix;
            set
            {
                matrix = value;
                NotifyPropertyChanged("Matrix");
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
