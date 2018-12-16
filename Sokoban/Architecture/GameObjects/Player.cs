using System;
namespace Sokoban.Architecture
{
    public class Player : IGameObject
    {
        public Player()
        {
        }

        public bool Moveable => true;

        public string ImageFileName => "player";
    }
}
