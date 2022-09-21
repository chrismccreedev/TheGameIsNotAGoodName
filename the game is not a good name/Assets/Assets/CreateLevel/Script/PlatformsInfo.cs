using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    public class PlatformsInfo : MonoBehaviour
    {
        [SerializeField] private GameObject _default;
        [SerializeField] private List<GroupeInfo> _typeGroupe;

        public GameObject Default => _default;
        public List<GroupeInfo> TypeGroupe => _typeGroupe;
    }

    [System.Serializable]
    public class GroupeInfo
    {
        [SerializeField] private PlatformType _type;
        [SerializeField] private List<Info> _platform;

        public PlatformType Type => _type;
        public List<Info> Platform => _platform;
    }

    [System.Serializable]
    public class Info
    {
        [FoldoutGroup("Info")]
        [HorizontalGroup("Info/Up", LabelWidth = 20)]
        [SerializeField] private PlatformType _leftUp;
        [HorizontalGroup("Info/Up")]
        [SerializeField] private PlatformType _up;
        [HorizontalGroup("Info/Up")]
        [SerializeField] private PlatformType _rightUp;

        [HorizontalGroup("Info/Middle", LabelWidth = 20)]
        [SerializeField] private PlatformType _left;
        [HorizontalGroup("Info/Middle")]
        [PreviewField(Height = 80)]
        [SerializeField] private GameObject _prefab;
        [HorizontalGroup("Info/Middle")]
        [SerializeField] private PlatformType _right;

        [HorizontalGroup("Info/Down", LabelWidth = 20)]
        [SerializeField] private PlatformType _leftDown;
        [HorizontalGroup("Info/Down")]
        [SerializeField] private PlatformType _down;
        [HorizontalGroup("Info/Down")]
        [SerializeField] private PlatformType _rightDown;

        public GameObject Prefab => _prefab;
        public PlatformType[,] Platform
        {
            get
            {
                PlatformType[,] platforms = new PlatformType[3,3];
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
