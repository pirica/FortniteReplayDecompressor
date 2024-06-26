using Xunit;

namespace Unreal.Core.Test;

public class ReceiveCustomDeltaPropertyTest
{
    [Fact]
    public void CustomDeltaPropertyTest()
    {
        // Inventory
        byte[] rawData = {
            0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x30, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x12, 0x01, 0x40, 0x00,
            0x00, 0x00, 0x32, 0x02, 0x61, 0x18, 0x0A, 0x02, 0x00, 0x00, 0x2A, 0x01,
            0x00, 0x00, 0x00, 0xFE, 0x1A, 0x01, 0x00, 0x00, 0x00, 0x00, 0x3A, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x6E, 0x3B, 0x45, 0x6A, 0x26, 0x01,
            0x5D, 0x51, 0x49, 0xCE, 0x96, 0x01, 0x06, 0x37, 0x9C, 0xB2, 0xB6, 0x01,
            0x03, 0xED, 0xF2, 0x11, 0x0E, 0x20, 0x17, 0x10, 0x07, 0x88, 0x17, 0xC4,
            0x00, 0x12, 0x05, 0x08, 0x10, 0x00, 0x00, 0xC8, 0x12, 0xE5, 0x01, 0xC8,
            0x0B, 0xFF, 0xFF, 0xFF, 0xFC, 0x28, 0x10, 0x00, 0x01, 0xA8, 0x10, 0x00,
            0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x90, 0x0A, 0x00, 0x00, 0x00, 0x01,
            0x90, 0x13, 0x48, 0xC0, 0x50, 0x12, 0x00, 0x01, 0x50, 0x08, 0x00, 0x00,
            0x07, 0xF0, 0xD0, 0x08, 0x00, 0x00, 0x00, 0x01, 0xD0, 0x08, 0x00, 0x00,
            0x00, 0x00, 0x30, 0x0B, 0xE5, 0x86, 0x68, 0x11, 0x30, 0x0A, 0xD3, 0x8A,
            0x80, 0x00, 0xB0, 0x08, 0x62, 0x24, 0x5F, 0x55, 0xB0, 0x08, 0xF8, 0x50,
            0x3F, 0xB0, 0x71, 0x00, 0xB8, 0x80, 0x3C, 0x40, 0xBE, 0x20, 0x00, 0x90,
            0x28, 0x40, 0x80, 0x00, 0x06, 0x40, 0x97, 0x28, 0x0E, 0x40, 0x5F, 0xFF,
            0xFF, 0xFF, 0xE1, 0x40, 0x80, 0x00, 0x0D, 0x40, 0x80, 0x00, 0x00, 0x18,
            0x00, 0x00, 0x00, 0x04, 0x80, 0x50, 0x00, 0x00, 0x00, 0x0C, 0x80, 0x99,
            0x46, 0x02, 0x80, 0x88, 0x00, 0x0A, 0x80, 0x40, 0x00, 0x00, 0x3F, 0x86,
            0x80, 0x40, 0x00, 0x00, 0x00, 0x0E, 0x80, 0x40, 0x00, 0x00, 0x00, 0x01,
            0x80, 0x59, 0x62, 0x0B, 0x88, 0xC9, 0x80, 0x55, 0x32, 0x4D, 0x89, 0x65,
            0x80, 0x4D, 0x01, 0xE9, 0x72, 0xAD, 0x80, 0x50, 0xB8, 0xC9, 0xF4, 0x63,
            0x88, 0x05, 0xC4, 0x01, 0xE2, 0x05, 0xF1, 0x00, 0x04, 0x81, 0x42, 0x04,
            0x00, 0x00, 0x32, 0x04, 0xB9, 0x40, 0x72, 0x02, 0xFF, 0xFF, 0xFF, 0xFF,
            0x0A, 0x04, 0x00, 0x00, 0x6A, 0x04, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00,
            0x00, 0x24, 0x02, 0x80, 0x00, 0x00, 0x00, 0x64, 0x04, 0xDA, 0x30, 0x14,
            0x04, 0xC0, 0x00, 0x54, 0x02, 0x00, 0x00, 0x01, 0xFC, 0x34, 0x02, 0x00,
            0x00, 0x00, 0x00, 0x74, 0x02, 0x00, 0x00, 0x00, 0x00, 0x0C, 0x02, 0xBF,
            0xB3, 0x66, 0x4A, 0x4C, 0x02, 0x2F, 0x72, 0x0B, 0x15, 0x2C, 0x02, 0x91,
            0x50, 0xBF, 0x11, 0x6C, 0x02, 0x73, 0xB2, 0x80, 0x62, 0x1C, 0x40, 0x2E,
            0x20, 0x0F, 0x10, 0x2F, 0x88, 0x00, 0x24, 0x0A, 0x10, 0x20, 0x00, 0x01,
            0x90, 0x25, 0xCA, 0x03, 0x90, 0x17, 0xFF, 0xFF, 0xFF, 0xF8, 0x50, 0x20,
            0x00, 0x03, 0x50, 0x20, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x01, 0x20,
            0x14, 0x00, 0x00, 0x00, 0x03, 0x20, 0x26, 0x31, 0x80, 0xA0, 0x21, 0x00,
            0x02, 0xA0, 0x10, 0x00, 0x00, 0x0F, 0xE1, 0xA0, 0x10, 0x00, 0x00, 0x00,
            0x03, 0xA0, 0x10, 0x00, 0x00, 0x00, 0x00, 0x60, 0x10, 0xF7, 0x78, 0x75,
            0xFA, 0x60, 0x15, 0x9D, 0x90, 0xFD, 0x11, 0x60, 0x14, 0x27, 0x61, 0xFD,
            0xCB, 0x60, 0x16, 0x46, 0xAE, 0xEE, 0x48, 0xE2, 0x01, 0x71, 0x00, 0x78,
            0x81, 0x7C, 0x40, 0x01, 0x20, 0x50, 0x81, 0x00, 0x00, 0x0C, 0x81, 0x2E,
            0x50, 0x1C, 0x80, 0xBF, 0xFF, 0xFF, 0xFF, 0xC2, 0x81, 0x00, 0x00, 0x1A,
            0x81, 0x00, 0x00, 0x00, 0x18, 0x00, 0x00, 0x00, 0x09, 0x00, 0xA0, 0x00,
            0x00, 0x00, 0x19, 0x01, 0x35, 0x8C, 0x05, 0x01, 0x28, 0x00, 0x15, 0x00,
            0x80, 0x00, 0x00, 0x7F, 0x0D, 0x00, 0x80, 0x00, 0x00, 0x00, 0x1D, 0x00,
            0x80, 0x00, 0x00, 0x00, 0x03, 0x00, 0xA0, 0xF2, 0x47, 0x39, 0xD3, 0x00,
            0x8C, 0x68, 0xB9, 0x44, 0x0B, 0x00, 0xA4, 0x6E, 0x59, 0x58, 0x5B, 0x00,
            0x95, 0xF4, 0x94, 0xCF, 0x07, 0x10, 0x0B, 0x88, 0x03, 0xC4, 0x0B, 0xE2,
            0x00, 0x09, 0x02, 0x84, 0x08, 0x00, 0x00, 0x64, 0x09, 0x72, 0x80, 0xE4,
            0x05, 0xFF, 0xFF, 0xFF, 0xFE, 0x14, 0x08, 0x00, 0x00, 0xD4, 0x08, 0x00,
            0x00, 0x00
        };

        //var archive = new BitReader(rawData, 4623)
        //{
        //    EngineNetworkVersion = Models.Enums.EngineNetworkVersionHistory.HISTORY_JITTER_IN_HEADER,
        //    NetworkVersion = Models.Enums.NetworkVersionHistory.HISTORY_CHARACTER_MOVEMENT_NOINTERP
        //};
        //var reader = new Mocks.MockReplayReader();
        //reader.ReceiveCustomDeltaProperty(archive);
    }

    [Fact]
    public void CustomDeltaPropertyTest2()
    {
        // CurrentPlayListInfo
        byte[] rawData = {
            0xFB, 0x34, 0x00, 0x00, 0x00, 0x00
        };

        //var archive = new BitReader(rawData, 48)
        //{
        //    EngineNetworkVersion = Models.Enums.EngineNetworkVersionHistory.HISTORY_JITTER_IN_HEADER,
        //    NetworkVersion = Models.Enums.NetworkVersionHistory.HISTORY_CHARACTER_MOVEMENT_NOINTERP
        //};
        //var reader = new Mocks.MockReplayReader();
        //reader.ReceiveCustomDeltaProperty(archive);
    }
}
