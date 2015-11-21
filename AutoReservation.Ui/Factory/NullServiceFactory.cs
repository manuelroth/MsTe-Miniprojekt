using AutoReservation.Common.Interfaces;
using Moq;

namespace AutoReservation.Ui.Factory
{
    public class NullServiceFactory : IServiceFactory
    {
        public IAutoReservationService GetService()
        {
            return new Mock<IAutoReservationService>().Object;
        }
    }
}
