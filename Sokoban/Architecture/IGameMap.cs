using System;
using System.Drawing;

namespace Sokoban.Architecture
{
    public interface IGameMap
    {
        int Width { get; }
        int Height { get; }
        int ObjectivesCount { get; }

        Point GetPlayerCoordinates();
        bool IsObjective(int x, int y);

        IGameObject this[int x, int y] { get;  set; }
    }
}
