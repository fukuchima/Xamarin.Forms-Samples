﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexChartDemo.Data.Model;
using Xamarin.Forms;
using Xuni.Forms.FlexChart;
using FlexChartDemo.Data.Resources;

namespace FlexChartDemo.Data.Views.Samples
{
    public partial class DynamicChartsSample
    {
        internal Random random = new Random();
        List<DummyObject> list = new List<DummyObject>();

        public DynamicChartsSample()
        {
            InitializeComponent();
            Title = AppResources.DynamicChartTitle;

            this.flexChart.ItemsSource = ChartSampleFactory.CreateEntityList();

            for (int i = 0; i < 8; i++)
            {
                list.Add(getItem());
            }
            this.flexChart.ItemsSource = list;
            this.flexChart.Palette = Xuni.Forms.ChartCore.ChartPalettes.Coral;
            this.flexChart.ShowTooltips = false;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            Device.StartTimer(TimeSpan.FromSeconds(Device.OnPlatform<Double>(0.5, 1.0, 0.5)), () =>
            {
                if (list.Count > 30)
                    list.RemoveAt(0);
                list.Add(getItem());
                flexChart.ItemsSource = list.ToArray();
                return true;
            });
        }
        public DummyObject getItem()
        {
            double nextDouble = random.Next(0, 1000);

            while (nextDouble < 900)
            {
                nextDouble = random.Next(0, 1000);
            }

            double trucks = nextDouble + 20;
            double ships = nextDouble + 10;
            double planes = nextDouble;

            DateTime now = DateTime.Now;

            return new DummyObject(now, now.ToString("mm:ss"), trucks, ships, planes);
        }

        public class DummyObject
        {
            public String Name { get; set; }

            public DateTime Time { get; set; }

            public double Trucks { get; set; }

            public double Ships { get; set; }

            public double Planes { get; set; }

            public DummyObject(DateTime time, String name, double trucks, double ships, double planes)
            {
                this.Time = time;
                this.Name = name;
                this.Trucks = trucks;
                this.Ships = ships;
                this.Planes = planes;
            }

        }
    }
}
