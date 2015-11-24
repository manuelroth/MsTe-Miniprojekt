using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Ui.Factory
{
    class LocalDataAccessCreator : Creator
    {

        public override IAutoReservationService CreateInstance()
        {
            return new AutoReservationService();
        }

    }

}
