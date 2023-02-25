using UnityEngine;

namespace Internal.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BallData", menuName = "ScriptableObjects/BallData", order = 1)]
    public class BallData : IdentifiedScriptableObject
    {
        [SerializeField] public Sprite Icon;
        [SerializeField] public Material Material;
    }
}