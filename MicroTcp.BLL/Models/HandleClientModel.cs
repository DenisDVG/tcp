using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class HandleClientModel
    {
        public TcpClient TcpClient { get; set; }
        public System.Threading.ThreadLocal<ConcurrentList<ClientListModel>> ClientListModel { get; set; }
    }
}
