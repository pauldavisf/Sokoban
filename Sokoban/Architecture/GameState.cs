namespace Sokoban.Architecture
{
    public class GameState
    {
        public enum State
        {
            ScoresShowing,
            Playing,
            Paused,
            LevelEnd,
            GameEnd
        }

        public int Scores;
        public State CurrentState;
        public int RemainingObjectives;
        public int Steps;

        public GameState()
        {

        }

        public GameState(int objectivesCount)
        {
            RemainingObjectives = objectivesCount;
        }
    }
}
