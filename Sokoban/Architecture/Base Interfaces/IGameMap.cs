using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sokoban.Architecture
{
    public interface IGameMap
    {
        int Width { get; }
        int Height { get; }
        int ObjectivesCount { get; }
        string[] StringRepresentation { get; }
        List<string> ImageFileNames { get; }

        Point GetPlayerCoordinates();
        bool IsObjective(int x, int y);

        IGameObject this[int x, int y] { get;  set; }
    }
}
