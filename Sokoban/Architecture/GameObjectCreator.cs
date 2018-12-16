using System;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public static class GameObjectCreator
    {
        public static IGameObject CreateObject(char objectName)
        {
            switch (objectName)
            {
                case Constants.Player:
                    return new Player();
                case Constants.Box:
                    return new Box();
                case Constants.Terrain:
                    return new Terrain();
                case Constants.Objective:
                    return new Objective();
                case Constants.Empty:
                    return new Empty();
                default:
                    throw new ArgumentException("Unknown object name", nameof(objectName));
            }
        }
    }
}
