using TMPro;
using UnityEngine;

namespace Internal.Scripts.Ui
{
    public class ScorePanelController : MonoBehaviour, IScorePanelController
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _bestScore;
    
        public void ChangeScore(int playerScore, int botScore)
        {
            _score.text = $"{playerScore}:{botScore}";
        }
    
        public void ChangeBestScore(int best)
        {
            _bestScore.text = $"Best: {best}";
        }
    }
}
