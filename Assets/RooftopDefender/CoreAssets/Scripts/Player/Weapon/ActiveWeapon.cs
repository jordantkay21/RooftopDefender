using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using KayosGames.RooftopDefender.Weapon;

namespace KayosGames.RooftopDefender.Player.Weapon
{
    public class ActiveWeapon : MonoBehaviour
    {
        [Header("Weapon Placement")]
        public Transform weaponParent;
        public Transform weaponLeftGrip;
        public Transform weaponRightGrip;
        [SerializeField]
        private Vector3 _weaponPosition;
        public Animator rigController;

        [Header("Components")]
        public UnityEngine.Animations.Rigging.Rig handIk;
        [SerializeField]
        private WeaponStats _weapon;

        [Header("Misc.")]
        public Transform crosshairTarget;

        // Start is called before the first frame update
        void Start()
        {
            WeaponStats existingWeapon = GetComponentInChildren<WeaponStats>();
            if (existingWeapon)
                Equip(existingWeapon, existingWeapon.weaponLocalPosition);

            
        }

        #region Input Events
        public void FireEvent()
        {
            if (_weapon)
            {
                _weapon.FireBullet();
            }

        }
        #endregion

        public void Equip(WeaponStats newWeapon, Vector3 position)
        {

             
            if (_weapon)
                Destroy(_weapon.gameObject);


            _weapon = newWeapon;
            _weaponPosition = position;
            _weapon.bulletRaycastDestination = crosshairTarget;

            _weapon.transform.parent = weaponParent;
            _weapon.transform.localPosition = position;
            _weapon.transform.localRotation = Quaternion.identity;

            Debug.Log("equip_" + _weapon.weaponName);
            rigController.Play("equip_" + _weapon.weaponName);
        }

        public void HolsterWeapon()
        {
            Debug.Log("HolsterWeapon method called");
            bool isHolster = rigController.GetBool("holster_weapon");
            Debug.Log("1. isHolster = " + isHolster.ToString());
            rigController.SetBool("holster_weapon", !isHolster);
            Debug.Log("2. isHolster = " + isHolster.ToString());
        }
    }
}
