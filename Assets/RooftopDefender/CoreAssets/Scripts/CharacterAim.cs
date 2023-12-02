using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayosGames.RooftopDefender.Weapon;

namespace KayosGames.RooftopDefender.Player
{
    public class CharacterAim : MonoBehaviour
    {
        [Header("COMPONENTS")]

        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private RaycastWeapon _raycastWeapon;

        [Header("CUSTOM VARIABLES")]
        [Tooltip("How fast the character rotates")]
        [SerializeField]
        private float _turnSpeed = 15;

        #region Program Updates
        void Start()
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;
            if (_raycastWeapon == null)
                _raycastWeapon = GetComponentInChildren<RaycastWeapon>();

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

        #endregion

        #region Input Events
        public void FireEvent()
        {
            _raycastWeapon.FireBullet();
        }
        #endregion
    }

}
