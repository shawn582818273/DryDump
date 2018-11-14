using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
namespace DryDump
{
    public class FormRelated
    {
        public ArrayList DataTableToArrayList(DataTable dt)
        {
            ArrayList arrList = new ArrayList();
            ArrayList yDataList = new ArrayList();
            int cou = dt.Columns.Count;
            for (int i = 1; i <= cou; i++)
            {
                arrList.Add(new ArrayList());
                yDataList.Add(new ArrayList());
                //MessageBox.Show("i=="+i);
            }
            foreach (DataRow dr in dt.Rows)
            {
                for (int j = 0; j < cou; j++)
                {
                    ((ArrayList)arrList[j]).Add(dr[j]);
                    //MessageBox.Show("cou=="+cou+"  when j="+j+"  arrList["+j+"]="+arrList[j]+"   and  dr["+j+"]=="+dr[j].ToString());
                }

            }
            for (int j = 0; j < cou; j++)
            {
                yDataList[j] = (object[])((ArrayList)arrList[j]).ToArray(typeof(object));
            }
            return yDataList;
        }
        public bool DataToChart(Chart chart2,Series series,object[] xray,object[] yray, SeriesChartType sct)
        {
            try
            {

                //yDataList[j] = (object[])((ArrayList)arrList[j]).ToArray(typeof(object));
                chart2.ChartAreas[0].Axes[0].LabelStyle.Format = "MM-dd HH:mm:ss";
                chart2.ChartAreas[0].Axes[0].MajorGrid.IntervalType = DateTimeIntervalType.Seconds;
                chart2.ChartAreas[0].Axes[0].MajorGrid.Interval = 1;
                chart2.ChartAreas[0].Axes[0].ScaleView.Size = 8;
                chart2.ChartAreas[0].Axes[0].ScrollBar.IsPositionedInside = false;
                chart2.ChartAreas[0].Axes[0].ScrollBar.Enabled = true;
                ////chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
                //chart2.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;
                ////chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;

                //chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                //chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                series.ChartType =sct;                   
                series.Points.DataBindXY(xray,yray);
                //MessageBox.Show("j="+j+"  and yDataList["+j+"]=="+yDataList[j]);
                
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex);
                return false;
            }
            
        }
    }
}
