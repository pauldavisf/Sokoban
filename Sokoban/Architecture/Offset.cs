using System;
namespace Sokoban.Architecture
{
    public class Offset
    {
        public int DeltaX { get; private set; }
        public int DeltaY { get; private set; }

        public Offset(int deltaX, int deltaY)
        {
            DeltaX = deltaX; 
            DeltaY = deltaY;
        }
    }
}
