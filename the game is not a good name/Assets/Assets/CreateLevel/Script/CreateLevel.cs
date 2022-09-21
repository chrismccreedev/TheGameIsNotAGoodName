using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    public enum PlatformType
    {
        Default,
        Ground,
        Asphalt,

        Transition
    }
    public class CreateLevel : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject _empty;
        [SerializeField] private Transform _platforParents;
        [SerializeField] private GameObject _check;
        [SerializeField] private PlatformCheck _platformCheck;
        [SerializeField] private PlatformsInfo _info;
        [SerializeField] private TypeInColor[] _typeInColor;

        private int _numX;
        private int _numZ;

        [ReadOnly, FoldoutGroup("Matrix"), SerializeField, TableMatrix] 
        private Color[,] _platformColor;
        private PlatformType[,] _platformType;

        private Vector3 _startPosition;

        [Button]
        private void CreateMatrix()
        {
            PlatformSizeCalculation();

            GameObject obj = Instantiate(_check);

            for(int i = 0; i < _numX; i++)
            {
                for(int j = 0; j < _numZ; j++)
                {
                    Check(obj, i, j);
                }
            }
            ConvertTypeToColor();
            DestroyImmediate(obj);
        }
        private void PlatformSizeCalculation()
        {
            int X = MatchPosition(GetComponent<Collider>().bounds.min.x, -1);
            int Z = MatchPosition(GetComponent<Collider>().bounds.min.z, -1);

            _startPosition = new Vector3(X, transform.localPosition.y, Z);

            X = MatchPosition(GetComponent<Collider>().bounds.max.x, 1);
            Z = MatchPosition(GetComponent<Collider>().bounds.max.z, 1);


            _numX = X - (int)_startPosition.x + 1;
            _numZ = Z - (int)_startPosition.z + 1;

            _platformColor = new Color[_numX, _numZ];
            _platformType = new PlatformType[_numX, _numZ];
        }

        [Button]
        private void Create()
        {
            GameObject obj = Instantiate(_empty);
            obj.transform.position = transform.position;
            obj.transform.SetParent(_platforParents);
            obj.name = ("Platform");

            for(int i = 1; i < _numX - 1; i++)
            {
                for(int j = 1; j < _numZ - 1; j++)
                {
                    if (_platformType[i, j] == PlatformType.Default)
                    {
                        GameObject platform = Instantiate(_info.Default);
                        platform.transform.position = _startPosition + new Vector3(i, 0, j);
                        platform.transform.SetParent(obj.transform);
                    }
                    else
                    {
                        foreach (var n in _info.TypeGroupe)
                        {
                            if (n.Type == _platformType[i, j])
                            {
                                foreach(Info info in n.Platform)
                                {
                                    CheckPlatform(info.Platform, n, info.Prefab, obj.transform, i, j);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void CheckPlatform(PlatformType[,] matrix, GroupeInfo info, GameObject obj, Transform parents, int x, int z)
        {
            PlatformType[,] rot = matrix;
            for (int r = 0; r < 4; r++)
            {
                bool check = true;
                Vector2 vector1 = new Vector2(0, 1);
                Vector2 vector2 = new Vector2(0, 2);

                for(int i = 0; i < 4; i++)
                {
                    PlatformType test = _platformType[x + ((int)vector1.x - 1), z + ((int)vector1.y - 1)];
                    if (test == PlatformType.Transition)
                    {
                        test = info.Type;
                    }
                    else if (test != PlatformType.Default && test != info.Type && info.Type != PlatformType.Transition)
                    {
                        test = PlatformType.Default;
                    }


                    if(rot[(int)vector1.x, (int)vector1.y] != test)
                    {
                        check = false;
                        break;
                    }

                    test = _platformType[x + ((int)vector2.x - 1), z + ((int)vector2.y - 1)];
                    if (test == PlatformType.Transition)
                    {
                        test = info.Type;
                    }

                    if (rot[(int)vector1.x, (int)vector1.y] == rot[(int)vector1.y, 2 - (int)vector1.x] && rot[(int)vector1.x, (int)vector1.y] != PlatformType.Default && 
                        rot[(int)vector2.x, (int)vector2.y] != test)
                    {
                        check = false;
                        break;
                    }
                    vector1 = new Vector2(vector1.y, (2 - vector1.x));
                    vector2 = new Vector2(vector2.y, (2 - vector2.x));
                }
                if(check)
                {
                    GameObject platform = Instantiate(obj);
                    platform.transform.position = _startPosition + new Vector3(x, 0, z);
                    platform.transform.SetParent(parents);
                    platform.transform.eulerAngles += new Vector3(0, (90 * r)-90, 0);
                    return;
                }
                rot = RoteMatrix(rot);
            }
        }
        private PlatformType[,] RoteMatrix(PlatformType[,] rot)
        {
            PlatformType[,] newRot = new PlatformType[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newRot[(j), (2-i)] = rot[i, j];
                }
            }
            return newRot;
        }

        private void Check(GameObject obj, int x, int z)
        {
            Vector3 vector = new Vector3(x, 0, z);
            obj.transform.position = _startPosition + vector;
            Vector3 position = obj.transform.position;
            foreach(CheckValue checkValue in _platformCheck.CheckValue)
            {
                foreach(GameObject check in checkValue.Check)
                {
                    Vector3 min = check.GetComponent<Collider>().bounds.min;
                    Vector3 max = check.GetComponent<Collider>().bounds.max;
                    if(position.x >= MatchPosition(min.x, -1) && position.z >= MatchPosition(min.z, -1) && position.x <= MatchPosition(max.x, 1) && position.z <= MatchPosition(max.z, 1))
                    {
                        _platformType[x, z] = checkValue.PlatformType;
                        return;
                    }
                }
            }
            _platformType[x, z] = PlatformType.Default;
        }
        private void ConvertTypeToColor()
        {
            for (int i = 0; i < _numX; i++)
            {
                for (int j = 0; j < _numZ; j++)
                {
                    foreach(TypeInColor inColor in _typeInColor)
                    {
                        if(inColor.Type == _platformType[i, j])
                        {
                            _platformColor[i, (_numZ - 1) - j] = inColor.Color;
                            break;
                        }
                    }
                }
            }
        }

        private int MatchPosition(float pos, int value)
        {
            int posInt = (int)pos;
            if (Mathf.Abs(posInt + value * 0.5f ) > Mathf.Abs(pos))
            {
                posInt -= value;
            }
            return posInt;
        }
    }

    [System.Serializable]
    public class TypeInColor
    {
        [SerializeField] private PlatformType _type;
        [SerializeField] private Color _color;

        public PlatformType Type => _type;
        public Color Color => _color;
    }
}