using System;
namespace Sokoban.Architecture
{
    public static class ValidationHelper
    {
        public static bool ValidateCoordinates(GameMap gameMap, int x, int y, bool throwException)
        {
            if (y < 0 || y >= gameMap.Height)
            {
                if (throwException)
                {
                    throw new ArgumentOutOfRangeException(nameof(y),
                        "Source y coordinate is out of map bounds");
                }
                return false;
            }

            if (x < 0 || x >= gameMap.Width)
            {
                if (throwException)
                {
                    throw new ArgumentOutOfRangeException(nameof(x),
                        "Source x coordinate is out of map bounds");
                }
                return false;
            }

            return true;
        }
    }
}
