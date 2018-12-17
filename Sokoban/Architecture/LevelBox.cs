using System;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public class LevelBox : ILevelBox
    {
        private List<Level> levels = new List<Level>();
        private int currentLevelIndex;

        public Level CurrentLevel { get; private set; }

        public void AddLevel(Level level)
        {
            levels.Add(level);
        }

        public void RemoveLevel(Level level)
        {
            levels.Remove(level);
        }

        public Level NextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex == levels.Count)
            {
                return null;
            }
            else
            {
                return levels[currentLevelIndex];
            }
        }

        public Level PrevLevel()
        {
            currentLevelIndex--;
            if (currentLevelIndex == -1)
            {
                return null;
            }
            else
            {
                return levels[currentLevelIndex];
            }
        }

        private Level ParseBaseInformation(string[] lines, int index)
        {
            var level = new Level();
            level.Label = lines[index];
            level.ScoresMultiplier = double.Parse(lines[index + 1]);
            level.BackgroundSoundFileName = lines[index + 2];

            if (level.BackgroundSoundFileName == "none")
            {
                level.BackgroundSoundFileName = null;
            }

            return level;
        }

        private GameMap ParseMap(string[] lines, int index)
        {
            var linesForMap = new List<string>();

            for (int i = index; i < lines.Length - 1; i++)
            {
                if (lines[i].Length == 0)
                {
                    break;
                }

                linesForMap.Add(lines[i]);
            }

            return new GameMap(linesForMap.ToArray());
        }

        public void LoadFromFile(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);

            for (int i = 0; i < lines.Length; i++)
            {
                var level = ParseBaseInformation(lines, i);
                i += 3;
                level.Map = ParseMap(lines, i);
                i += level.Map.Height;

                AddLevel(level);
            }
        }

        public LevelBox(string levelsFileName)
        {
            LoadFromFile(levelsFileName);

            if (levels.Count == 0)
            {
                throw new InvalidOperationException("No levels found in levels file!");
            }

            currentLevelIndex = 0;
            CurrentLevel = levels[currentLevelIndex];
        }

        public LevelBox()
        {

        }
    }
}
