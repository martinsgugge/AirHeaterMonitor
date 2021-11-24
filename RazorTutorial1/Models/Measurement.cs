using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;

namespace RazorTutorial1.Models
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementUnit { get; set; }
        public string MeasurementDescription { get; set; }
        public List<Measurement> GetMeasurmentParameters(string connectionString)
        {
            List<Measurement> measurementParameterList = new List<Measurement>();
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            string sqlQuery = "select * from get_tags()";
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Measurement measurmentParameter = new Measurement();
                    measurmentParameter.MeasurementId = Convert.ToInt32(dr["o_tag_id"]);
                    measurmentParameter.MeasurementName = dr["o_tag"].ToString();
                    measurmentParameter.MeasurementUnit = dr["o_physical_unit"].ToString();
                    measurmentParameter.MeasurementDescription = dr["o_description"].ToString();
                    measurementParameterList.Add(measurmentParameter);
                }
            }
            return measurementParameterList;
        }
    }
}
