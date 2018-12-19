using System;
namespace Sokoban.Architecture
{
    public class Level
    {
        public string Label { get; set; }
        public double ScoresMultiplier { get; set; }
        public string BackgroundSoundFileName { get; set; }
        public IGameMap Map { get; set; }

        public Level(string label, double scoresMultiplier, string soundFileName, IGameMap map)
        {
            Label = label;
            ScoresMultiplier = scoresMultiplier;
            BackgroundSoundFileName = soundFileName;
            Map = map;
        }

        public Level()
        {

        }
    }
}
