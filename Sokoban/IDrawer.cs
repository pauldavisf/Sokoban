﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sokoban.Architecture;

namespace Sokoban.Desktop
{
    public interface IDrawer
    {
        void DrawMenu(IGameMenu gameMenu);

        void DrawTextAtCenter(string label, SpriteFont spriteFont, Color color);

        void DrawMap(IGameMap gameMap, Dictionary<string,
                     Texture2D> texturesDectionary,
                     bool blur);
    }
}
