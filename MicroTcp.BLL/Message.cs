using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL
{
    public class Message
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public int FromPort { get; set; }
        public int ToPort { get; set; }
        public string ClientId { get; set; }

    }
}
