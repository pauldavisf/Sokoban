using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Architecture
{
    public class Box : IGameObject
    {
        public bool Moveable => true;

        public string DefaultImageFileName => "box";

        public Texture2D Texture { get; set; }

        public GameActionResult Move(Offset offset)
        {
            throw new NotImplementedException();
        }
    }
}
