namespace AutoReservation.Common.DataTransferObjects.Core
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
