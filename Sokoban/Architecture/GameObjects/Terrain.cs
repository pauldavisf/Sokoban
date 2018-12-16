using System;
namespace Sokoban.Architecture
{
    public class Terrain : IGameObject
    {
        public string ImageFileName => "terrain";

        public bool Moveable => false;
    }
}
