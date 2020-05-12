namespace _10DemoUI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Core;

    public partial class Form1 : Form
    {
        private const string Path = "..\\..\\..\\cars.csv";

        public delegate void AppendText(string textToAppend);

        private readonly AppendText appendToContentDelegate;
        private readonly AppendText loggingDelegate;

        public Form1()
        {
            this.InitializeComponent();
            this.appendToContentDelegate = this.AppendToContent;
            this.loggingDelegate = this.Log;
        }

        private void GetDataBtn_Click(object sender, EventArgs e)
        {
            this.Log("start to process file");

            Thread t = new Thread(() => {
                var cars = this.ProcessCarsFile(Path).ToList();
                this.DisplayCars(cars);
                this.logTbx.Invoke(loggingDelegate, $"finish to process file. {cars.Count()} cars downloaded");
                //this.Log($"finish to process file. {cars.Count()} cars downloaded");
            });

            t.Start();

            //var cars = this.ProcessCarsFile(Path).ToList();


        }

        private void DisplayCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                //this.AppendToContent(car.ToString());
                this.contentTxb.Invoke(appendToContentDelegate, car.ToString());
            }
        }

        private IEnumerable<Car> ProcessCarsFile(string filePath)
        {
            var cars = new List<Car>(600);
            var lines = File.ReadAllLines(filePath).Skip(2);

            foreach (var line in lines)
            {
                cars.Add(Car.Parse(line));
            }

            Thread.Sleep(TimeSpan.FromSeconds(3)); // simulate some work

            return cars;
        }

        public void Log(string s)
        {
            this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}");
        }

        public void AppendToContent(string s)
        {
            this.contentTxb.AppendText($"{s}{Environment.NewLine}");

            //Thread.Sleep(TimeSpan.FromSeconds(3)); // simulate other work
        }
    }
}
