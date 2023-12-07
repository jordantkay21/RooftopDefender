using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using KayosGames.RooftopDefender.Weapon;

namespace KayosGames.RooftopDefender.Player.Weapon
{
    public class ActiveWeapon : MonoBehaviour
    {
        public enum WeaponSlot
        {
            Primary = 0,
            Secondary = 1
        }

        [Header("Weapon Placement")]
        public Transform[] weaponSlots;
        public Transform weaponLeftGrip;
        public Transform weaponRightGrip;
        public Animator rigController;

        [Header("Components")]
        public UnityEngine.Animations.Rigging.Rig handIk;

        [SerializeField]
        private List<WeaponStats> _equippedWeapons = new List<WeaponStats>();
        [SerializeField]
        private WeaponStats[] _equippedWeaponsArray;
        [SerializeField]
        private int _activeWeaponIndex;

        [Header("Misc.")]
        public Transform crosshairTarget;
        [SerializeField]
        private bool isHolstered = false;
        

        // Start is called before the first frame update
        void Start()
        {
            WeaponStats existingWeapon = GetComponentInChildren<WeaponStats>();

            _equippedWeaponsArray = new WeaponStats[weaponSlots.Length];
        }

        #region Input Events
        public void FireEvent()
        {
            WeaponStats weapon = GetWeapon(_activeWeaponIndex);
            if (weapon && !isHolstered)
            {
                weapon.FireBullet();
            }

        }
        #endregion

        WeaponStats GetWeapon(int index)
        {
            if (index < 0 || index >= _equippedWeaponsArray.Length)
                return null;

            return _equippedWeaponsArray[index];
        }

        public void Equip(WeaponStats newWeapon)
        {
            int weaponSlotIndex = (int)newWeapon.weaponSlot;
            WeaponStats weapon = GetWeapon(weaponSlotIndex);
             
            if (weapon)
                Destroy(weapon.gameObject);


            weapon = newWeapon;
            weapon.bulletRaycastDestination = crosshairTarget;
            weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);

            _equippedWeaponsArray[weaponSlotIndex] = weapon;

            SetActiveWeapon(newWeapon.weaponSlot);
        }

        public void SetActiveWeapon(WeaponSlot weaponSlot)
        {
            int holsterIndex = _activeWeaponIndex;
            int activateIndex = (int)weaponSlot;
            StartCoroutine(SwitchWeaponPickup(holsterIndex, activateIndex));
        }

        #region AnimationControl
        public void SwitchWeapon()
        {
            WeaponStats weapon = GetWeapon(_activeWeaponIndex);

            if (_equippedWeaponsArray[_activeWeaponIndex] != null)
            {
                if (weapon.weaponSlot == WeaponSlot.Primary)
                {
                    if (_equippedWeaponsArray[(int)WeaponSlot.Secondary] != null)
                        SetActiveWeapon(WeaponSlot.Secondary);

                }
                else if (weapon.weaponSlot == WeaponSlot.Secondary)
                {
                    if (_equippedWeaponsArray[(int)WeaponSlot.Primary] != null)
                        SetActiveWeapon(WeaponSlot.Primary);
                }
            }       
        }
        public void ToggleActiveWeaponHolster()
        {
            bool isHolster = rigController.GetBool("holster_weapon");

            if (!isHolster)
                StartCoroutine(HolsterWeapon(_activeWeaponIndex));
            else
                StartCoroutine(ActivateWeapon(_activeWeaponIndex));
               
        }
        IEnumerator SwitchWeaponPickup(int holsterIndex, int activeIndex)
        {
            yield return StartCoroutine(HolsterWeapon(holsterIndex));
            yield return StartCoroutine(ActivateWeapon(activeIndex));
            _activeWeaponIndex = activeIndex;
        }

        IEnumerator HolsterWeapon(int index)
        {
            WeaponStats weapon = GetWeapon(index);
            if (weapon)
            {
                rigController.SetBool("holster_weapon", true);

                yield return new WaitForSeconds(0.1f);
                do
                {
                    yield return new WaitForEndOfFrame();
                } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

                isHolstered = true;
            }
            
        }

        IEnumerator ActivateWeapon(int index)
        {
            WeaponStats weapon = GetWeapon(index);
            if (weapon)
            {
                rigController.SetBool("holster_weapon", false);
                rigController.Play("equip_" + weapon.weaponName);
                do
                {
                    yield return new WaitForEndOfFrame();
                } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
                
                isHolstered = false;
            }
        }
        #endregion
    }
}
