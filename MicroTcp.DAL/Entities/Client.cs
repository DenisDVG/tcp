using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class Client
    {
        public int Port { get; set; }
        public TcpClient TcpClient { get; set; }

    }
}
