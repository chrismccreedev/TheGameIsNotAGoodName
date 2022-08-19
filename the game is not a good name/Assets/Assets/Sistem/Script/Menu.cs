using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected SistemInfo _info;

    protected KeyTransform _key;
    protected CameraRotation _rotation;

    protected virtual void Start()
    {
        _key = FindObjectOfType<KeyTransform>();
        _rotation = FindObjectOfType<CameraRotation>();
    }

    public abstract void Open();
    public abstract void Close();
    public abstract KeyCode Key();
}
