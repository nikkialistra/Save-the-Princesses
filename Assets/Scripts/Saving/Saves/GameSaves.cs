using System.Collections.Generic;

namespace Saving.Saves
{
    public class GameSaves
    {
        public bool LoadingFromSave => CurrentSave != null;

        public GameSave CurrentSave;

        public List<GameSave> Saves = new();

        public void CreateNewSave()
        {
            CurrentSave = new GameSave();

            Saves.Add(CurrentSave);
        }
    }
}
