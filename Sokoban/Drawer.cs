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

        public Drawer(GameWindow window, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.window = window;
        }

        public void DrawMap(IGameMap gameMap, 
                            Dictionary<string, Texture2D> texturesDectionary,
                            bool blur)
        {
            for (int x = 0; x < gameMap.Width; x++)
            {
                for (int y = 0; y < gameMap.Height; y++)
                {
                    spriteBatch.Draw(texturesDectionary[gameMap[x, y].ImageFileName],
                                     new Rectangle(x + x * Config.CellSize,
                                                   y + y * Config.CellSize,
                                                   Config.CellSize,
                                                   Config.CellSize),
                                     blur ? Color.White : Color.CornflowerBlue);
                }
            }
        }

        public void DrawMenu(IGameMenu gameMenu)
        {
            int y = 0;
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
        }
    }
}
