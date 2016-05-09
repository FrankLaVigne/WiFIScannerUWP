using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.WiFi;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System.Text;
using Windows.Devices.Geolocation;

namespace WiFiScannerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private WiFiScanner _wifiScanner;

        public MainPage()
        {
            this.InitializeComponent();

            this._wifiScanner = new WiFiScanner();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeScanner();
        }

        private async Task InitializeScanner()
        {
            await this._wifiScanner.InitializeScanner();
        }

        private async void btnScan_Click(object sender, RoutedEventArgs e)
        {
            this.btnScan.IsEnabled = false;

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 30);
            //timer.Tick += Timer_Tick;
            //timer.Start();



            try
            {
                StringBuilder networkInfo = await RunWifiScan();
                this.txbReport.Text = networkInfo.ToString();

            }
            catch (Exception ex)
            {

            }

            this.btnScan.IsEnabled = true;

        }

        private async void Timer_Tick(object sender, object e)
        {
            try
            {
                StringBuilder networkInfo = await RunWifiScan();
                this.txbReport.Text = networkInfo.ToString();

            }
            catch (Exception  ex)
            {

                //throw;
            }

            //StringBuilder networkInfo = await RunWifiScan();
            //this.txbReport.Text += networkInfo.ToString();
        }

        private async Task<StringBuilder> RunWifiScan()
        {
            await this._wifiScanner.ScanForNetworks();

            var accessStatus = await Geolocator.RequestAccessAsync();

            Geolocator geolocator = new Geolocator();

            Geoposition position = await geolocator.GetGeopositionAsync();
            
            var report = this._wifiScanner.WiFiAdapter.NetworkReport;

            StringBuilder networkInfo = new StringBuilder();

            var wifiPoint = new WiFiPointData()
            {
                Latitude = position.Coordinate.Point.Position.Latitude,
                Longitude = position.Coordinate.Point.Position.Longitude,
                Accuracy = position.Coordinate.Accuracy,
                TimeStamp = position.Coordinate.Timestamp
            };


            foreach (var availableNetwork in report.AvailableNetworks)
            {
                WiFiSignal wifiSignal = new WiFiSignal()
                {
                    MacAddress = availableNetwork.Bssid,
                    Ssid = availableNetwork.Ssid,
                    SignalBars = availableNetwork.SignalBars,
                    ChannelCenterFrequencyInKilohertz = availableNetwork.ChannelCenterFrequencyInKilohertz,
                    NetworkKind = availableNetwork.NetworkKind.ToString(),
                    PhysicalKind = availableNetwork.PhyKind.ToString()
                };

                wifiPoint.WiFiSignals.Add(wifiSignal);


                networkInfo.Append(availableNetwork.Bssid);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.Ssid);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.SignalBars);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.NetworkRssiInDecibelMilliwatts);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.NetworkKind);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.IsWiFiDirect);
                networkInfo.Append(", ");

                networkInfo.Append(position.Coordinate.Point.Position.Latitude);
                networkInfo.Append(", ");

                networkInfo.Append(position.Coordinate.Point.Position.Longitude);
                networkInfo.Append(", ");

                networkInfo.Append(position.Coordinate.Accuracy);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.Uptime);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.SecuritySettings.NetworkEncryptionType);

                networkInfo.AppendLine();
            }

            return networkInfo;
        }

        private async Task ShowMessage(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

    }
}
