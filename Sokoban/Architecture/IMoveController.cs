using System;
namespace Sokoban.Architecture
{
    public interface IMoveController
    {
        GameActionResult Move(int sourceX, int sourceY, Offset offset);
    }
}
