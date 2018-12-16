using System;
namespace Sokoban.Architecture
{
    public class GameActionResult
    {
        public bool GameOver { get; private set; }
        public bool NeedToDraw { get; private set; }
        public int Scores { get; private set; }
        public int ObjectivesDelta { get; private set; }

        public GameActionResult(bool gameOver, bool needToDraw, int scores, int objectivesDelta)
        {
            GameOver = gameOver;
            NeedToDraw = needToDraw;
            Scores = scores;
            ObjectivesDelta = objectivesDelta;
        }
    }
}
