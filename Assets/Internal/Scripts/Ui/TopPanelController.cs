using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Scripts.Ui
{
    public class TopPanelController : MonoBehaviour
    {
        [SerializeField] private MainMenuController _mainMenuController;
        [SerializeField] private Button _button;
        private IGameRoundController _gameRoundController;

        [Inject]
        private void Construct(IGameRoundController gameRoundController)
        {
            _gameRoundController = gameRoundController;
        }

        private void Awake()
        {
            _button.onClick.AddListener(PauseClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void PauseClick()
        {
            _mainMenuController.gameObject.SetActive(true);
            _mainMenuController.UpdateBestScore();
            _gameRoundController.StopRound();
        }
    }
}