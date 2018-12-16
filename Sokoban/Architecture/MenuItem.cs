using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sokoban.Architecture
{
    public class MenuItem
    {
        public enum ItemType
        {
            Continue,
            Replay,
            NextLevel,
            ShowHighScores,
            Exit
        }

        public ItemType Type;
        public bool isActive;

        public Texture2D CurrentTexture;
        public Texture2D DefaultTexture;
        public Texture2D InactiveTexture;
        public Texture2D SelectedTexture;

        public MenuItem(ItemType type,
                        Texture2D defaultTexture,
                        Texture2D inactiveTexture,
                        Texture2D selectedTexture)
        {
            Type = type;
            DefaultTexture = defaultTexture;
            InactiveTexture = inactiveTexture;
            SelectedTexture = selectedTexture;
            CurrentTexture = DefaultTexture;
            isActive = true;
        }
    }
}
