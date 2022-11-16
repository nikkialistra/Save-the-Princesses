using System;
using UnityEngine;

namespace GameData.Settings.Types
{
    [Serializable]
    public class GeneralSettings
    {
        public float TimeToFinishRun => _timeToFinishRun;

        [SerializeField] private float _timeToFinishRun = 1.5f;
    }
}
