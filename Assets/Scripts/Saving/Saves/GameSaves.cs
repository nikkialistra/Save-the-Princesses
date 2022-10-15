using System;
using System.Collections.Generic;

namespace Saving.Saves
{
    [Serializable]
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
