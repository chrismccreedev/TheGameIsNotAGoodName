using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    public enum GroundPlatformType
    {
        Default,
        Ground,
        Asphalt,

        Transition
    }

    public class CreateGroundController : SerializedMonoBehaviour
    {
        [FoldoutGroup("Matrix")]
        [SerializeField] private TypeInColor<GroundPlatformType>[] _typeInColor;
        [FoldoutGroup("Matrix")]
        [SerializeField] private MatrixInfo<GroundPlatformType> _matrixInfo = new MatrixInfo<GroundPlatformType>();
        [FoldoutGroup("Matrix")]
        [SerializeField, MinValue(0.5f), MaxValue(1)] private float _steap;

        [FoldoutGroup("Create")]
        [SerializeField] private CreateGround _create;

        [FoldoutGroup("Settings")]
        [SerializeField] private PlatformGroundCheck _platformCheck;
        [FoldoutGroup("Settings")]
        [SerializeField] private PlatformsGroundInfo _platformInfo = new PlatformsGroundInfo();

        private Matrix _matrix = new Matrix();

        [FoldoutGroup("Matrix"), Button]
        private void CreateMatrix()
        {
            _matrixInfo = _matrix.PlatformSizeCalculation<GroundPlatformType>(gameObject, _steap);
            _matrixInfo.PlatformType = _matrix.CtreateTypeMatrix<GroundPlatformType>(_platformCheck, _matrixInfo, _steap);
            _matrixInfo.PlatformColor = _matrix.ConvertTypeToColor<GroundPlatformType>(_typeInColor, _matrixInfo);
        }
        [FoldoutGroup("Matrix"), Button]
        private void ConvertColorToType()
        {
            _matrixInfo.PlatformType = _matrix.ConvertColorToType<GroundPlatformType>(_typeInColor, _matrixInfo);
        }

        [FoldoutGroup("Create"), Button]
        private void Create()
        {
            _create.Create(_matrixInfo, _platformInfo.Default, transform.position, _platformInfo);
        }

        // --- nested classes ---------------------------------------------------------
        // ----------------------------------------------------------------------------

        [System.Serializable]
        private class PlatformsGroundInfo : PlatformInfo<GroundPlatformType>
        {
            [SerializeField] private GameObject _default;
            public GameObject Default => _default;
        }

        [System.Serializable]
        private class PlatformGroundCheck : PlatformCheck<GroundPlatformType>
        {
            [FoldoutGroup("Button")]
            [SerializeField, MinValue(0)] private int _index;

            [FoldoutGroup("Button")]
            [Button]
            private void AddTrigger()
            {
                _values[_index].Add(_trigger, _parents);
            }
            [FoldoutGroup("Button")]
            [Button]
            private void DestroyTrigger()
            {
                _values[_index].Clear();
            }
        }
    }
}
