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
                   (gameMap[endX, endY] is Terrain || gameMap[endX, endY] is Objective);
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
                gameMap[x, y] = new Terrain();
            }
        }

        private GameActionResult Move(int sourceX, int sourceY, Offset offset)
        {
            ValidationHelper.ValidateCoordinates(gameMap, sourceX, sourceY, true);

            if (!CanMove(sourceX, sourceY, offset))
            {
                return null;
            }

            var sourceObject = gameMap[sourceX, sourceY];
            var endX = sourceX + offset.DeltaX;
            var endY = sourceY + offset.DeltaY;
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
            MakeMapCellEmpty(sourceX, sourceY);

            return new GameActionResult(objectivesDelta);
        }

        public GameActionResult MovePlayer(Offset offset)
        {
            var playerCoordinates = gameMap.GetPlayerCoordinates();
            var endX = playerCoordinates.X + offset.DeltaX;
            var endY = playerCoordinates.Y + offset.DeltaY;

            if (!ValidationHelper.ValidateCoordinates(gameMap, endX, endY, false))
            {
                return null;
            }

            var endPointObject = gameMap[endX, endY];

            if (endPointObject is Box)
            {
                var boxMoveResult = Move(endX, endY, offset);
                var playerMoveResult = Move(playerCoordinates.X, playerCoordinates.Y, offset);

                return boxMoveResult;
            }

            return Move(playerCoordinates.X, playerCoordinates.Y, offset);
        }
    }
}
