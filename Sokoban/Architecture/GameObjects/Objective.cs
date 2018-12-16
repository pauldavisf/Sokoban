using System;
namespace Sokoban.Architecture
{
    public class Objective : IGameObject
    {
        public Objective()
        {
        }

        public string ImageFileName => "objective";

        public bool Moveable => false;
    }
}
