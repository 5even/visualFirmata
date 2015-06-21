public enum SysExCommand
{
    RESERVED_COMMAND = 0x00,        // 2nd SysEx data byte is a chip-specific command (AVR, PIC, TI, etc).
    ANALOG_MAPPING_QUERY = 0x69,    // ask for mapping of analog to pin numbers
    ANALOG_MAPPING_RESPONSE = 0x6A, // reply with mapping info
    CAPABILITY_QUERY = 0x6B,        // ask for supported modes and resolution of all pins
    CAPABILITY_RESPONSE = 0x6C,     // reply with supported modes and resolution
    PIN_STATE_QUERY = 0x6D,         // ask for a pin's current mode and value
    PIN_STATE_RESPONSE = 0x6E,      // reply with a pin's current mode and value
    EXTENDED_ANALOG = 0x6F,         // analog write (PWM, Servo, etc) to any pin
    SERVO_CONFIG = 0x70,            // set max angle, minPulse, maxPulse, freq
    STRING_DATA = 0x71,             // a string message with 14-bits per char
    SHIFT_DATA = 0x75,              // shiftOut config/data message (34 bits)
    I2C_REQUEST = 0x76,             // I2C request messages from a host to an I/O board
    I2C_REPLY = 0x77,               // I2C reply messages from an I/O board to a host
    I2C_CONFIG = 0x78,              // Configure special I2C settings such as power pins and delay times
    REPORT_FIRMWARE = 0x79,         // report name and version of the firmware
    SAMPLING_INTERVAL = 0x7A,       // sampling interval
    SYSEX_NON_REALTIME = 0x7E,      // MIDI Reserved for non-realtime messages
    SYSEX_REALTIME = 0x7F           // MIDI Reserved for realtime messages
}