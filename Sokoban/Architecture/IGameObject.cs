using System;
namespace Sokoban.Architecture
{
    public interface IGameObject
    {
        string ImageFileName { get; }
        bool Moveable { get; }
    }
}
