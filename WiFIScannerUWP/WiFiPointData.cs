using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiScannerUWP
{
    /// <summary>
    /// Class to contain data about a WiFi collection point
    /// </summary>
    public class WiFiPointData
    {
        public DateTimeOffset TimeStamp { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Accuracy { get; set; }

        public List<WiFiSignal> WiFiSignals { get; set; }

        public WiFiPointData()
        {
            this.WiFiSignals = new List<WiFiSignal>();
        }
    }
}
