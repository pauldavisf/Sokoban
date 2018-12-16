using System;
namespace Sokoban.Architecture
{
    public class MoveController : IMoveController
    {
        private GameMap gameMap;

        public MoveController(GameMap gameMap)
        {
            this.gameMap = gameMap;
        }

        private bool CanMove(int sourceX, int sourceY, Offset offset)
        {
            ValidationHelper.ValidateCoordinates(gameMap, sourceX, sourceY, true);

            var sourceObject = gameMap[sourceX, sourceY];
            var endX = sourceX + offset.DeltaX;
            var endY = sourceY + offset.DeltaY;

            return sourceObject.Moveable &&
                   ValidationHelper.ValidateCoordinates(gameMap, endX, endY, false) &&
                   (gameMap[endX, endY] is Empty || gameMap[endX, endY] is Objective);
        }

        private void MakeMapCellEmpty(int x, int y)
        {
            ValidationHelper.ValidateCoordinates(gameMap, x, y, true);

            if (gameMap.IsObjective(x, y))
            {
                gameMap[x, y] = new Objective();
            }
            else
            {
                gameMap[x, y] = new Empty();
            }
        }

        public GameActionResult Move(int sourceX, int sourceY, Offset offset)
        {
            ValidationHelper.ValidateCoordinates(gameMap, sourceX, sourceY, true);

            if (!CanMove(sourceX, sourceY, offset))
            {
                return null;
            }

            var sourceObject = gameMap[sourceX, sourceY];
            var endX = sourceX + offset.DeltaX;
            var endY = sourceY + offset.DeltaY;
            var scores = 0;
            var objectivesDelta = 0;

            if (gameMap.IsObjective(endX, endY) && sourceObject is Box)
            {
                objectivesDelta += 1;
            }

            if (gameMap.IsObjective(sourceX, sourceY) && sourceObject is Box)
            {
                objectivesDelta -= 1;
            }

            gameMap[endX, endY] = sourceObject;

            return new GameActionResult(false, true, scores, objectivesDelta);
        }
    }
}
