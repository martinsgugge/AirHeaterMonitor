using System;
using System.Collections.Generic;
using System.Data;
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
        public List<ChartData> GetChartData(string cs, int tag_id, string start, string stop)
        {
            List<ChartData> chartDataList = new List<ChartData>();

            NpgsqlConnection con = new NpgsqlConnection(cs);
            string selectSQL = "select * from get_tag_measurements(@i_tag_id, @i_start, @i_stop);";
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand(selectSQL, con);
            //cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("i_tag_id", tag_id);
            cmd.Parameters.AddWithValue("i_start", start);
            cmd.Parameters.AddWithValue("i_stop", stop);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    ChartData chartData = new ChartData();
                    chartData.ChartTimeStamp = dr["o_meas_time"].ToString();
                    chartData.ChartValue = Convert.ToDouble(dr["o_meas_value"]);
                    chartDataList.Add(chartData);
                }
            }

            con.Close();
            return chartDataList;
        }
        public List<ChartData> GetChartData(string cs, int tag_id, DateTime start, DateTime stop)
        {
            List<ChartData> chartDataList = new List<ChartData>();

            NpgsqlConnection con = new NpgsqlConnection(cs);
            string selectSQL = "select * from get_tag_measurements(@i_tag_id, @i_start, @i_stop);";
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand(selectSQL, con);
            //cmd.CommandType = CommandType.StoredProcedure;
            NpgsqlParameter p1 = new NpgsqlParameter("i_start", NpgsqlTypes.NpgsqlDbType.Timestamp);
            p1.Value = start;
            NpgsqlParameter p2 = new NpgsqlParameter("i_stop", NpgsqlTypes.NpgsqlDbType.Timestamp);
            p2.Value = stop;
            cmd.Parameters.AddWithValue("i_tag_id", tag_id);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    ChartData chartData = new ChartData();
                    chartData.ChartTimeStamp = dr["o_meas_time"].ToString();
                    chartData.ChartValue = Convert.ToDouble(dr["o_meas_value"]);
                    chartDataList.Add(chartData);
                }
            }

            con.Close();
            return chartDataList;
        }
    }
}
