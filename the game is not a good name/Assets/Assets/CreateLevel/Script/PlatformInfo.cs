using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    [System.Serializable]
    public class PlatformInfo<T>
    {
        [SerializeField] private List<ListInfo<T>> _typeGroupe;

        public List<ListInfo<T>> TypeGroupe => _typeGroupe;
    }
    [System.Serializable]
    public class ListInfo<T>
    {
        [SerializeField] private T _type;
        [SerializeField] private List<Info<T>> _platform;

        public T Type => _type;
        public List<Info<T>> Platform => _platform;
    }
    [System.Serializable]
    public class Info<T>
    {
        [FoldoutGroup("GroundPlatformInfo")]
        [HorizontalGroup("GroundPlatformInfo/Up", LabelWidth = 20)]
        [SerializeField] private T _leftUp;
        [HorizontalGroup("GroundPlatformInfo/Up")]
        [SerializeField] private T _up;
        [HorizontalGroup("GroundPlatformInfo/Up")]
        [SerializeField] private T _rightUp;

        [HorizontalGroup("GroundPlatformInfo/Middle", LabelWidth = 20)]
        [SerializeField] private T _left;
        [HorizontalGroup("GroundPlatformInfo/Middle")]
        [PreviewField(Height = 80)]
        [SerializeField] private GameObject _prefab;
        [HorizontalGroup("GroundPlatformInfo/Middle")]
        [SerializeField] private T _right;

        [HorizontalGroup("GroundPlatformInfo/Down", LabelWidth = 20)]
        [SerializeField] private T _leftDown;
        [HorizontalGroup("GroundPlatformInfo/Down")]
        [SerializeField] private T _down;
        [HorizontalGroup("GroundPlatformInfo/Down")]
        [SerializeField] private T _rightDown;

        public GameObject Prefab => _prefab;
        public T[,] Platform
        {
            get
            {
                T[,] platforms = new T[3, 3];
                platforms[0, 0] = _leftUp;
                platforms[0, 1] = _up;
                platforms[0, 2] = _rightUp;
                platforms[1, 0] = _left;
                platforms[1, 2] = _right;
                platforms[2, 0] = _leftDown;
                platforms[2, 1] = _down;
                platforms[2, 2] = _rightDown;

                return platforms;
            }
        }
    }
}
