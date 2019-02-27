using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models
{
    public class EbayLocation
    {
        public Location location { get; set; }
        public string locationAdditionalInformation { get; set; }
        public string locationInstructions { get; set; }
        public string[] locationTypes { get; set; }
        public string locationWebUrl { get; set; }
        public string merchantLocationKey { get; set; }
        public string merchantLocationStatus { get; set; }
        public string name { get; set; }
        public Operatinghour[] operatingHours { get; set; }
        public string phone { get; set; }
        public Specialhour[] specialHours { get; set; }

        public class Location
        {
            public Address address { get; set; }
            public Geocoordinates geoCoordinates { get; set; }
            public string locationId { get; set; }
        }

        public class Address
        {
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string county { get; set; }
            public string postalCode { get; set; }
            public string stateOrProvince { get; set; }
        }

        public class Geocoordinates
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
        }

        public class Operatinghour
        {
            public string dayOfWeekEnum { get; set; }
            public Interval[] intervals { get; set; }
        }

        public class Interval
        {
            public string close { get; set; }
            public string open { get; set; }
        }

        public class Specialhour
        {
            public string date { get; set; }
            public Interval1[] intervals { get; set; }
        }

        public class Interval1
        {
            public string close { get; set; }
            public string open { get; set; }
        }

    }
}
