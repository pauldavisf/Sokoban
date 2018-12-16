using System;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public class GameMenu : IGameMenu
    {
        private List<MenuItem> items;
        private int currentItemIndex;

        public MenuItem CurrentItem => throw new NotImplementedException();

        public void MoveNext()
        {
            if (items.Count > 0)
            {
                currentItemIndex = (currentItemIndex + 1) % items.Count;
                CurrentItem = items[currentItemIndex];
            }
        }

        public void MovePrev()
        {
            throw new NotImplementedException();
        }
    }
}
