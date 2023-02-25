using System;
using Internal.Scripts.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Scripts.Ui
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _bestScore;
        
        private ISaveDataManger _saveDataManger;
        private IGameRoundController _gameRoundController;

        [Inject]
        private void Construct(ISaveDataManger saveDataManger, IGameRoundController gameRoundController)
        {
            _gameRoundController = gameRoundController;
            _saveDataManger = saveDataManger;
        }

        private void Start()
        {
            _bestScore.text = _saveDataManger.GetBestScore().ToString();
            _playButton.onClick.AddListener(StartRound);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveAllListeners();
        }

        private void StartRound()
        {
            gameObject.SetActive(false);
            _gameRoundController.StartRound();
        }

        public void UpdateBestScore()
        {
            _bestScore.text = _saveDataManger.GetBestScore().ToString();
        }
    }
}