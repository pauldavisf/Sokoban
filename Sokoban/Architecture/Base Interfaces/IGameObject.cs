using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Architecture
{
    public interface IGameObject
    {
        string DefaultImageFileName { get; }
        bool Moveable { get; }
        Texture2D Texture { get; set; }
    }
}
