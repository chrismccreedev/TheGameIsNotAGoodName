using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    public class PlatformCheck : MonoBehaviour
    {
        [SerializeField] private GameObject _trigger;
        [SerializeField] private Transform _parents;
        [SerializeField] private CheckValue[] _checkValues;

        public CheckValue[] CheckValue => _checkValues;

        [FoldoutGroup("Button")]
        [SerializeField, MinValue(0)] private int _index;
        [FoldoutGroup("Button")]
        [Button] 
        private void AddTrigger()
        {
            GameObject trigger = Instantiate(_trigger);
            trigger.transform.SetParent(_parents);
            trigger.transform.localPosition = Vector3.zero;
            trigger.name = _checkValues[_index].PlatformType.ToString();
            trigger.GetComponentInChildren<SphereCollider>().gameObject.name = _checkValues[_index].PlatformType.ToString();
            _checkValues[_index].Add(trigger);

        }
        [FoldoutGroup("Button")]
        [Button]
        private void DestroyTrigger()
        {
            foreach(GameObject trigger in _checkValues[_index].Check)
            {
                DestroyImmediate(trigger);
            }
            _checkValues[_index].Clear();
        }
    }

    [System.Serializable]
    public class CheckValue
    {
        [HorizontalGroup("Horizontal_1")]
        [VerticalGroup("Horizontal_1/Left")]
        [SerializeField] private PlatformType _platformType;
        [VerticalGroup("Horizontal_1/Right")]
        [SerializeField] private List<GameObject> _check;

        public PlatformType PlatformType => _platformType;
        public List<GameObject> Check => _check;

        public void Add(GameObject obj)
        {
            _check.Add(obj);
        }
        public void Clear()
        {
            _check.Clear();
        }
    }
}
