using System;

namespace Internal.Scripts.Save
{
    public interface ISaveDataManger
    {
        public void SetSkinId(Guid id);
        public void SetBestScore(int score);
        public Guid GetSkinId();
        public int GetBestScore();
    }
}