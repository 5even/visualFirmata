namespace visualFirmata
{
    public interface IIOComponent
    {
        object Tag { get; set; }
        int PinNumber { get; set; }
        IOPinMode PinMode { get; set; }
        IOPinState PinState { get; set; }
    }
}