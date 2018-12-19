using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Architecture
{
    public class Terrain : IGameObject
    {
        public string DefaultImageFileName => null;

        public Texture2D Texture { get; set; }

        public bool Moveable => false;
    }
}
