using System;
using System.IO;
using Internal.Scripts.Ball;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Save
{
    public class SaveDataManager : MonoBehaviour, ISaveDataManger
    {
        private SaveData _currentSave = new SaveData();
        private IBallSkinController _ballSkinController;
        
        [Inject]
        private void Construct(IBallSkinController ballSkinController)
        {
            _ballSkinController = ballSkinController;
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            LoadSave();
        }

        private void LoadSave()
        {
            var pathFolder = Path.Combine(Application.persistentDataPath, "Save.json");
            if (!File.Exists(pathFolder)) return;
            var save = JsonUtility.FromJson<SaveData>(File.ReadAllText(pathFolder));
            if (save == null) return;
            _currentSave = save;
        }

        private void SaveData()
        {
            var pathFolder = Path.Combine(Application.persistentDataPath, "Save.json");
            var json = JsonUtility.ToJson(_currentSave);
            File.WriteAllText(pathFolder, json);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SaveData();
        }

        private void OnApplicationPause(bool a)
        {
            SaveData();
        }

        public void SetSkinId(Guid id)
        {
            _currentSave.BallDataId = id.ToString();
            _ballSkinController.SetSkin(GetSkinId());
        }

        public void SetBestScore(int score)
        {
            _currentSave.BestScore = score;
        }

        public Guid GetSkinId() => string.IsNullOrEmpty(_currentSave.BallDataId)
            ? Guid.Empty
            : Guid.Parse(_currentSave.BallDataId);

        public int GetBestScore() => _currentSave.BestScore;
    }
}