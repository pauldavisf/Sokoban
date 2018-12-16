using System;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public interface IGameMenu
    {
        MenuItem CurrentItem { get; }

        void MoveNext();
        void MovePrev();
    }
}
