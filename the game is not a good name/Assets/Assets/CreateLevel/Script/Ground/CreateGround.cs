using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    [System.Serializable]
    public class CreateGround : Create<GroundPlatformType>
    {
        [SerializeField] private GameObject _empty;
        [SerializeField] private Transform _platforParents;

        public void Create(MatrixInfo<GroundPlatformType> matrixInfo, GameObject defaultObj, Vector3 position, PlatformInfo<GroundPlatformType> info)
        {
            GameObject obj = MonoBehaviour.Instantiate(_empty);
            obj.transform.position = position;
            obj.transform.SetParent(_platforParents);
            obj.name = ("Platform");
            for (int i = 1; i < matrixInfo.X - 1; i++)
            {
                for (int j = 1; j < matrixInfo.Z - 1; j++)
                {
                    if (matrixInfo.PlatformType[i, j] == GroundPlatformType.Default)
                    {
                        GameObject platform = MonoBehaviour.Instantiate(defaultObj);
                        platform.transform.position = matrixInfo.StartPosition + new Vector3(i, 0, j);
                        platform.transform.SetParent(obj.transform);
                    }
                    else
                    {
                        _onCheckVertical += CheckVertical;
                        _onCheckDiagonal += CheckDiagonal;
                        CheckType(matrixInfo, info, obj.transform, i, j);
                        _onCheckVertical -= CheckVertical;
                        _onCheckDiagonal -= CheckDiagonal;
                    }
                }
            }
        }

        private bool CheckVertical(GroundPlatformType type, GroundPlatformType info, GroundPlatformType[,] rot, Vector2 vector1)
        {
            if (type == GroundPlatformType.Transition)
            {
                type = info;
            }
            else if (type != GroundPlatformType.Default && type != info && info != GroundPlatformType.Transition)
            {
                type = GroundPlatformType.Default;
            }
            if (rot[(int)vector1.x, (int)vector1.y] != type)
            {
                return false;
            }

            return true;
        }
        private bool CheckDiagonal(GroundPlatformType type, GroundPlatformType info, GroundPlatformType[,] rot, Vector2 vector1, Vector2 vector2)
        {
            if (type == GroundPlatformType.Transition)
            {
                type = info;
            }
            if (rot[(int)vector1.x, (int)vector1.y] == rot[(int)vector1.y, 2 - (int)vector1.x] && rot[(int)vector1.x, (int)vector1.y] != GroundPlatformType.Default &&
                rot[(int)vector2.x, (int)vector2.y] != type)
            {
                return false;
            }

            return true;
        }
    }
}
