namespace Sokoban.Architecture
{
    public static class Constants
    {
        public const char Player = 'p';
        public const char Box = 'b';
        public const char Terrain = 't';
        public const char Objective = 'o';
        public const char Empty = ' ';

        public const int CellSize = 60;

        public static string Level1 = "   ttttt\n" +
        	                          "tttttt  \n" +
        	                          "bttttttt\n" +
        	                          "ttbbt tt\n" +
        	                          "tttttott\n" +
        	                          "  tottpt";
        public static int DefaultScoresForObjective = 100;
    }
}
