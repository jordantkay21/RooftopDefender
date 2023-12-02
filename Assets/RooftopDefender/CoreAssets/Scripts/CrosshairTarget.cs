using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayosGames.RooftopDefender.Utility
{
    /// <summary>
    /// Finds the impact zone between what the character is looking at (AimLookAt GameObject) and what the crosshair is aiming at (CrosshairTarget GameObject)
    /// </summary>
    public class CrosshairTarget : MonoBehaviour
    {
        [SerializeField]
        Camera _mainCamera;

        Ray _ray;
        RaycastHit _hitInfo;

        // Start is called before the first frame update
        void Start()
        {
            if (_mainCamera == null) _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            _ray.origin = _mainCamera.transform.position;
            _ray.direction = _mainCamera.transform.forward;

            if (Physics.Raycast(_ray, out _hitInfo))
                transform.position = _hitInfo.point;
            else
                transform.position = _ray.origin + _ray.direction * 1000.0f;

        }
    }
}