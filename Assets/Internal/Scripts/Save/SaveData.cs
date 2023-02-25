using System;
using UnityEngine;

namespace Internal.Scripts.Save
{
    [Serializable]
    public class SaveData
    {
        [SerializeField] public string BallDataId;
        [SerializeField] public int BestScore;
    }
}