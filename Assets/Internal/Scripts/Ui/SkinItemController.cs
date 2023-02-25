using System;
using Internal.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Ui
{
    [RequireComponent(typeof(Toggle))]
    public class SkinItemController : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Toggle _toggle;

        public void Initialize(BallData data, Action<BallData> callback)
        {
            _icon.sprite = data.Icon;
            _toggle.onValueChanged.AddListener(b => callback.Invoke(data));
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveAllListeners();
        }

        public void SetActiveBorder()
        {
            _toggle.isOn = true;
            _toggle.Select();
        }
    }
}