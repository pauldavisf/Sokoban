using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Architecture
{
    public class Objective : IGameObject
    {
        public string DefaultImageFileName => "objective";

        public Texture2D Texture { get; set; }

        public bool Moveable => false;
    }
}
