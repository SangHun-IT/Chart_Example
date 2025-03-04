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
    public partial class ChartExample_7_Form : TemplateForm
    {
        private DataTable _dataSource;

        public ChartExample_7_Form()
        {
            InitializeComponent();

            SetData();
            SetChart();
        }

        private void SetData()
        {
            this._dataSource = new DataTable();

            string[] years = new string[] { "2004년", "2005년", "2006년" };
            int[,] values = new int[,] { { 9203, 7345, 5036 }, { 11190, 8913, 6237 }, { 13854, 8743, 6151 } };

            this._dataSource.Columns.Add("years");
            this._dataSource.Columns.Add("영업1부");
            this._dataSource.Columns.Add("영업2부");
            this._dataSource.Columns.Add("영업3부");

            for (int i = 0; i < years.Length; i++)
            {
                DataRow row = this._dataSource.NewRow();

                row[0] = years[i];

                for (int j = 0; j < values.GetLength(1); j++)
                    row[j + 1] = values[i, j];

                this._dataSource.Rows.Add(row);
            }

        }

        private void SetChart()
        {
            this.chartControl.DataSource = this._dataSource;

            this.chartControl.Series.Clear();

            this.chartControl.Titles.Add(new Title("영업부서의 년도별 매출 비교", Docking.Top,
                                                    new Font("Malgun Gothic", 20, FontStyle.Bold), Color.Black));

            for (int i = 1; i < this._dataSource.Columns.Count; i++)
            {
                // 가로 막대차트용 Series
                Series columnSeries = new Series(this._dataSource.Columns[i].ColumnName);
                

                columnSeries.XValueMember = "years";
                columnSeries.YValueMembers = this._dataSource.Columns[i].ColumnName;

                // 막대 차트 Series 세팅
                columnSeries.ChartType = SeriesChartType.StackedColumn;
                columnSeries.SetCustomProperty("PointWidth", "0.5");
                columnSeries.BorderWidth = 1;
                columnSeries.BorderColor = Color.Black;
                columnSeries.BorderDashStyle = ChartDashStyle.Solid;

                this.chartControl.Series.Add(columnSeries);

                // 연결선을 위한 영역형 차트 Series
                Series areaSeries = new Series(this._dataSource.Columns[i].ColumnName + "_Area");

                // 연결 선을 위한 영역형 차트 세팅
                areaSeries.ChartType = SeriesChartType.StackedArea;
                areaSeries.BorderWidth = 1;
                areaSeries.BorderColor = Color.Black;
                areaSeries.BorderDashStyle = ChartDashStyle.Solid;
                areaSeries.Color = Color.Transparent;

                for (int j = 0; j < this._dataSource.Rows.Count; j++)
                {
                    areaSeries.Points.AddXY(j + 0.75, Convert.ToDouble(this._dataSource.Rows[j][i]));
                    areaSeries.Points.AddXY(j + 1.25, Convert.ToDouble(this._dataSource.Rows[j][i]));
                }

                areaSeries.IsVisibleInLegend = false;
                areaSeries.XAxisType = AxisType.Secondary;

                this.chartControl.Series.Add(areaSeries);
            }

            ChartArea chArea = this.chartControl.ChartAreas[0];
            // 보조축 세팅
            chArea.AxisX2.MajorGrid.Enabled = false;
            chArea.AxisX2.Minimum = 0.5;
            chArea.AxisX2.Maximum = this._dataSource.Rows.Count + 0.5;
            chArea.AxisX2.MajorTickMark.Enabled = false;
            chArea.AxisX2.LabelStyle.Enabled = false;
            // 배경 컬러 세팅
            chArea.BackColor = Color.LightGray;
            // 주축 세팅
            chArea.AxisX.MajorGrid.Enabled = false;
            chArea.AxisX.Minimum = 0.5;
            chArea.AxisX.Maximum = this._dataSource.Rows.Count + 0.5;
            chArea.AxisX.LabelStyle.Interval = 0.5;
            chArea.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            // Y축 세팅
            chArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chArea.AxisY.Maximum = 35000;
            chArea.AxisY.Interval = 5000;
            chArea.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            chArea.AxisY.LabelStyle.Format = "#,##0;#,##0;-";

            Legend chLegend = this.chartControl.Legends[0];
            chLegend.BorderWidth = 1;
            chLegend.BorderColor = Color.Black;
            chLegend.BorderDashStyle = ChartDashStyle.Solid;
            chLegend.Alignment = StringAlignment.Center;

        }
    }
}
