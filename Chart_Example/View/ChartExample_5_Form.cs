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
    public partial class ChartExample_5_Form : TemplateForm
    {
        private DataTable dataSource;

        public ChartExample_5_Form()
        {
            InitializeComponent();

            this.SetData();
            this.SetChart();
        }

        private void SetData()
        {
            this.dataSource = new DataTable("ChartData");

            string[] subjects = new string[] { "Excel", "Access", "Power Point", "Word", "Visio" };
            int[,] values = new int[,] {
                                    {9, 4, 5},
                                    {8, 8, 5},
                                    {5, 10, 7},
                                    {5, 10, 6},
                                    {4, 5, 10}
                                };

            this.dataSource.Columns.Add("실무");
            this.dataSource.Columns.Add("오일남");
            this.dataSource.Columns.Add("홍길동");
            this.dataSource.Columns.Add("김덕수");

            for (int i = 0; i < subjects.Length; i++)
            {
                DataRow row = this.dataSource.NewRow();

                row[0] = subjects[i];

                for (int j = 0; j < values.GetLength(1); j++)
                {
                    row[j + 1] = values[i, j];
                }

                this.dataSource.Rows.Add(row);
            }
        }

        private void SetChart()
        {
            this.chartControl.DataSource = this.dataSource;

            this.chartControl.Titles.Add(
                new Title("직원의 사무 기술 능력 점수", Docking.Top,
                            new Font("Malgun Gothic", 20, FontStyle.Bold), Color.Black)
                );

            this.chartControl.Series.RemoveAt(0);

            for (int i = 1; i < this.dataSource.Columns.Count; i++)
            {
                Series chartSeries = new Series(this.dataSource.Columns[i].ColumnName);

                chartSeries.XValueMember = "실무";
                chartSeries.YValueMembers = this.dataSource.Columns[i].ColumnName;
                chartSeries.ChartType = SeriesChartType.Radar;

                chartSeries.SetCustomProperty("RadarDrawingStyle", "Line");

                chartSeries.MarkerStyle = MarkerStyle.Square;
                chartSeries.MarkerSize = 15;
                chartSeries.BorderWidth = 2;

                this.chartControl.Series.Add(chartSeries);
            }

            ChartArea chArea = this.chartControl.ChartAreas[0];

            chArea.AxisY.MajorGrid.Enabled = false;
            chArea.AxisY.MajorTickMark.Enabled = false;

            chArea.BackColor = Color.Transparent;

            Legend chLegend = this.chartControl.Legends[0];

            chLegend.BorderColor = Color.DarkGray;
            chLegend.BorderWidth = 1;
            chLegend.BackColor = Color.Transparent;
            chLegend.Alignment = StringAlignment.Center;
            chLegend.Docking = Docking.Bottom;

        }
    }
}
