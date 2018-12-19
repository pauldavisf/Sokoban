namespace Sokoban.Architecture
{
    public static class Constants
    {
        public const char Player = 'p';
        public const char Box = 'b';
        public const char Terrain = 't';
        public const char Objective = 'o';
        public const char Empty = ' ';

        public const int FrameOffset = 35;

        public static string DefaultLevelMap = "   ttttt\n" +
        	                          "tttttt  \n" +
        	                          "bttttttt\n" +
        	                          "ttbbt tt\n" +
        	                          "tttttott\n" +
        	                          "  tottpt";

        public static Level DefaultLevel = new Level("Demo mode!", 1, null, new GameMap(DefaultLevelMap));
        public static int DefaultScoresForObjective = 100;
    }
}
