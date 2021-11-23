using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace RazorTutorial1.Models
{
    public class ChartData
    {
        public int ChartDataId { get; set; }
        public string ChartTimeStamp { get; set; }
        public double ChartValue { get; set; }
        public List<ChartData> GetChartData(string cs, string tagname)
        {
            List<ChartData> chartDataList = new List<ChartData>();
            NpgsqlConnection con = new NpgsqlConnection(cs);
            string selectSQL = string.Format("SELECT tag_id, meas_time::timestamp(0), meas_value FROM measurement where tag_id = (select tag_id from tag where tag = '{0}')" +
                "order by meas_time;", tagname);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(selectSQL, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    ChartData chartData = new ChartData();
                    chartData.ChartDataId = Convert.ToInt32(dr["tag_id"]);
                    chartData.ChartTimeStamp = dr["meas_time"].ToString();
                    chartData.ChartValue = Convert.ToDouble(dr["meas_value"]);
                    chartDataList.Add(chartData);
                }
            }
            return chartDataList;
        }
    }
}
