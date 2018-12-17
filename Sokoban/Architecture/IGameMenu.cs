using System;
using System.Collections;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public interface IGameMenu : IEnumerable
    {
        MenuItem CurrentItem { get; }
        void AddItem(MenuItem menuItem);

        void SelectNext();
        void SelectPrev();
    }
}
