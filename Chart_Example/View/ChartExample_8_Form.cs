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
    public partial class ChartExample_8_Form : TemplateForm
    {
        private DataTable _dataSource;

        public ChartExample_8_Form()
        {
            InitializeComponent();

            SetData();
            SetChart();
        }

        private void SetData()
        {
            this._dataSource = new DataTable();

            string[] items = new string[] { "컴퓨터", "MP3", "가전", "자동차" };
            double[] values = new double[] { 0.3, 0.02, 0.38, 0.3 };

            this._dataSource.Columns.Add("items");
            this._dataSource.Columns.Add("value");

            for (int i = 0; i < items.Length; i++)
            {
                DataRow row = this._dataSource.NewRow();

                row[0] = items[i];
                row[1] = values[i]; 

                this._dataSource.Rows.Add(row);
            }

        }

        private void SetChart()
        {
            this.chartControl.DataSource = this._dataSource;

            this.chartControl.Titles.Add(new Title("사업 부문별 매출", Docking.Top,
                                                    new Font("Malgun Gothic", 20, FontStyle.Bold), Color.Black));

            Legend chLegend = this.chartControl.Legends[0];
            chLegend.BorderWidth = 1;
            chLegend.BorderColor = Color.Black;
            chLegend.BorderDashStyle = ChartDashStyle.Solid;
            chLegend.Alignment = StringAlignment.Center;

            Series chSeries = this.chartControl.Series[0];

            chSeries.XValueMember = "items";
            chSeries.YValueMembers = "value";

            chSeries.ChartType = SeriesChartType.Pie;

            chSeries.BorderColor = Color.Black;
            chSeries.BorderDashStyle = ChartDashStyle.Solid;
            chSeries.BorderWidth = 1;

            chSeries.SetCustomProperty("PieStartAngle", "270");
            chSeries.SetCustomProperty("PieLabelStyle", "OutSide");

            chSeries.Label = "#VALX \r\n #VALY \r\n #PERCENT{0.0%}";
            chSeries.LegendText = "#VALX";

        }
    }
}
