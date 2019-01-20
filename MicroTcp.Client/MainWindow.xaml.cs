using MicroTcp.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace MicroTcp.Client
{
    public partial class MainWindow : Window
    {
        private TcpClient _client;

        private StreamReader _sReader;
        private StreamWriter _sWriter;
        private int _portNumber;

        private Boolean _isConnected;
        public MainWindow()
        {
            ModalWindow modalWindow = new ModalWindow();
            modalWindow.ShowDialog();
            if (!ModalWindow._portNumber.IsValid)
            {
                System.Windows.Application.Current.Shutdown();
            }
            _portNumber = ModalWindow._portNumber.PortNmber;
            InitializeComponent();

            StartTcpClient();
        }

        private void StartTcpClient()
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _portNumber);

            _client = new TcpClient();
            _client.Connect(ipPoint);

            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);
            _isConnected = true;
            HandleCommunication();
        }

        public void HandleCommunication()
        {
            new Thread(() =>
            {
                while (_isConnected)
                {
                    String sDataIncomming = _sReader.ReadLine();
                    this.Dispatcher.Invoke(() =>
                    {
                        textBox.Text = sDataIncomming;
                        if(textBox.Text.Contains("conect to:"))
                        {
                            var toPortsString = textBox.Text.Replace("conect to:", string.Empty);
                            int toPort = 0;
                            if(Int32.TryParse(toPortsString, out toPort))
                            {
                                _isConnected = false;
                                _client.Close();
                                _portNumber = toPort;
                                StartTcpClient();
                            }

                        }
                    });
                }
            }).Start();

        }

        private void btn_Sent_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textSent.Text))
            {
                return;
            }
            var toPort = 0;
            if(textSent.Text.Contains("to ports:"))
            {
                var toPortsString = textSent.Text.Replace("to ports:", string.Empty);
                Int32.TryParse(toPortsString, out toPort);
            }
            var message = new Message {
            Text = textSent.Text,
            FromPort = _portNumber,
            ToPort = toPort
            };

            string messageJson = JsonConvert.SerializeObject(message);

            
            _sWriter.WriteLine(messageJson);
            textSent.Text = string.Empty;
            _sWriter.Flush();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

