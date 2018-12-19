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
            None,
            Continue,
            Replay,
            NextLevel,
            Back,
            ShowHighScores,
            Exit
        }

        public enum TextureType
        {
            Default,
            Inactive,
            Selected
        }

        private Texture2D defaultTexture;
        private Texture2D inactiveTexture;
        private Texture2D selectedTexture;
        private bool isActive;

        public ItemType Type;
        public Texture2D CurrentTexture;


        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
                if (isActive)
                {
                    ChangeTextureType(TextureType.Default);
                }
                else
                {
                    ChangeTextureType(TextureType.Inactive);
                }
            }
        }

        public MenuItem(ItemType type,
                        Texture2D defaultTexture,
                        Texture2D selectedTexture,
                        Texture2D inactiveTexture)
        {
            Type = type;
            this.defaultTexture = defaultTexture;
            this.inactiveTexture = inactiveTexture;
            this.selectedTexture = selectedTexture;
            CurrentTexture = defaultTexture;
            isActive = true;
        }

        public void ChangeTextureType(TextureType textureType)
        {
            switch (textureType)
            {
                case TextureType.Default:
                    CurrentTexture = defaultTexture;
                    break;
                case TextureType.Inactive:
                    CurrentTexture = inactiveTexture;
                    break;
                case TextureType.Selected:
                    CurrentTexture = selectedTexture;
                    break;
            }
        }
    }
}
