namespace Internal.Scripts.Ui
{
    public interface IScorePanelController
    {
        void ChangeScore(int playerScore, int botScore);
        void ChangeBestScore(int best);
    }
}