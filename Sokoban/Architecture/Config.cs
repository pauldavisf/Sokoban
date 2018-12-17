﻿using System.IO;

namespace Sokoban.Architecture
{
    public static class Config
    {
        private static string DefaultConfigFileName = "config.conf";
        public static string LevelsFileName { get; private set; }
        public static int CellSize { get; private set; }

        private const int defaultCellSize = 25;

        static Config()
        {
            ReadFromFile(DefaultConfigFileName);
        }

        private static void ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                CellSize = defaultCellSize;
                LevelsFileName = null;
                return;
            }

            string[] lines = File.ReadAllLines(fileName);

            CellSize = int.Parse(lines[0]);
            LevelsFileName = lines[1];
        }
    }
}
