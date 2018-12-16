using System;
namespace Sokoban.Architecture
{
    public class Empty : IGameObject
    {
        public string ImageFileName => "empty";

        public bool Moveable => false;
    }
}
