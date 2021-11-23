using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTutorial1.Models;

namespace RazorTutorial1.Pages
{
    public class MeasurementModel : PageModel
    {
        readonly IConfiguration _configuration;
        public string cs;
        public MeasurementModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Measurement> measurementParameterList = new List<Measurement>();
        
        public void OnGet()
        {
            Measurement meas = new Measurement();
            cs = _configuration.GetConnectionString("DBCon");
            measurementParameterList = meas.GetMeasurmentParameters(cs);
        }
    }
}
