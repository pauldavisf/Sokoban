namespace Sokoban.Architecture
{
    public class GameState
    {
        public enum State
        {
            ScoresShowing,
            Playing,
            Paused,
            LevelEnd
        }

        public int Scores;
        public State CurrentState;
        public int RemainingObjectives;

        public GameState()
        {

        }

        public GameState(int objectivesCount)
        {
            RemainingObjectives = objectivesCount;
        }
    }
}
