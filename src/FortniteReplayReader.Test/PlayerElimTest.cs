using FortniteReplayReader.Exceptions;
using FortniteReplayReader.Extensions;
using FortniteReplayReader.Models;
using System;
using System.IO;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;
using Xunit;

namespace FortniteReplayReader.Test
{
    public class PlayerElimTest
    {
        [Theory]
        [InlineData(new byte[] {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x0D, 0x00, 0x00, 0x00, 0x53, 0x6F, 0x75, 0x74, 0x68, 0x48, 0x75, 0x6E,
            0x74, 0x65, 0x72, 0x5A, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x49, 0x74, 0x5A,
            0x4D, 0x65, 0x65, 0x6E, 0x64, 0x59, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_NETEXPORT_SERIALIZATION, "++Fortnite+Release-4.0")]
        [InlineData(new byte[] {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F,
            0x00, 0x00, 0x80, 0x3F, 0x09, 0x00, 0x00, 0x00, 0x53, 0x6F, 0x6C, 0x64,
            0x69, 0x77, 0x65, 0x62, 0x00, 0x08, 0x00, 0x00, 0x00, 0x63, 0x72, 0x61,
            0x6E, 0x6B, 0x30, 0x35, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_NETEXPORT_SERIALIZATION, "++Fortnite+Release-4.2")]
        [InlineData(new byte[] {
            0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x0D, 0x00, 0x00,
            0x00, 0x73, 0x69, 0x6C, 0x76, 0x65, 0x72, 0x63, 0x72, 0x65, 0x73, 0x74,
            0x79, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x46, 0x72, 0x65, 0x7A, 0x65, 0x72,
            0x33, 0x35, 0x33, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_NETEXPORT_SERIALIZATION, "++Fortnite+Release-4.3")]
        [InlineData(new byte[] {
            0x03, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x0A, 0x00, 0x00,
            0x00, 0x78, 0x63, 0x75, 0x63, 0x61, 0x78, 0x6C, 0x75, 0x76, 0x00, 0x08,
            0x00, 0x00, 0x00, 0x6C, 0x75, 0x6E, 0x64, 0x65, 0x6A, 0x36, 0x00, 0x04,
            0x01, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_NETEXPORT_SERIALIZATION, "++Fortnite+Release-5.41")]
        [InlineData(new byte[] {
            0x03, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x0C, 0x00, 0x00,
            0x00, 0x54, 0x77, 0x69, 0x74, 0x68, 0x20, 0x50, 0x61, 0x72, 0x74, 0x79,
            0x00, 0x08, 0x00, 0x00, 0x00, 0x52, 0x61, 0x6D, 0x61, 0x72, 0x61, 0x73,
            0x00, 0x05, 0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_NETEXPORT_SERIALIZATION, "++Fortnite+Release-6.00")]
        [InlineData(new byte[] {
            0x03, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0xF9, 0xFF, 0xFF,
            0xFF, 0x53, 0x00, 0xB1, 0x03, 0x6C, 0x00, 0x74, 0x00, 0x79, 0x00, 0xC4,
            0x30, 0x00, 0x00, 0x0D, 0x00, 0x00, 0x00, 0x6D, 0x38, 0x20, 0x4C, 0x20,
            0x65, 0x20, 0x6E, 0x20, 0x6E, 0x20, 0x79, 0x00, 0x02, 0x00, 0x00, 0x00,
            0x00
        }, EngineNetworkVersionHistory.HISTORY_NETEXPORT_SERIALIZATION, "++Fortnite+Release-7.10")]
        [InlineData(new byte[] {
            0x03, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x09, 0x00, 0x00,
            0x00, 0x47, 0x6F, 0x44, 0x5F, 0x45, 0x64, 0x38, 0x78, 0x00, 0x09, 0x00,
            0x00, 0x00, 0x47, 0x6F, 0x44, 0x5F, 0x45, 0x64, 0x38, 0x78, 0x00, 0x30,
            0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_NETEXPORT_SERIALIZATION, "++Fortnite+Release-8.20")]
        [InlineData(new byte[] {
            0x08, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x80, 0x8B, 0xB5, 0x33, 0x3F, 0xF2, 0x51, 0x36, 0x3F, 0xFD, 0x7F, 0x59,
            0x48, 0x0F, 0xFF, 0x07, 0xC6, 0xB7, 0x59, 0x73, 0x44, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x80, 0x00, 0x00, 0x00, 0x00, 0x41, 0x36, 0xD5, 0xBE, 0x3C, 0xBF, 0x68,
            0x3F, 0x0E, 0x85, 0x52, 0x48, 0xE1, 0x9A, 0xEF, 0xC3, 0x5C, 0x9F, 0xE5,
            0xC4, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80,
            0x3F, 0x11, 0x10, 0xDA, 0x90, 0x74, 0x0C, 0x6B, 0x45, 0x4D, 0xBA, 0xBD,
            0x21, 0x43, 0x3F, 0x6D, 0xFC, 0x2E, 0xD2, 0x11, 0x10, 0x47, 0x3D, 0x00,
            0x81, 0xE0, 0xEF, 0x45, 0x10, 0xA4, 0x92, 0x19, 0x5F, 0xB7, 0x73, 0x1B,
            0x8F, 0x06, 0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_FAST_ARRAY_DELTA_STRUCT, "++Fortnite+Release-9.10")]
        [InlineData(new byte[] {
            0x09, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x80, 0x2A, 0x68, 0x13, 0x3F, 0x3E, 0x4D, 0x51, 0x3F, 0xA9, 0x9D, 0x48,
            0xC7, 0x80, 0x8B, 0x4C, 0x47, 0x66, 0x42, 0x2B, 0x45, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x80, 0x73, 0xF5, 0x6F, 0x3F, 0xF4, 0x63, 0xB2,
            0x3E, 0xD4, 0x5D, 0x49, 0xC7, 0x4A, 0xD3, 0x4B, 0x47, 0x66, 0x52, 0x2D,
            0x45, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80,
            0x3F, 0x03, 0x11, 0x10, 0x3F, 0x60, 0x81, 0xF4, 0xE2, 0x9B, 0x40, 0x81,
            0xAF, 0xC8, 0x85, 0xB6, 0x05, 0x21, 0xBE, 0x38, 0x03, 0x01, 0x00, 0x00,
            0x00
        }, EngineNetworkVersionHistory.HISTORY_OPTIONALLY_QUANTIZE_SPAWN_INFO, "++Fortnite+Release-11.00")]
        [InlineData(new byte[] {
            0x09, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x80, 0x6E, 0x9A, 0x0B, 0x3F, 0xE3, 0x95, 0x56, 0x3F, 0x21, 0xEB, 0x26,
            0x48, 0x2A, 0xDF, 0x25, 0xC8, 0x8F, 0x76, 0x0F, 0xC5, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x80, 0x6E, 0x9A, 0x0B, 0x3F, 0xE3, 0x95, 0x56,
            0x3F, 0x21, 0xEB, 0x26, 0x48, 0x2A, 0xDF, 0x25, 0xC8, 0x8F, 0x76, 0x0F,
            0xC5, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80,
            0x3F, 0x11, 0x10, 0x91, 0xB5, 0x74, 0x66, 0xEC, 0xEB, 0x4A, 0x5F, 0xAC,
            0x08, 0xEA, 0x46, 0x55, 0xB9, 0xDD, 0x27, 0x11, 0x10, 0x91, 0xB5, 0x74,
            0x66, 0xEC, 0xEB, 0x4A, 0x5F, 0xAC, 0x08, 0xEA, 0x46, 0x55, 0xB9, 0xDD,
            0x27, 0x30, 0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_OPTIONALLY_QUANTIZE_SPAWN_INFO, "++Fortnite+Release-11.00")]
        [InlineData(new byte[] {
            0x09, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80,
            0x3F, 0x10, 0x0C, 0x00, 0x00, 0x00, 0x43, 0x61, 0x63, 0x74, 0x75, 0x73,
            0x44, 0x61, 0x64, 0x38, 0x30, 0x00, 0x11, 0x10, 0x52, 0xEE, 0x0B, 0xF0,
            0xBA, 0x3E, 0x4A, 0x53, 0xB1, 0xEC, 0xFE, 0x9A, 0x37, 0x92, 0x5A, 0x13,
            0x06, 0x01, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_OPTIONALLY_QUANTIZE_SPAWN_INFO, "++Fortnite+Release-11.11")]
        [InlineData(new byte[] {
            0x09, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x80, 0xB2, 0x0A, 0xC9, 0x3C, 0x43, 0xEC, 0x7F, 0x3F, 0x42, 0xB8, 0x90,
            0x47, 0xC1, 0x50, 0x82, 0xC7, 0xFD, 0x61, 0x88, 0x47, 0x00, 0x00, 0x80,
            0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x80, 0x11, 0xFB, 0x7F, 0x3F, 0xC0, 0x0E, 0x49,
            0x3C, 0xA4, 0x4A, 0xA1, 0x47, 0xE1, 0x92, 0x91, 0xC7, 0x93, 0xF3, 0x82,
            0x47, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80,
            0x3F, 0x11, 0x10, 0x1F, 0x39, 0x0E, 0x4F, 0x62, 0x11, 0x47, 0xD7, 0x8D,
            0x1B, 0xE4, 0xFE, 0xE3, 0x35, 0xE5, 0x9F, 0x11, 0x10, 0x74, 0x94, 0xBB,
            0xA6, 0x39, 0x20, 0x4A, 0xDC, 0x8E, 0xC2, 0x63, 0x53, 0xA2, 0xD5, 0xB8,
            0x39, 0x08, 0x00, 0x00, 0x00, 0x00
        }, EngineNetworkVersionHistory.HISTORY_OPTIONALLY_QUANTIZE_SPAWN_INFO, "++Fortnite+Release-11.31")]
        public void ParsePlayerElimTest(byte[] rawData, EngineNetworkVersionHistory version, string branch)
        {
            using var stream = new MemoryStream(rawData);
            using var archive = new Unreal.Core.BinaryReader(stream)
            {
                EngineNetworkVersion = version
            };
            var reader = new ReplayReader()
            {
                Branch = branch
            };
            reader.ParseElimination(archive, new EventInfo { StartTime = 0 });

            Assert.True(archive.AtEnd());
            Assert.False(archive.IsError);
        }

        [Fact]
        public void PlayerElimThrows()
        {
            byte[] rawData = {
                0x09, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x80, 0xB2, 0x0A, 0xC9, 0x3C, 0x43, 0xEC, 0x7F, 0x3F, 0x42, 0xB8, 0x90,
                0x47, 0xC1, 0x50, 0x82, 0xC7, 0xFD, 0x61, 0x88, 0x47, 0x00, 0x00, 0x80,
                0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x80, 0x11, 0xFB, 0x7F, 0x3F, 0xC0, 0x0E, 0x49,
                0x3C, 0xA4, 0x4A, 0xA1, 0x47, 0xE1, 0x92, 0x91, 0xC7, 0x93, 0xF3, 0x82
            };

            using var stream = new MemoryStream(rawData);
            using var archive = new Unreal.Core.BinaryReader(stream)
            {
                EngineNetworkVersion = EngineNetworkVersionHistory.HISTORY_JITTER_IN_HEADER
            };
            var reader = new ReplayReader()
            {
                Branch = "++Fortnite+Release-11.31"
            };

            Assert.Throws<PlayerEliminationException>(() => reader.ParseElimination(archive, null));
        }
    }
}
