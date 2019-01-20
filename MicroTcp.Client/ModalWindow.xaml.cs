using MicroTcp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MicroTcp.Client
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        private BLL.Common Common;
        public static ValidatePortModel _portNumber;
        public ModalWindow()
        {
            InitializeComponent();
            Common = new BLL.Common();
        }

        private void btn_Set_Port_Click(object sender, RoutedEventArgs e)
        {
            bool isPortNumberValid = Common.ValidatePortNumber(txt_PortNumber.Text);
            if (isPortNumberValid)
            {
                _portNumber = Common.ValidatePortNumberTuple(txt_PortNumber.Text);
                this.Close();
            }
            txt_PortNumber.Text = String.Empty;
        }
    }
}
