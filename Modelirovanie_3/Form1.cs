using System;
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
        private MathClass mathObject;
        private bool _randomType;
        public Form1()
        {
            InitializeComponent();
            mathObject = new MathClass(this);
            _randomType = true;
            this.chart1.Palette = ChartColorPalette.SeaGreen;
            this.chart1.Titles.Add("f(x)");
            this.chart1.Series.RemoveAt(0);
            this.chart2.Palette = ChartColorPalette.Fire;
            this.chart2.Titles.Add("F(x)");
            this.chart2.Series.RemoveAt(0);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _randomType = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _randomType = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (_randomType)
            {
                double[] buffArr = mathObject.Start((int) numericSeqLen.Value);
                listView1.Items.Add((listView1.Items.Count + 1 +") ") + "Mx = " + buffArr[0] + "\n" +" Dx = " + buffArr[1]);
            }
            Series series_1 = this.chart1.Series.Add(listView1.Items.Count.ToString() +" - " + numericSeqLen.Value);
            Series series_2 = this.chart2.Series.Add(listView1.Items.Count.ToString() +" - " + numericSeqLen.Value);
            series_2.ChartType = SeriesChartType.Line;
            for (int i = 0; i < 100; i++)
            {
                series_1.Points.Add(mathObject.arrM[i]);
                series_2.Points.AddXY(i, mathObject.arrP[i]);
                series_2.Points.AddXY(i+1, mathObject.arrP[i]);

            }
        }
    }
}
