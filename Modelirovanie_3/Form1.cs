﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Modelirovanie_3
{
    public partial class Form1 : Form
    {
        private enum RandomTypeEnum
        {
            Лемер, Встроенный
        }
        private MathClass mathObject;
        public bool RandomType { get; set; }
        private RandomTypeEnum type;
        public Form1()
        {
            InitializeComponent();
            mathObject = new MathClass(this, 1);
            RandomType = true;
            this.chart1.Palette = ChartColorPalette.Bright;
            this.chart1.Titles.Add("f(x)");
            this.chart1.Series.RemoveAt(0);
            this.chart2.Palette = ChartColorPalette.Bright;
            this.chart2.Titles.Add("F(x)");
            this.chart2.Series.RemoveAt(0);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RandomType = true;
            type = RandomTypeEnum.Встроенный;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RandomType = false;
            type = RandomTypeEnum.Лемер;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            double[] buffArr = mathObject.Start((int)numericSeqLen.Value);
            listView1.Items.Add((listView1.Items.Count + 1 + ") ") + "Mx = " + buffArr[0].ToString("#.###\n") + " Dx = " + buffArr[1].ToString("#.###"));
            Series series_1 = this.chart1.Series.Add(listView1.Items.Count.ToString() + " - " + numericSeqLen.Value + " - " + type);
            Series series_2 = this.chart2.Series.Add(listView1.Items.Count.ToString() + " - " + numericSeqLen.Value + " - " + type);
            series_2.ChartType = SeriesChartType.Line;
            for (int i = 0; i < 100; i++)
            {
                series_1.Points.Add(mathObject.arrM[i]);
                series_2.Points.AddXY(i + 1, mathObject.arrP[i]);
                series_2.Points.AddXY(i + 2, mathObject.arrP[i]);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            mathObject = new MathClass(this, 1);
            chart1.Series.Clear();
            chart2.Series.Clear();
            listView1.Items.Clear();
            labelPi.Text = "P = ";
        }

        private void buttonForPi_Click(object sender, EventArgs e)
        {
            labelPi.Text = "P = " + mathObject.CalculatePi((int)numericForPi.Value);
        }
    }
}
