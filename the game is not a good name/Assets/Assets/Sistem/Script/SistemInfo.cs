using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SistemInfo", menuName = "SistemInfo")]
public class SistemInfo : ScriptableObject
{
    public KeyCode _pause;
    public KeyCode _inventory;
}
