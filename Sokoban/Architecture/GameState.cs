using System;
namespace Sokoban.Architecture
{
    public class GameState
    {
        public enum State
        {
            ScoresShowing,
            Playing,
            Paused
        }

        public int Scores;
        public State CurrentState;
        public int RemainingObjectives;
    }
}
