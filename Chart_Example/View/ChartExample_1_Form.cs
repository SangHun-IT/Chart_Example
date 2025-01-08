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
    public partial class ChartExample_1_Form : TemplateForm
    {
        private DataTable dataSource;

        public ChartExample_1_Form()
        {
            InitializeComponent();

            this.SetData();
            this.SetChart();
        }

        private void SetData()
        {
            this.dataSource = new DataTable("ChartData");

            string[] years = new string[] { "2000년", "2001년", "2002년", "2003년", "2004년", "2005년", "2006년" };
            int[] prices = new int[] { 800000, 1800000, 2400000, 1420000, 1920000, 3200000, 4800000 };

            this.dataSource.Columns.Add("Year");
            this.dataSource.Columns.Add("Price");

            for (int i = 0; i < years.Length; i++)
            {
                DataRow row = this.dataSource.NewRow();

                row[0] = years[i];
                row[1] = prices[i];

                this.dataSource.Rows.Add(row);
            }
        }

        private void SetChart()
        {
            this.chartControl.DataSource = this.dataSource;

            this.chartControl.Series[0].XValueMember = "Year";
            this.chartControl.Series[0].YValueMembers = "Price";

            this.chartControl.Titles.Add(
                new Title("2000년 ~ 2006년 매출 실적 추이", Docking.Top,
                            new Font("Malgun Gothic", 20, FontStyle.Bold), Color.Black)
                );

            Series chartSeries = this.chartControl.Series[0];

            chartSeries.ChartType = SeriesChartType.Line;
            chartSeries.MarkerStyle = MarkerStyle.Diamond;
            chartSeries.MarkerSize = 10;
            chartSeries.MarkerColor = Color.Blue;

            ChartArea chArea = this.chartControl.ChartAreas[0];

            chArea.AxisX.MajorGrid.Enabled = false;
            chArea.AxisX.LabelStyle.IntervalOffset = 0.5;
            chArea.AxisX.Minimum = 0.5;
            chArea.AxisX.Maximum = this.dataSource.Rows.Count + 0.5;
            chArea.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;

            chArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chArea.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            chArea.AxisY.Maximum = 6000000;
            chArea.AxisY.Interval = 1000000;
            chArea.AxisY.LabelStyle.Format = "#,##0;#,##0;-";

            chArea.BackColor = Color.LightGray;

            chartSeries.IsValueShownAsLabel = true;
            chartSeries.LabelFormat = "#,##0;#,##0;-";

            this.chartControl.Legends.RemoveAt(0);
        }
    }
}
