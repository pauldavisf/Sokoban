using System;
namespace Sokoban.Architecture
{
    public interface ILevelBox
    {
        void AddLevel(Level level);
        void RemoveLevel(Level level);
        Level NextLevel();
        Level PrevLevel();
        Level CurrentLevel { get; }
        void LoadFromFile(string fileName);
    }
}
