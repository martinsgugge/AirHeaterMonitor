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
            string sqlQuery = "select tag_id, tag, physical_unit, description from tag";
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Measurement measurmentParameter = new Measurement();
                    measurmentParameter.MeasurementId = Convert.ToInt32(dr["tag_id"]);
                    measurmentParameter.MeasurementName = dr["tag"].ToString();
                    measurmentParameter.MeasurementUnit = dr["physical_unit"].ToString();
                    measurmentParameter.MeasurementDescription = dr["description"].ToString();
                    measurementParameterList.Add(measurmentParameter);
                }
            }
            return measurementParameterList;
        }
    }
}
