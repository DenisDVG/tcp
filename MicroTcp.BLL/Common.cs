using MicroTcp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL
{
    public class Common
    {
        public bool ValidatePortNumber(string portNumber)
        {
            var portNumberParsed = 0;
            var isPortNumberValid = Int32.TryParse(portNumber, out portNumberParsed)
                && (portNumberParsed >= 5555 && portNumberParsed <= 5564);
            return isPortNumberValid;
        }

        public ValidatePortModel ValidatePortNumberTuple(string portNumber)
        {
            var portNumberParsed = 0;
            var isPortNumberValid = Int32.TryParse(portNumber, out portNumberParsed)
                && (portNumberParsed >= 5555 && portNumberParsed <= 5564);
            return new ValidatePortModel
            {
                IsValid = isPortNumberValid,
                PortNmber = portNumberParsed
            };
        }
    }
}
