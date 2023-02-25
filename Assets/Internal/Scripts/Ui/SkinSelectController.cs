using Internal.Scripts.Save;
using Internal.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Ui
{
    public class SkinSelectController : MonoBehaviour
    {
        [SerializeField] private SkinItemController _skinItem;
        private ISaveDataManger _saveDataManger;
        private BallDataList _ballDataList;

        [Inject]
        private void Construct(BallDataList ballDataList, ISaveDataManger saveDataManger)
        {
            _saveDataManger = saveDataManger;
            _ballDataList = ballDataList;
        }

        public void Start()
        {
            foreach (var ballData in _ballDataList.BallList)
            {
                var skinItem = Instantiate(_skinItem, transform);
                skinItem.Initialize(ballData, data => _saveDataManger.SetSkinId(data.Id));
                if (ballData.Id.Equals(_saveDataManger.GetSkinId()))
                {
                    skinItem.SetActiveBorder();
                }
            }
        }
    }
}