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

namespace Chart_Example.View
{
    public partial class ChartExample_4_Form : TemplateForm
    {
        private DataTable dataSource;

        public ChartExample_4_Form()
        {
            InitializeComponent();

            this.SetData();
            this.SetChart();
        }

        private void SetData()
        {
            this.dataSource = new DataTable("ChartData");

            string[] columns = new string[] { "한국", "싱가포르", "유럽", "일본", "중국ㆍ홍콩", "미국"};
            int[] values = new int[] { 3800, 3290, 3170, 2780, 2600, 2300 };

            this.dataSource.Columns.Add("Country");
            this.dataSource.Columns.Add("Price");

            for (int i = 0; i < columns.Length; i++)
            {
                DataRow row = this.dataSource.NewRow();

                row[0] = columns[i];
                row[1] = values[i];

                this.dataSource.Rows.Add(row);
            }
        }

        private void SetChart()
        {
            this.chartControl.Titles.Add(
                new Title("□□벅스 카페라떼 가격 국가별 비교", Docking.Top,
                            new Font("Malgun Gothic", 20, FontStyle.Bold), Color.Black)
                );

            this.chartControl.Legends.RemoveAt(0);

            ChartArea chArea = this.chartControl.ChartAreas[0];

            chArea.BackColor = Color.LightGray;

            chArea.AxisX.MajorGrid.Enabled = false;
            chArea.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            chArea.AxisX.Minimum = 0.5;
            chArea.AxisX.Maximum = this.dataSource.Rows.Count + 0.5;
            chArea.AxisX.LabelStyle.IntervalOffset = 0.5;

            chArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chArea.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            chArea.AxisY.Interval = 500;
            chArea.AxisY.LabelStyle.Format = "#,##0;#,##0;-";

            Series chartSeries = this.chartControl.Series[0];

            chartSeries.Color = Color.LightBlue;
            chartSeries.BorderColor = Color.Black;
            chartSeries.IsValueShownAsLabel = true;
            chartSeries.SetCustomProperty("PointWidth", "0.5");
            chartSeries.LabelFormat = "#,##0;#,##0;-";

            for (int i = 0; i < this.dataSource.Rows.Count; i++)
            {
                string country = this.dataSource.Rows[i][0].ToString();
                int price = int.Parse(this.dataSource.Rows[i][1].ToString());

                chartSeries.Points.AddXY(country, price);
            }

            chartSeries.Points.FindMaxByValue().Color = Color.Orange;
        }

        private void chartControl_MouseClick(object sender, MouseEventArgs e)
        {
            var target = this.chartControl.HitTest(e.X, e.Y);

            if(target.ChartElementType == ChartElementType.DataPoint)
            {
                int pointIdx = target.PointIndex;

                Series chartSr = this.chartControl.Series[0];

                foreach(var point in chartSr.Points)
                    point.Color = Color.LightBlue;

                chartSr.Points.FindMaxByValue().Color = Color.Orange;

                chartSr.Points[pointIdx].Color = Color.Blue;
            }

            
        }
    }
}
