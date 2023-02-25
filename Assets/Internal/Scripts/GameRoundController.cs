using Internal.Scripts.Ball;
using Internal.Scripts.Save;
using Internal.Scripts.Ui;
using UnityEngine;
using Zenject;

namespace Internal.Scripts
{
    public class GameRoundController : MonoBehaviour, IGameRoundController
    {
        [SerializeField] private GoalAreaController _aiGolArea;
        [SerializeField] private GoalAreaController _playerArea;

        private IBallMovementController _ballMovementController;
        private IScorePanelController _scorePanelController;
        private ISaveDataManger _saveDataManger;

        private int _bestScore;
        private int _aiScore;
        private int _playerScore;

        [Inject]
        private void Construct(IBallMovementController ballMovementController,
            IScorePanelController scorePanelController,
            ISaveDataManger saveDataManger)
        {
            _saveDataManger = saveDataManger;
            _ballMovementController = ballMovementController;
            _scorePanelController = scorePanelController;
        }

        private void Start()
        {
            _aiGolArea.Initialize(() =>
            {
                ChangeScore(ref _playerScore);
                UpdateBestScore();
            });
            _playerArea.Initialize(() => { ChangeScore(ref _aiScore); });

            _bestScore = _saveDataManger.GetBestScore();
            _scorePanelController.ChangeBestScore(_bestScore);
        }


        private void UpdateBestScore()
        {
            if (_playerScore <= _bestScore) return;
            _bestScore = _playerScore;
            _saveDataManger.SetBestScore(_bestScore);
            _scorePanelController.ChangeBestScore(_bestScore);
        }

        private void ChangeScore(ref int score)
        {
            score++;
            _scorePanelController.ChangeScore(_playerScore, _aiScore);
            StartRound();
        }

        public void StopRound()
        {
            _aiScore = 0;
            _playerScore = 0;
            _scorePanelController.ChangeScore(0, 0);
            _ballMovementController.ResetPosition();
        }

        public void StartRound()
        {
            _ballMovementController.ResetPosition();
            _ballMovementController.StartMove();
        }
    }
}