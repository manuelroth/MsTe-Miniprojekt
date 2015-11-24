using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{

    [DataContract]
    public class UpdateConcurrencyFault
    {

        [DataMember]
        public string Message { get; set; }

    }

}
