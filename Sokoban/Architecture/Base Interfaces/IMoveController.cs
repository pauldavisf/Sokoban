using System;
namespace Sokoban.Architecture
{
    public interface IMoveController
    {
        GameActionResult MoveObject(int sourceX, int sourceY, Offset offset);
        GameActionResult MovePlayer(Offset offset);
    }
}
