using System;
namespace Sokoban.Architecture
{
    public class GameActionResult
    {
        public int ObjectivesDelta { get; private set; }

        public GameActionResult(int objectivesDelta)
        {
            ObjectivesDelta = objectivesDelta;
        }
    }
}
