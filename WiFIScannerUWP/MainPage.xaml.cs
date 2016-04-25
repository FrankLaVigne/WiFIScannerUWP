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

namespace WiFIScannerUWP
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

            await this._wifiScanner.ScanForNetworks();

            var report = this._wifiScanner.WiFiAdapter.NetworkReport;

            StringBuilder networkInfo = new StringBuilder();

            foreach (var availableNetwork in report.AvailableNetworks)
            {
                networkInfo.Append(availableNetwork.Ssid);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.SignalBars);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.NetworkKind);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.PhyKind);
                networkInfo.Append(", ");

                networkInfo.Append(availableNetwork.Uptime);
                //networkInfo.Append(", ");

                //networkInfo.Append(availableNetwork.SecuritySettings);

                networkInfo.AppendLine();
            }

            this.txbReport.Text = networkInfo.ToString();

            this.btnScan.IsEnabled = true;

        }

        private async Task ShowMessage(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

    }
}
