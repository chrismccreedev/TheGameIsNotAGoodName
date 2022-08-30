using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected SistemInfo _info;

    protected CameraRotation _rotation;

    protected virtual void Start()
    {
        _rotation = FindObjectOfType<CameraRotation>();
    }

    public abstract void Open();
    public abstract void Close();
    public abstract KeyCode Key();
}
