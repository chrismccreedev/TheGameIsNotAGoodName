using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateLevel
{
    public class Create<T>
    {
        public event System.Func<T, T, T[,], Vector2, bool> _onCheckVertical;
        public event System.Func<T, T, T[,], Vector2, Vector2, bool> _onCheckDiagonal;
        protected void CheckType(MatrixInfo<T> matrixInfo, PlatformInfo<T> info, Transform parents, int x, int z)
        {
            foreach(ListInfo<T> type in info.TypeGroupe)
            {
                if(type.Type.GetType().GetEnumName(type.Type) == matrixInfo.PlatformType[x, z].GetType().GetEnumName(matrixInfo.PlatformType[x, z]))
                {
                    CheckPlatform(matrixInfo, type, parents, x, z);
                }
            }
        }
        private void CheckPlatform(MatrixInfo<T> matrixInfo, ListInfo<T> info, Transform parents, int x, int z)
        {
            foreach(Info<T> type in info.Platform)
            {
                T[,] rot = type.Platform;
                for(int i = 0; i < 4; i++)
                {
                    bool check = true;
                    Vector2 vector1 = new Vector2(0, 1);
                    Vector2 vector2 = new Vector2(0, 2);

                    for (int j = 0; j < 4; j++)
                    {
                        T test = matrixInfo.PlatformType[x + ((int)vector1.x - 1), z + ((int)vector1.y - 1)];
                        check = _onCheckVertical(test, info.Type, rot, vector1);
                        if(!check)
                        {
                            break;
                        }
                        test = matrixInfo.PlatformType[x + ((int)vector2.x - 1), z + ((int)vector2.y - 1)];
                        check = _onCheckDiagonal(test, info.Type, rot, vector1, vector2);
                        if (!check)
                        {
                            break;
                        }

                        vector1 = new Vector2(vector1.y, (2 - vector1.x));
                        vector2 = new Vector2(vector2.y, (2 - vector2.x));
                    }

                    if(check)
                    {
                        GameObject platform = MonoBehaviour.Instantiate(type.Prefab);
                        platform.transform.position = matrixInfo.StartPosition + new Vector3(x, 0, z);
                        platform.transform.SetParent(parents);
                        platform.transform.eulerAngles += new Vector3(0, (90 * i) - 90, 0);
                        return;
                    }

                    rot = RoteMatrix(rot);
                }
            }
        }

        public T[,] RoteMatrix(T[,] rot)
        {
            T[,] newRot = new T[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newRot[(j), (2 - i)] = rot[i, j];
                }
            }
            return newRot;
        }
    }
}
