using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(SphereCollider))]
public class TargetAcquisition : MonoBehaviour
{
    [MinValue(1)]
    [SerializeField] private float _radius;
    [SerializeField] private float _rotateY;
    [SerializeField] private float _rotateZ;
    [SerializeField] private Camera _camera;

    [SerializeField, ReadOnly] private GameObject[] _enemys;

    private SphereCollider _sphereCollider;
    private CameraLookAt _lookAt;
    private LookAtRotate _lookAtRotate;
    private CameraRotation _rotation;

    private GameObject _selected;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _radius;
        _sphereCollider.isTrigger = true;
        _sphereCollider.enabled = true;
        _lookAt = FindObjectOfType<CameraLookAt>();
        _rotation = FindObjectOfType<CameraRotation>();
        _lookAtRotate = FindObjectOfType<LookAtRotate>();
        _lookAtRotate.Rotate += NextHorizontalEnableCheck;
        _lookAt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBeacon>() != null)
        {
            AddObjectToArray(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == _selected)
        {
            DisableCheck();
        }
        if (other.GetComponent<EnemyBeacon>() != null)
        {
            RemoveObjectToArray(other.gameObject);
        }
    }
    [Button]
    public void EnableCheck()
    {
        float f = 90;

        float rotCam = _camera.transform.eulerAngles.y - 90;

        if (rotCam > 180f)
        {
            rotCam -= 360f;
        }

        GameObject j = null;
        foreach(GameObject obj in _enemys)
        {
            float rotY = Rotate(obj);
            float value = Mathf.Abs(rotY - rotCam);
            if (value <= _rotateY / 2f)
            {
                if(value<f)
                {
                    f = value;
                    j = obj;
                }
            }
        }
        if(j != null)
        {
            _lookAt.gameObject.SetActive(true);
            _selected = j;
            _rotation.gameObject.SetActive(false);
            _lookAt.Enemy = _selected;
            _selected.GetComponent<EnemyBeacon>().EnableOutline();
        }
    }
    [Button]
    public void DisableCheck()
    {
        _lookAt.gameObject.SetActive(false);
        _rotation.gameObject.SetActive(true);
        _lookAt.Enemy = null;
        foreach (GameObject obj in _enemys)
        {
            obj.GetComponent<EnemyBeacon>().DisableOutline();
        }
    }

    private void AddObjectToArray(GameObject obj)
    {
        GameObject[] oldArray = _enemys;
        _enemys = new GameObject[oldArray.Length + 1];
        for (int i = 0; i < oldArray.Length; i++)
        {
            _enemys[i] = oldArray[i];
        }
        _enemys[_enemys.Length - 1] = obj;
    }
    private void RemoveObjectToArray(GameObject obj)
    {
        GameObject[] oldArray = _enemys;
        _enemys = new GameObject[oldArray.Length - 1];
        int a = 0;
        for (int i = 0; i < oldArray.Length; i++)
        {
            if(oldArray[i] == obj)
            {
                a++;
            }
            if(i+a >= oldArray.Length)
            {
                return;
            }
            _enemys[i] = oldArray[i + a];
        }
    }
    private float Rotate(GameObject obj)
    {
        float conversionValue = 180f / Mathf.PI;

        float x = obj.transform.position.x - _camera.transform.position.x;
        float y = obj.transform.position.y - _camera.transform.position.y;
        float z = obj.transform.position.z - _camera.transform.position.z;
        float distans = Mathf.Sqrt((x * x) + (z * z));

        float cornerY = Mathf.Acos(x / distans) * conversionValue;

        if (Mathf.Asin(z / distans) * conversionValue < 0)
        {
            return cornerY;
        }
        else
        {
            return -cornerY;
        }
    }
    public void NextHorizontalEnableCheck(int direction)
    {
        float f = 90;
        float startRot = Rotate(_selected);
        GameObject j = null;

        foreach (GameObject obj in _enemys)
        {
            float rotY = Rotate(obj);
            float rot = (rotY - startRot) * direction;
            if(rot < -360 + _rotateY)
            {
                rot += 360f;
            }
            if(rot > 0 && rot <= _rotateY)
            {
                if ((rotY - startRot) * direction < f)
                {
                    f = (rotY - startRot) * direction;
                    j = obj;
                }
            }
        }

        if (j != null)
        {
            _selected.GetComponent<EnemyBeacon>().DisableOutline();
            _selected = j;
            _lookAt.Enemy = _selected;
            _selected.GetComponent<EnemyBeacon>().EnableOutline();
        }
    }
}
