using Internal.Scripts.Ball;
using Internal.Scripts.Save;
using Internal.Scripts.ScriptableObjects;
using Internal.Scripts.Ui;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BallDataList _ballDataList;
        [SerializeField] private BallMovementController _ballMovementController;
        [SerializeField] private ScorePanelController _scorePanelController;
        [SerializeField] private GameRoundController _gameRoundController;
        [SerializeField] private BallSkinController _ballSkinController;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BallMovementController>().FromComponentOn(_ballMovementController.gameObject).AsCached();
            Container.BindInterfacesTo<ScorePanelController>().FromComponentOn(_scorePanelController.gameObject).AsCached();
            Container.BindInterfacesTo<GameRoundController>().FromComponentOn(_gameRoundController.gameObject).AsCached();
            Container.BindInterfacesTo<BallSkinController>().FromComponentOn(_ballSkinController.gameObject).AsCached();
            Container.Bind<BallDataList>().FromScriptableObject(_ballDataList).AsCached();
            Container.BindInterfacesTo<SaveDataManager>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}