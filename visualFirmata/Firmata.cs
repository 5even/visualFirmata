namespace visualFirmata
{
    public static class Firmata
    {
        public static FirmataDigitalPort PortFromPinNumber(int PinNumber)
        {
            return (FirmataDigitalPort)((int)(PinNumber / 8));
        }

        public static int[] PinsInPort(FirmataDigitalPort Port)
        {
            return new int[] { (int)Port * 8, (int)Port * 8 + 1, (int)Port * 8 + 2, (int)Port * 8 + 3, (int)Port * 8 + 4, (int)Port * 8 + 5, (int)Port * 8 + 6, (int)Port * 8 + 7 };
        }
                
        public class DataMessage
        {
            public FirmataMessageType Command;
            public int Channel;
            public int Data;

            public DataMessage(FirmataMessageType Command, int Channel, int Data)
            {
                this.Channel = Channel;
                this.Command = Command;
                this.Data = Data;
            }

            public DataMessage(byte[] Message)
            {
                this.Channel = Message[0] & 0xF;
                this.Command = (FirmataMessageType)(Message[0] & 0xF0);
                this.Data = (Message[1] << 7) + Message[2];
            }
            byte[] Message(FirmataMessageType Command, int Channel, int Data)
            {
                this.Channel = Channel;
                this.Command = Command;
                this.Data = Data;
                return Message();
            }

            byte[] Message()
            {
                return new byte[] { (byte)((byte)Command | (byte)Channel), (byte)((Data & 0x80) >> 7), (byte)(Data & 0x7F) };
            }
            
        }
    }

}