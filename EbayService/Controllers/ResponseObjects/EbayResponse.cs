using System;
using System.Collections.Generic;

namespace EbayService.Controllers.ResponseObjects
{
    public class EbayResponse
    {
        public List<Warning> warnings { get; set; }
        public class Parameter
        {
            public string value { get; set; }
            public string name { get; set; }
        }

        public class Warning
        {
            public int errorId { get; set; }
            public string domain { get; set; }
            public string subdomain { get; set; }
            public string category { get; set; }
            public string message { get; set; }
            public List<Parameter> parameters { get; set; }
            public string longMessage { get; set; }
            public List<string> inputRefIds { get; set; }
            public List<string> outputRefIds { get; set; }
        }
    }
}
