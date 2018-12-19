using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Architecture
{
    public class Empty : IGameObject
    {
        public string DefaultImageFileName => "empty";

        public Texture2D Texture { get; set; }

        public bool Moveable => false;
    }
}
