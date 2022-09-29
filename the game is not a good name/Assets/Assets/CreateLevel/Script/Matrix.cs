using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateLevel
{
    [System.Serializable]
    public class TypeInColor<T>
    {
        [SerializeField] private T _type;
        [SerializeField] private Color _color;

        public T Type => _type;
        public Color Color => _color;
    }

    public class Matrix
    {
        private Math _math = new Math();
        public T[,] CtreateTypeMatrix<T>(PlatformCheck<T> platform, MatrixInfo<T> matrixInfo, float step)
        {
            int x = matrixInfo.X;
            int z = matrixInfo.Z;
            T[,] newMatrix = new T[x, z];

            for(int i = 0; i < x; i += (int)(1/step))
            {
                for(int j =0; j < z; j += (int)(1 / step))
                {
                    newMatrix[i, j] = Check<T>(platform, new Vector3(i * step + matrixInfo.StartPosition.x, 0, j * step + matrixInfo.StartPosition.z), i, j);
                }
            }

            return newMatrix;
        }
        private T Check<T>(PlatformCheck<T> platform, Vector3 position, int x, int z)
        {
            foreach (var checkValue in platform.Values)
            {
                foreach (GameObject check in checkValue.Check)
                {
                    Vector3 min = check.GetComponent<Collider>().bounds.min;
                    Vector3 max = check.GetComponent<Collider>().bounds.max;
                    if (position.x >= _math.MathPosition(min.x, -1) && position.z >= _math.MathPosition(min.z, -1) && position.x <= _math.MathPosition(max.x, 1) && position.z <= _math.MathPosition(max.z, 1))
                    {
                        return checkValue.PlatformType;
                    }
                }
            }
            return default(T);
        }

        public Color[,] ConvertTypeToColor<T>(TypeInColor<T>[] typeInColor, MatrixInfo<T> matrixInfo)
        {
            int x = matrixInfo.X;
            int z = matrixInfo.Z;
            Color[,] color = new Color[x, z];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    foreach (TypeInColor<T> inColor in typeInColor)
                    {
                        if (inColor.Type.GetType().GetEnumName(inColor.Type) == matrixInfo.PlatformType[i, j].GetType().GetEnumName(matrixInfo.PlatformType[i, j]))
                        {
                            color[i, (z - 1) - j] = inColor.Color;
                            break;
                        }
                    }
                }
            }
            return color;
        }
        
        public T[,] ConvertColorToType<T>(TypeInColor<T>[] typeInColor, MatrixInfo<T> matrixInfo)
        {
            int x = matrixInfo.X;
            int z = matrixInfo.Z;
            T[,] type = new T[x, z];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    foreach(TypeInColor<T> inType in typeInColor)
                    {
                        if (Range(inType.Color, matrixInfo.PlatformColor[i, j]))
                        {
                            type[i, (z - 1) - j] = inType.Type;
                            break;
                        }
                    }
                }
            }
            return type;
        }

        private bool Range(Color color1, Color color2)
        {
            float range = 0.01f;
            if(color1.r > (color2.r + range) || color1.r < (color2.r - range))
            {
                return false;
            }
            if (color1.g > (color2.g + range) || color1.g < (color2.g - range))
            {
                return false;
            }
            if (color1.b > (color2.b + range) || color1.b < (color2.b - range))
            {
                return false;
            }
            return true;
        }

        public MatrixInfo<T> PlatformSizeCalculation<T>(GameObject obj, float value)
        {
            MatrixInfo<T> matrixInfo = new MatrixInfo<T>();
            int X = _math.MathPosition(obj.GetComponent<Collider>().bounds.min.x, -1);
            int Z = _math.MathPosition(obj.GetComponent<Collider>().bounds.min.z, -1);

            matrixInfo.StartPosition = new Vector3(X, obj.transform.localPosition.y, Z);

            X = _math.MathPosition(obj.GetComponent<Collider>().bounds.max.x, 1);
            Z = _math.MathPosition(obj.GetComponent<Collider>().bounds.max.z, 1);


            matrixInfo.X = (int)((X - (int)matrixInfo.StartPosition.x) / value) + 1;
            matrixInfo.Z = (int)((Z - (int)matrixInfo.StartPosition.z) / value) + 1;

            matrixInfo.PlatformColor = new Color[matrixInfo.X, matrixInfo.Z];
            matrixInfo.PlatformType = new T[matrixInfo.X, matrixInfo.Z];

            return matrixInfo;
        }

        private class Math
        {
            public int MathPosition(float pos, int value)
            {
                int posInt = (int)pos;
                if (Mathf.Abs(posInt + value * 0.5f) > Mathf.Abs(pos))
                {
                    posInt -= value;
                }
                return posInt;
            }
        }
    }


    [System.Serializable]
    public class MatrixInfo<T>
    {
        [TableMatrix, ShowInInspector]
        private Color[,] _platformColor;
        private T[,] _platformType;

        private Vector3 _startPosition;
        private int _numX;
        private int _numZ;

        public Color[,] PlatformColor
        {
            get { return _platformColor; }
            set { _platformColor = value; }
        }
        public T[,] PlatformType
        {
            get { return _platformType; }
            set { _platformType = value; }
        }
        public Vector3 StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }
        public int X
        {
            get { return _numX; }
            set 
            { 
                if(value > 0)
                    _numX = value;
                else
                    _numX = 0;
            }
        }
        public int Z
        {
            get { return _numZ; }
            set
            {
                if(value > 0)
                    _numZ = value;
                else
                    _numZ=0;
            }
        }
    }
}