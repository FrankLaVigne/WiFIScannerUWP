using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiScannerUWP
{
    public class WiFiPointData
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTimeOffset Timestamp { get; }

        public List<WiFiSignal> WiFiSignals { get; set; }

        public WiFiPointData()
        {
            this.WiFiSignals = new List<WiFiSignal>();
        }

    }
}
