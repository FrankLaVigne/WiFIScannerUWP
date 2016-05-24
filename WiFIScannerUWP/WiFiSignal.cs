using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiScannerUWP
{
    public class WiFiSignal
    {
        public string MacAddress { get; set; }

        public string Ssid { get; set; }

        public byte SignalBars { get; set; }

        public string NetworkKind { get; set; }

        public string PhysicalKind { get; set; }

        public double ChannelCenterFrequencyInKilohertz { get; set; }

        public string Encryption { get; set; }

        //TODO: add security details
    }
}
