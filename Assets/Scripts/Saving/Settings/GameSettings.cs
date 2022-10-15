using System;
using System.IO;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Saving.Settings
{
    public class GameSettings
    {
        public bool Fullscreen
        {
            get => _gameSettingsData.Fullscreen;
            set
            {
                Screen.fullScreen = value;
                _gameSettingsData.Fullscreen = value;
                Save();
            }
        }

        public ReactiveProperty<Resolution> Resolution { get; } = new();

        public ReactiveProperty<float> MasterVolume { get; private set; } = new();
        public ReactiveProperty<float> EffectsVolume { get; private set; } = new();
        public ReactiveProperty<float> MusicVolume { get; private set; } = new();

        public ReactiveProperty<string> Language { get; private set; } = new();

        public ReactiveProperty<UiScale> UiScale { get; private set; } = new();

        public ReactiveProperty<FontStyle> FontStyle { get; private set; } = new();
        public ReactiveProperty<FontSize> FontSize { get; private set; } = new();

        private readonly CompositeDisposable _disposables = new();

        private bool _loaded;

        private string _savePath;

        private GameSettingsData _gameSettingsData;

        public void Initialize()
        {
            GenerateSavePath();

            _gameSettingsData = new GameSettingsData();

            Load();
            Apply();

            SubscribeToChanges();
        }

        public void Dispose()
        {
            UnsubscribeFromChanges();
        }

        private void GenerateSavePath()
        {
            _savePath = Path.Combine(Application.persistentDataPath, "settings.json");
        }

        private void OnResolutionChange(Resolution value)
        {
            if (value.width == 0) return;

            Screen.SetResolution(value.width, value.height, Fullscreen);
            _gameSettingsData.Resolution = value.ToString();
            Save();
        }

        private void Save()
        {
            var saveData = JsonUtility.ToJson(_gameSettingsData);

            try
            {
                File.WriteAllText(_savePath, saveData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write to {_savePath} with exception {e}");
            }
        }

        [Button(ButtonSizes.Medium)]
        private void DeleteSave()
        {
            GenerateSavePath();

            try
            {
                if (File.Exists(_savePath))
                {
                    File.Delete(_savePath);
                    Debug.Log("File deleted");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to delete file at {_savePath} with exception {e}");
            }
        }

        private void Apply()
        {
            if (!_loaded)
                Load();

            SetUpParameters();
        }

        private void Load()
        {
            if (!File.Exists(_savePath))
            {
                CreateDefaultSettings();

                _loaded = true;
                return;
            }

            try
            {
                var saveData = File.ReadAllText(_savePath);
                JsonUtility.FromJsonOverwrite(saveData, _gameSettingsData);

                _loaded = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {_savePath} with exception {e}");
            }
        }

        private void SetUpParameters()
        {
            Fullscreen = _gameSettingsData.Fullscreen;
            FindResolution();

            MasterVolume.Value = _gameSettingsData.MasterVolume;
            EffectsVolume.Value = _gameSettingsData.EffectsVolume;
            MusicVolume.Value = _gameSettingsData.MusicVolume;

            Language.Value = _gameSettingsData.Language;

            FontStyle.Value = _gameSettingsData.FontStyle;
            FontSize.Value = _gameSettingsData.FontSize;
        }

        private void FindResolution()
        {
            foreach (var resolution in Screen.resolutions)
                if (resolution.ToString() == _gameSettingsData.Resolution)
                    Resolution.Value = resolution;
        }

        private void CreateDefaultSettings()
        {
            _gameSettingsData.Fullscreen = true;
            _gameSettingsData.Resolution = Screen.resolutions[^1].ToString();

            _gameSettingsData.MasterVolume = 100;
            _gameSettingsData.EffectsVolume = 100;
            _gameSettingsData.MusicVolume = 100;

            _gameSettingsData.Language = "English";

            _gameSettingsData.UiScale = Settings.UiScale.Normal;

            _gameSettingsData.FontStyle = Settings.FontStyle.Pixelated;
            _gameSettingsData.FontSize = Settings.FontSize.Medium;
        }

        private void SubscribeToChanges()
        {
            _disposables.Add(Resolution.Subscribe(OnResolutionChange));
        }

        private void UnsubscribeFromChanges()
        {
            _disposables.Dispose();
        }
    }
}
