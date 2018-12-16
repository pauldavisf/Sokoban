using System;
namespace Sokoban.Architecture
{
    public class Box : IGameObject
    {
        public bool Moveable => true;

        public string ImageFileName => "box";

        public GameActionResult Move(Offset offset)
        {
            throw new NotImplementedException();
        }
    }
}
