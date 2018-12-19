using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Architecture
{
    public class Player : IGameObject
    {
        public Player()
        {
        }

        public bool Moveable => true;

        public string DefaultImageFileName => "player";

        public Texture2D Texture { get; set; }
    }
}
