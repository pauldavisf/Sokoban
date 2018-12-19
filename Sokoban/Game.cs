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

        private bool demoMode = false;

        private IGameMap gameMap;
        private IMoveController moveController;
        private KeyboardState previousState;
        private GameState gameState;

        private SpriteFont spriteFont;

        private IGameMenu mainMenu;
        private IGameMenu scoresMenu;
        private IGameMenu nextLevelMenu;

        ILevelBox levelBox;
        private Song music;
        IDrawer drawer;

        Level currentLevel;

        private string[] menuItemLabels = { "continue", "scores", "exit", "back", "replay" };

        private Dictionary<string, Texture2D> gameObjectTextures = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D[]> menuTextures = new Dictionary<string, Texture2D[]>();

        private void InitGameMenus()
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
            MenuItem replayItem1 = new MenuItem(MenuItem.ItemType.Replay,
                                             menuTextures[menuItemLabels[4]][0],
                                             menuTextures[menuItemLabels[4]][1],
                                             menuTextures[menuItemLabels[4]][2]);
            MenuItem replayItem2 = new MenuItem(MenuItem.ItemType.Replay,
                                             menuTextures[menuItemLabels[4]][0],
                                             menuTextures[menuItemLabels[4]][1],
                                             menuTextures[menuItemLabels[4]][2]);
            mainMenu = new GameMenu();
            mainMenu.AddItem(continueItem);
            mainMenu.AddItem(scoresItem);
            mainMenu.AddItem(replayItem1);
            mainMenu.AddItem(exitItem);

            scoresMenu = new GameMenu();
            scoresMenu.AddItem(backItem);

            nextLevelMenu = new GameMenu();
            nextLevelMenu.AddItem(replayItem2);
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
                if (imageFileName != null)
                {
                    gameObjectTextures[imageFileName] = Content.Load<Texture2D>("Images/" + imageFileName);
                }
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void LoadLevel(Level level)
        {
            gameMap = new GameMap(level.Map.StringRepresentation);
            if (level.BackgroundSoundFileName != null)
            {
                music = Content.Load<Song>("Sound/Level Music/" + level.BackgroundSoundFileName);
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }

            moveController = new MoveController(gameMap);
            gameState = new GameState(gameMap.ObjectivesCount)
            {
                CurrentState = GameState.State.Playing
            };

            graphics.PreferredBackBufferWidth = gameMap.Width * Config.CellSize 
                                                + Config.DefaultFrameOffset * 2;
            graphics.PreferredBackBufferHeight = gameMap.Height * Config.CellSize
                                                 + Config.DefaultFrameOffset * 2;
            graphics.ApplyChanges();

            previousState = Keyboard.GetState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            if (Config.LevelsFileName != null)
            {
                levelBox = new LevelBox(Config.LevelsFileName);
                currentLevel = levelBox.CurrentLevel;
            }
            else
            {
                currentLevel = Constants.DefaultLevel;
                demoMode = true;
            }

            LoadLevel(currentLevel);

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

            var frame = Content.Load<Texture2D>("Images/tv");
            drawer = new Drawer(Window, spriteBatch, frame);

            spriteFont = Content.Load<SpriteFont>("Fonts/Score");

            LoadMenuTextures();
            InitGameMenus();

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
                switch (gameState.CurrentState)
                {
                    case GameState.State.Playing:
                        gameState.CurrentState = GameState.State.Paused;
                        break;
                    case GameState.State.Paused:
                        gameState.CurrentState = GameState.State.Playing;
                        break;
                    case GameState.State.ScoresShowing:
                        gameState.CurrentState = GameState.State.Paused;
                        break;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.M) && !previousState.IsKeyDown(Keys.M))
            {
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            }

            if (gameState.CurrentState == GameState.State.Playing)
            {
                var actionResult = KeyboardHandler.HandlePlayerMoveKeys(keyboardState,
                                                                        previousState,
                                                                        moveController);

                if (actionResult != null)
                {
                    gameState.RemainingObjectives -= actionResult.ObjectivesDelta;
                    gameState.Steps++;

                    if (gameState.RemainingObjectives == 0)
                    {
                        gameState.CurrentState = GameState.State.LevelEnd;
                    }
                }
            }
            else if (gameState.CurrentState == GameState.State.Paused)
            {
                var result = KeyboardHandler.HandleMainMenuKeys(keyboardState,
                                                                previousState,
                                                                mainMenu);

                if (result == MenuItem.ItemType.Continue)
                {
                    gameState.CurrentState = GameState.State.Playing;
                }
                else if (result == MenuItem.ItemType.ShowHighScores)
                {
                    gameState.CurrentState = GameState.State.ScoresShowing;
                }
                else if (result == MenuItem.ItemType.Replay)
                {
                    LoadLevel(currentLevel);
                }
                else if (result == MenuItem.ItemType.Exit)
                {
                    Exit();
                }
            }
            else if (gameState.CurrentState == GameState.State.LevelEnd)
            {
                if (demoMode)
                {
                    LoadLevel(Constants.DefaultLevel);
                }
                else
                {

                    gameState.Scores = (int)(Constants.DefaultScoresForObjective * gameMap.ObjectivesCount *
                                       currentLevel.ScoresMultiplier /
                                        ((gameState.Steps * gameTime.TotalGameTime.TotalSeconds) / 30));

                    currentLevel = levelBox.NextLevel();

                    if (currentLevel == null)
                    {
                        gameState.CurrentState = GameState.State.GameEnd;
                    }
                    else
                    {
                        LoadLevel(currentLevel);
                    }
                }
            }
            else if (gameState.CurrentState == GameState.State.GameEnd)
            {
                Exit();
            }

            base.Update(gameTime);

            previousState = keyboardState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkViolet);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (gameState.CurrentState == GameState.State.Playing)
            {
                drawer.DrawMap(gameMap, gameObjectTextures, true);
                drawer.DrawLevelLabel(currentLevel.Label, spriteFont);
            }
            else if (gameState.CurrentState == GameState.State.Paused)
            {
                drawer.DrawMap(gameMap, gameObjectTextures, false);
                drawer.DrawMenu(mainMenu);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
