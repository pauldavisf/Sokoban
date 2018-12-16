using System;
namespace Sokoban.Architecture
{
    public interface IMoveController
    {
        GameActionResult MovePlayer(Offset offset);
    }
}
