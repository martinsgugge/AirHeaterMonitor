using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTutorial1.Models;
using Microsoft.Extensions.Configuration;

namespace RazorTutorial1.Pages
{
    public class LineChartModel : PageModel
    {
        public List<ChartData> chartDataList = new List<ChartData>();
        public Measurement meas = new Measurement();
        public string dateTimeStart;
        public string dateTimeEnd;
        public DateTime DateTimeStart;
        public DateTime DateTimeEnd;
        string cs;
        readonly IConfiguration _configuration;
        public LineChartModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        
        public void OnPost()
        {
            
        }
        public void OnGet()
        {
            string tag = Request.Query["tag"];
            ParseURL(tag);
            cs = _configuration.GetConnectionString("DBCon");
            //measurementParameterList = meas.GetMeasurmentParameters(cs, tagname);
            //this.tagname = "TT1";
            chartDataList = ChartData();
        }
        public List<ChartData> ChartData()
        {
            
            cs = _configuration.GetConnectionString("DBCon");
            List<ChartData> chartDataList = new List<ChartData>();
            ChartData chartData = new ChartData();
            chartDataList = chartData.GetChartData(cs, this.meas.MeasurementId, this.DateTimeStart, this.DateTimeEnd);
            this.chartDataList = chartDataList;
            return chartDataList;
        }       
        public List<Measurement> measurementParameterList = new List<Measurement>();
        private void ParseURL(string url)
        {
            string[] temp = url.Split(";");
            meas.MeasurementName = temp[0];
            meas.MeasurementUnit = temp[1];
            meas.MeasurementDescription = temp[2];
            meas.MeasurementId = Convert.ToInt32(temp[3]);
            //dateTimeStart = temp[4];
            //dateTimeEnd = temp[5];
            DateTimeStart = Convert.ToDateTime(temp[4]);
            DateTimeEnd = Convert.ToDateTime(temp[5]);

        }
    }
}
