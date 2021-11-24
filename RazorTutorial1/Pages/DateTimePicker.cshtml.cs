using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTutorial1.Models;

namespace RazorTutorial1.Pages
{
    public class DateTimePickerModel : PageModel
    {
        public Measurement meas = new Measurement();
        public string dateTimeEnd;
        public string dateTimeStart;
        [BindProperty]
        public DateTime DateTimeStart { get; set; }
        [BindProperty]
        public DateTime DateTimeEnd { get; set; }
        
        public void OnGet()

        {
            string tag = Request.Query["tag"];
            ParseURL(tag);
        }
        public void OnPost()
        {
            //this.dateTimeStart = String.Format("yyyy-MM-dd HH:mm:ss", DateTimeStart);
            //this.dateTimeEnd = String.Format("yyyy-MM-dd HH:mm:ss", DateTimeEnd);
            //this.dateTimeStart = DateTimeStart.ToString("yyyy-MM-dd HH:mm:ss");
            //this.dateTimeEnd = DateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss");
            //this.DateTimeStart = Convert.ToDateTime(DateTimeStart.ToString("yyyy-MM-dd HH:mm:ss"));
            //this.DateTimeEnd = Convert.ToDateTime(DateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss"));
            string tag = Request.Query["tag"];
            ParseURL(tag);
            //this.DateTimeEnd = DateTimeEnd;
        }
        private void ParseURL(string url)
        {
            string[] temp = url.Split(";");
            meas.MeasurementName = temp[0];
            meas.MeasurementUnit = temp[1];
            meas.MeasurementDescription = temp[2];
            meas.MeasurementId = Convert.ToInt32(temp[3]);
        }
    }
}
