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
    public partial class ChartExample_6_Form : TemplateForm
    {

        private DataTable _dataSource;

        public ChartExample_6_Form()
        {
            InitializeComponent();

            SetData();
            SetChart();
        }

        private void SetData()
        {
            this._dataSource = new DataTable();

            string[] countries = new string[] { "미국", "캐나다", "일본", "한국" };
            double[,] values = new double[,] { { 0.64, 0.36 }, { 0.394, 0.5 }, { 0.349, 0.617 }, { 0.204, 0.768 } };

            this._dataSource.Columns.Add("국가");
            this._dataSource.Columns.Add("금융자산");
            this._dataSource.Columns.Add("부동산");

            for (int i = 0; i < countries.GetLength(0); i++) 
            { 
                DataRow row = this._dataSource.NewRow();

                row[0] = countries[i];

                for (int j = 0; j < values.GetLength(1); j++)
                {
                    row[j + 1] = values[i, j] * (j == 0 ? -1 : 1);
                } 

                this._dataSource.Rows.Add(row);
            }

        }

        private void SetChart()
        {
            this.chartControl.DataSource = this._dataSource;

            this.chartControl.Series.Clear();

            this.chartControl.Titles.Add(new Title("국가별 자산 비중", Docking.Top,
                                                    new Font("Malgun Gothic", 20, FontStyle.Bold), Color.Black));


            for (int i = 1; i < this._dataSource.Columns.Count; i++)
            {

                Series newSeries = new Series(this._dataSource.Columns[i].ColumnName);

                newSeries.XValueMember = "국가";
                newSeries.YValueMembers = this._dataSource.Columns[i].ColumnName;

                newSeries.ChartType = SeriesChartType.StackedBar;

                newSeries.BorderColor = Color.Black;
                newSeries.BorderDashStyle = ChartDashStyle.Solid;
                newSeries.BorderWidth = 1;

                newSeries.SetCustomProperty("PointWidth", "0.5");

                this.chartControl.Series.Add(newSeries);
            }

            Legend chLegend = this.chartControl.Legends[0];

            chLegend.Docking = Docking.Bottom;
            chLegend.Alignment = StringAlignment.Center;
            chLegend.BorderColor = Color.Black;
            chLegend.BorderWidth = 1;
            chLegend.BorderDashStyle = ChartDashStyle.Solid;

            ChartArea chArea = this.chartControl.ChartAreas[0];

            chArea.BackColor = Color.LightGray;

            chArea.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            chArea.AxisX.MajorGrid.Enabled = false;
            chArea.AxisX.Minimum = 0.5;
            chArea.AxisX.Maximum = this._dataSource.Rows.Count + 0.5;
            chArea.AxisX.LabelStyle.Interval = 0.5;

            chArea.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            chArea.AxisY.Crossing = 0;
            chArea.AxisY.Maximum = 1;
            chArea.AxisY.Minimum = -1;
            chArea.AxisY.Interval = 0.2;
            chArea.AxisY.LabelStyle.Format = "0.0%;0.0%";

        }
    }
}
