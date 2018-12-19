using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sokoban.Architecture
{
    public class GameMap : IGameMap
    {
        private IGameObject[,] gameObjects;
        private Objective[,] objectivesMap;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int ObjectivesCount { get; private set; }
        public string[] StringRepresentation { get; private set; }

        public List<string> ImageFileNames { get; private set; } = new List<string>();

        public Point GetPlayerCoordinates()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (gameObjects[x, y] is Player)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw new InvalidOperationException("Player not found on the map!");
        }

        public bool IsObjective(int x, int y)
        {
            ValidationHelper.ValidateCoordinates(this, x, y, true);

            return objectivesMap[x, y] is Objective;
        }

        public void LoadFromStrings(string[] lines)
        {
            var isPlayerOnMap = false;

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];

                if (line.Length != Width)
                {
                    throw new ArgumentException("All initial string lines must be same length",
                                                 nameof(lines));
                }

                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == Constants.Player)
                    {
                        if (isPlayerOnMap)
                        {
                            throw new ArgumentException("There must be only one player on the map",
                                                        nameof(lines));
                        }
                        isPlayerOnMap = true;
                    }

                    gameObjects[x, y] = GameObjectCreator.CreateObject(line[x]);

                    if (gameObjects[x, y] is Objective)
                    {
                        ObjectivesCount++;
                        objectivesMap[x, y] = gameObjects[x, y] as Objective;
                    }

                    if (!ImageFileNames.Contains(gameObjects[x, y].DefaultImageFileName))
                    {
                        ImageFileNames.Add(gameObjects[x, y].DefaultImageFileName);
                    }
                }
            }

            if (!isPlayerOnMap)
            {
                throw new ArgumentException("There is no player on the map",
                                            nameof(lines));
            }
        }

        public GameMap(string initialString)
        {
            if (initialString == null)
            {
                throw new ArgumentNullException(nameof(initialString));
            }

            var lines = initialString.Split('\n');
            Width = lines[0].Length;
            Height = lines.Length;
            gameObjects = new IGameObject[Width, Height];
            objectivesMap = new Objective[Width, Height];

            LoadFromStrings(lines);
        }

        public GameMap(string[] lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            StringRepresentation = lines;

            Width = lines[0].Length;
            Height = lines.Length;
            gameObjects = new IGameObject[Width, Height];
            objectivesMap = new Objective[Width, Height];

            LoadFromStrings(lines);
        }

        public GameMap(int width, int height)
        {
            Width = width;
            Height = height;
            gameObjects = new IGameObject[Width, Height];
            objectivesMap = new Objective[Width, Height];
        }

        public IGameObject this[int x, int y]
        {
            get
            {
                return gameObjects[x, y];
            }
            set
            {
                gameObjects[x, y] = value;
            }
        }
    }
}
