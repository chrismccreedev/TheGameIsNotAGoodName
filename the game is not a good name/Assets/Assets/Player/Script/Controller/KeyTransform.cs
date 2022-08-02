using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyTransform : MonoBehaviour
{
    [SerializeField] private KeyInfo _keyInfo;

    public event Action<int, int> KeyDown;

    private void FixedUpdate()
    {
        if(Input.GetKey(_keyInfo._keyForward))
        {
            KeyDown(1, 0);
        }
        if(Input.GetKey(_keyInfo._keyBackward))
        {
            KeyDown(-1, 0);
        }
        if (Input.GetKey(_keyInfo._keyRight))
        {
            KeyDown(0, 1);
        }
        if (Input.GetKey(_keyInfo._keyLeft))
        {
            KeyDown(0, -1);
        }
    }
}
