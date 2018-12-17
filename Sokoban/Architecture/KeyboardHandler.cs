using System;
using Microsoft.Xna.Framework.Input;

namespace Sokoban.Architecture
{
    public static class KeyboardHandler
    {
        public static MenuItem.ItemType HandleMainMenuKeys(KeyboardState keyboardState,
                                                           KeyboardState previousState,
                                                           GameMenu mainMenu)
        {
            if (keyboardState.IsKeyDown(Keys.Up) && !previousState.IsKeyDown(Keys.Up))
            {
                mainMenu.SelectPrev();
            }

            if (keyboardState.IsKeyDown(Keys.Down) && !previousState.IsKeyDown(Keys.Down))
            {
                mainMenu.SelectNext();
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousState.IsKeyDown(Keys.Enter))
            {
                return mainMenu.CurrentItem.Type;
            }

            return MenuItem.ItemType.None;
        }

        public static GameActionResult HandlePlayerMoveKeys(KeyboardState keyboardState,
                                                            KeyboardState previousState,
                                                            IMoveController moveController)
        {
            if (keyboardState.IsKeyDown(Keys.Up) && !previousState.IsKeyDown(Keys.Up))
            {
                return moveController.MovePlayer(new Offset(0, -1));
            }

            if (keyboardState.IsKeyDown(Keys.Down) && !previousState.IsKeyDown(Keys.Down))
            {
                return moveController.MovePlayer(new Offset(0, 1));
            }

            if (keyboardState.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
            {
                return moveController.MovePlayer(new Offset(-1, 0));
            }

            if (keyboardState.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
            {
                return moveController.MovePlayer(new Offset(1, 0));
            }

            return null;
        }
    }
}
