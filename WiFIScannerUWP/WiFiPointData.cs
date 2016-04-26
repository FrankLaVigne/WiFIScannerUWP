using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFIScannerUWP
{
    public class WiFiPointData
    {
        public string Ssid { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public byte SignalBars { get; set; }

        public DateTimeOffset Timestamp { get; }

        public string NetworkKind { get; set; }

        public string PhysicalKind { get; set; }

        public double ChannelCenterFrequencyInKilohertz { get; set; }


    }
}
