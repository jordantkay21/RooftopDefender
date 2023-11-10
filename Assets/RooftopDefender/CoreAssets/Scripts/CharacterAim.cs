using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : MonoBehaviour
{   
    [Tooltip("How fast the character rotates")]
    [SerializeField]
    private float _turnSpeed = 15;
    [SerializeField]
    private Camera _mainCamera;

    void Start()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Smoothly rotates Character to match the rotation of the Main Camera [FreeLook Virtual Camera]
    /// </summary>
    private void FixedUpdate()
    {
        float yawCamera = _mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.fixedDeltaTime);
    }
}
