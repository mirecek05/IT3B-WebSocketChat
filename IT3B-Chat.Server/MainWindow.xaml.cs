using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace IT3B_Chat.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebSocketServer _server; //vytvoreni serveru

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (_server != null)
            {
                _server.Stop();
            }

            string address = txtBoxZadaniAdresy.Text;
            _server = new WebSocketServer(8080);
            _server.AddWebSocketService<Chat>("/chat");
            _server.Start(); // Spuštění serveru

            txtBlockServerMessages.Text = "Server running...";
            btnPripojit.IsEnabled = false;
            btnOdpojit.IsEnabled = true;
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            _server?.Stop();
            txtBlockServerMessages.Text = "Server stopped.";
            btnPripojit.IsEnabled = true;
            btnOdpojit.IsEnabled = false;
        }

        private void btnOdeslaniZpravy_Click(object sender, RoutedEventArgs e)
        {
            string message = txtBoxZadaniOdesilaneZpravy.Text;
            
        }
    }

    public class Chat : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            string msg = e.Data;
            Console.WriteLine($"Received message: {msg}");
            Send(msg);
        }
    }
}