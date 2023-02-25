using UnityEngine;

namespace Internal.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BallDataList", menuName = "ScriptableObjects/BallDataList", order = 1)]
    public class BallDataList : ScriptableObject
    {
        public BallData[] BallList;
    }
}