using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sokoban.Architecture;

namespace Sokoban.Desktop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameMap gameMap;
        private MoveController moveController;
        private KeyboardState previousState;
        private GameState gameState;
        private GameMenu mainMenu;
        private GameMenu scoresMenu;
        private GameMenu nextLevelMenu;
        private Song music;

        private string[] menuItemLabels = { "continue", "scores", "exit", "back", "replay" };

        private Dictionary<string, Texture2D> gameObjectTextures = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D[]> menuTextures = new Dictionary<string, Texture2D[]>();

        private void InitGameMenu()
        {
            MenuItem continueItem = new MenuItem(MenuItem.ItemType.Continue,
                                                 menuTextures[menuItemLabels[0]][0],
                                                 menuTextures[menuItemLabels[0]][1],
                                                 menuTextures[menuItemLabels[0]][2]);
            MenuItem scoresItem = new MenuItem(MenuItem.ItemType.ShowHighScores,
                                               menuTextures[menuItemLabels[1]][0],
                                               menuTextures[menuItemLabels[1]][1],
                                               menuTextures[menuItemLabels[1]][2]);
            MenuItem exitItem = new MenuItem(MenuItem.ItemType.Exit,
                                             menuTextures[menuItemLabels[2]][0],
                                             menuTextures[menuItemLabels[2]][1],
                                             menuTextures[menuItemLabels[2]][2]);
            MenuItem backItem = new MenuItem(MenuItem.ItemType.Back,
                                             menuTextures[menuItemLabels[3]][0],
                                             menuTextures[menuItemLabels[3]][1],
                                             menuTextures[menuItemLabels[3]][2]);
            MenuItem replayItem = new MenuItem(MenuItem.ItemType.Replay,
                                             menuTextures[menuItemLabels[4]][0],
                                             menuTextures[menuItemLabels[4]][1],
                                             menuTextures[menuItemLabels[4]][2]);
            mainMenu = new GameMenu();
            mainMenu.AddItem(continueItem);
            mainMenu.AddItem(scoresItem);
            mainMenu.AddItem(exitItem);

            scoresMenu = new GameMenu();
            scoresMenu.AddItem(backItem);

            nextLevelMenu = new GameMenu();
            nextLevelMenu.AddItem(replayItem);
            nextLevelMenu.AddItem(continueItem);
        }

        private void LoadMenuTextures()
        {
            foreach(var label in menuItemLabels)
            {
                var textures = new Texture2D[3];
                textures[0] = Content.Load<Texture2D>($"Images/Menu/{label}Default");
                textures[1] = Content.Load<Texture2D>($"Images/Menu/{label}Selected");
                textures[2] = Content.Load<Texture2D>($"Images/Menu/{label}Inactive");

                menuTextures[label] = textures;
            }
        }

        private void LoadGameObjectTextures()
        {
            foreach (var imageFileName in gameMap.ImageFileNames)
            {
                gameObjectTextures[imageFileName] = Content.Load<Texture2D>("Images/" + imageFileName);
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Level currentLevel;
            if (Config.LevelsFileName != null)
            {
                var levelBox = new LevelBox(Config.LevelsFileName);
                currentLevel = levelBox.CurrentLevel;
                gameMap = currentLevel.Map;
                music = Content.Load<Song>("Sound/Level Music/" + currentLevel.BackgroundSoundFileName);
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }
            else
            {
                gameMap = new GameMap(Constants.DefaultLevel);
            }

            moveController = new MoveController(gameMap);
            gameState = new GameState(gameMap.ObjectivesCount);
            gameState.CurrentState = GameState.State.Playing;

            graphics.PreferredBackBufferWidth = gameMap.Width * Config.CellSize;
            graphics.PreferredBackBufferHeight = gameMap.Height * Config.CellSize;
            graphics.ApplyChanges();

            previousState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadMenuTextures();
            InitGameMenu();

            LoadGameObjectTextures();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        private void HandleMainMenuKeys(KeyboardState keyboardState)
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
                if (mainMenu.CurrentItem.Type == MenuItem.ItemType.Continue)
                {
                    gameState.CurrentState = GameState.State.Playing;
                }
                else if (mainMenu.CurrentItem.Type == MenuItem.ItemType.ShowHighScores)
                {
                    gameState.CurrentState = GameState.State.ScoresShowing;
                }
                else if (mainMenu.CurrentItem.Type == MenuItem.ItemType.Exit)
                {
                    Exit();
                }
            }
        }

        private GameActionResult HandlePlayerMoveKeys(KeyboardState keyboardState)
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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape))
            {
                if (gameState.CurrentState == GameState.State.Playing)
                {
                    gameState.CurrentState = GameState.State.Paused;
                }
                else if (gameState.CurrentState == GameState.State.Paused)
                {
                    gameState.CurrentState = GameState.State.Playing;
                }
                else if (gameState.CurrentState == GameState.State.ScoresShowing)
                {
                    gameState.CurrentState = GameState.State.Paused;
                }
            }

            if (gameState.CurrentState == GameState.State.Playing)
            {
                var actionResult = HandlePlayerMoveKeys(keyboardState);

                if (actionResult != null)
                {
                    gameState.RemainingObjectives -= actionResult.ObjectivesDelta;

                    if (gameState.RemainingObjectives == 0)
                    {
                        gameState.CurrentState = GameState.State.LevelEnd;
                    }
                }
            }
            else if (gameState.CurrentState == GameState.State.Paused)
            {
                HandleMainMenuKeys(keyboardState);
            }

            base.Update(gameTime);

            previousState = keyboardState;
        }

        private void DrawMenu()
        {
            int y = 0;
            foreach(var item in mainMenu)
            {
                spriteBatch.Draw(item.CurrentTexture,
                                 new Rectangle(Window.ClientBounds.Width / 2 - item.CurrentTexture.Width / 2,
                                               y,
                                               item.CurrentTexture.Width,
                                               item.CurrentTexture.Height),
                                               Color.White);
                y += item.CurrentTexture.Height;
            }
        }

        private void DrawMap(bool active)
        {
            for (int x = 0; x < gameMap.Width; x++)
            {
                for (int y = 0; y < gameMap.Height; y++)
                {
                    spriteBatch.Draw(gameObjectTextures[gameMap[x, y].ImageFileName],
                                     new Rectangle(x + x * Config.CellSize,
                                                   y + y * Config.CellSize,
                                                   Config.CellSize,
                                                   Config.CellSize),
                                     active ? Color.White : Color.CornflowerBlue);
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (gameState.CurrentState == GameState.State.Playing)
            {
                DrawMap(true);
            }
            else if (gameState.CurrentState == GameState.State.Paused)
            {
                DrawMap(false);
                DrawMenu();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
