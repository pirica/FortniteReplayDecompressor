﻿using FortniteReplayReaderDecompressor;
using System;
using System.IO;

namespace ConsoleReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var replayFile = "Replays/UnsavedReplay-2018.10.06-22.00.32.replay";
            using (var stream = File.Open(replayFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new FortniteBinaryDecompressor(stream))
                {
                    var replay = reader.ReadFile();
                }
            }
            Console.WriteLine("---- done ----");
            Console.ReadLine();
        }
    }
}