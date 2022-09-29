using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    [System.Serializable]
    public abstract class PlatformCheck<T>
    {
        [SerializeField] protected GameObject _trigger;
        [SerializeField] protected Transform _parents;
        [SerializeField] protected List<CheckValue<T>> _values;

        public List<CheckValue<T>> Values => _values;

    }

    [System.Serializable]
    public class CheckValue<T>
    {
        [HorizontalGroup("Horizontal_1")]
        [SerializeField] private T _platformType;
        [HorizontalGroup("Horizontal_2")]
        [SerializeField] private List<GameObject> _check;

        public T PlatformType => _platformType;
        public List<GameObject> Check => _check;

        public void Add(GameObject trigger, Transform parents)
        {
            GameObject trig = MonoBehaviour.Instantiate(trigger);
            trig.transform.SetParent(parents);
            trig.transform.localPosition = Vector3.zero;
            trig.name = _platformType.ToString();
            trig.GetComponentInChildren<SphereCollider>().gameObject.name = _platformType.ToString();
            _check.Add(trig);
        }
        public void Clear()
        {
            foreach (GameObject trigger in _check)
            {
                MonoBehaviour.DestroyImmediate(trigger);
            }
            _check.Clear();
        }
    }
}
