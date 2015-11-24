using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Ui.Factory
{
    class RemoteDataAccessCreator : Creator
    {

        public override IAutoReservationService CreateInstance()
        {
            ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            return channelFactory.CreateChannel();
        }

    }

}
