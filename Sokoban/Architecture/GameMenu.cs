using System;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public class GameMenu : IGameMenu
    {
        private List<MenuItem> items = new List<MenuItem>();
        private int currentItemIndex;

        public MenuItem CurrentItem { get; private set; }

        public void MoveNext()
        {
            if (items.Count > 0)
            {
                CurrentItem.ChangeTextureType(MenuItem.TextureType.Default);

                currentItemIndex = (currentItemIndex + 1) % items.Count;

                CurrentItem = items[currentItemIndex];
                CurrentItem.ChangeTextureType(MenuItem.TextureType.Selected);
            }
        }

        public void MovePrev()
        {
            if (items.Count > 0)
            {
                CurrentItem.ChangeTextureType(MenuItem.TextureType.Default);

                currentItemIndex -= 1;
                if (currentItemIndex == -1)
                {
                    currentItemIndex = items.Count - 1;
                }

                CurrentItem = items[currentItemIndex];
                CurrentItem.ChangeTextureType(MenuItem.TextureType.Selected);
            }
        }
    }
}
