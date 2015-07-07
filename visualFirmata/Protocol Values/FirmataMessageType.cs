
    public enum FirmataMessageType
    {
        ANALOG_MESSAGE = 0xE0,
        DIGITAL_MESSAGE = 0x90,
        REPORT_ANALOG = 0xC0,
        REPORT_DIGITAL = 0xD0,
        START_SYSEX = 0xF0,
        SET_PIN_MODE = 0xF4,
        END_SYSEX = 0xF7,
        REPORT_VERSION = 0xF9,
        SYSTEM_RESET = 0xFF
    }

