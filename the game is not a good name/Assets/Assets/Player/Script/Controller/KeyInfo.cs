using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyInfo", menuName = "Key Info")]
public class KeyInfo : ScriptableObject
{
    public KeyCode _keyForward;
    public KeyCode _keyBackward;
    public KeyCode _keyRight;
    public KeyCode _keyLeft;
}
