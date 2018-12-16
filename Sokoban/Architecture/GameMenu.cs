using System;
using System.Collections;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public class GameMenu : IGameMenu, IEnumerable<MenuItem>
    {
        private List<MenuItem> items = new List<MenuItem>();
        private int currentItemIndex;

        public MenuItem CurrentItem { get; private set; }

        public void AddItem(MenuItem menuItem)
        {
            items.Add(menuItem);

            if (items.Count == 1)
            {
                currentItemIndex = 0;
                CurrentItem = items[currentItemIndex];
                CurrentItem.ChangeTextureType(MenuItem.TextureType.Selected);
            }
        }

        public IEnumerator<MenuItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public void SelectNext()
        {
            if (items.Count > 0)
            {
                CurrentItem.ChangeTextureType(MenuItem.TextureType.Default);

                currentItemIndex = (currentItemIndex + 1) % items.Count;

                CurrentItem = items[currentItemIndex];
                CurrentItem.ChangeTextureType(MenuItem.TextureType.Selected);
            }
        }

        public void SelectPrev()
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
