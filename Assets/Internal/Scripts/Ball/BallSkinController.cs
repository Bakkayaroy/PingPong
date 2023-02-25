using System;
using System.Linq;
using Internal.Scripts.Save;
using Internal.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Ball
{
    public class BallSkinController : MonoBehaviour, IBallSkinController
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        private BallDataList _ballDataList;
        private ISaveDataManger _saveDataManger;

        [Inject]
        private void Construct(BallDataList ballDataList, ISaveDataManger saveDataManger)
        {
            _saveDataManger = saveDataManger;
            _ballDataList = ballDataList;
        }

        private void Start()
        {
            SetSkin(_saveDataManger.GetSkinId());
        }

        public void SetSkin(Guid id)
        {
            var data = _ballDataList.BallList.FirstOrDefault(x => x.Id.Equals(id));
            if (data != null)
                _meshRenderer.material = data.Material;
        }
    }
}