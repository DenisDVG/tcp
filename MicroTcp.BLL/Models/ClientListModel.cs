using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class ClientListModel
    {
        public int PortNumber { get; set; }
        public StreamWriter StreamWriter { get; set; }
    }
}
