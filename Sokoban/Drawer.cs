using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sokoban.Architecture;

namespace Sokoban.Desktop
{
    public class Drawer : IDrawer
    {
        private GameWindow window;
        private SpriteBatch spriteBatch;
        private Texture2D frame;

        public Drawer(GameWindow window, SpriteBatch spriteBatch, Texture2D frame)
        {
            this.spriteBatch = spriteBatch;
            this.window = window;
            this.frame = frame;
        }

        public void DrawTextAtCenter(string text, SpriteFont spriteFont, Color color)
        {
            spriteBatch.DrawString(spriteFont,
                                   text,
                                   new Vector2(window.ClientBounds.Width / 2 - 
                                               spriteFont.MeasureString(text).Length() / 2,
                                               Config.DefaultFrameOffset),
                                   color);
        }

        public void DrawFrame()
        {

            spriteBatch.Draw(frame,
                             new Rectangle(0, 
                                           0,
                                           window.ClientBounds.Width,
                                           window.ClientBounds.Height),
                             Color.White);
        }

        public void DrawMap(IGameMap gameMap, 
                            Dictionary<string, Texture2D> texturesDectionary,
                            bool blur)
        {
            for (int x = 0; x < gameMap.Width; x++)
            {
                for (int y = 0; y < gameMap.Height; y++)
                {
                    if (gameMap[x, y].DefaultImageFileName != null)
                    {
                                spriteBatch.Draw(texturesDectionary[gameMap[x, y].DefaultImageFileName],
                                                new Rectangle(x + x * Config.CellSize + Config.DefaultFrameOffset,
                                                       y + y * Config.CellSize + Config.DefaultFrameOffset,
                                                       Config.CellSize,
                                                       Config.CellSize),
                                                blur ? Color.White : Color.CornflowerBlue);
                    }
                }
            }

            DrawFrame();
        }

        public void DrawMenu(IGameMenu gameMenu)
        {
            int y = 50;
            foreach (var menuItem in gameMenu)
            {
                var item = menuItem as MenuItem;
                spriteBatch.Draw(item.CurrentTexture,
                                 new Rectangle(window.ClientBounds.Width / 2 - item.CurrentTexture.Width / 2,
                                               y,
                                               item.CurrentTexture.Width,
                                               item.CurrentTexture.Height),
                                               Color.White);
                y += item.CurrentTexture.Height;
            }

            DrawFrame();
        }
    }
}
