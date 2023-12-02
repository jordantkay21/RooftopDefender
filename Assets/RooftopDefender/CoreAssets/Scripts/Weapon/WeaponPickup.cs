using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayosGames.RooftopDefender.Player.Weapon;

namespace KayosGames.RooftopDefender.Weapon
{
    public class WeaponPickup : MonoBehaviour
    {
        [Tooltip("Weapon to be Picked Up")]
        public WeaponStats weaponPrefab;

        /// <summary>
        /// Gains access to the ActiveWeapon script found within the player gameObject. 
        /// If ActiveWeapon Component is found, instantiate a new weapon.
        /// Pushes the newly instantiated weapon and its local position to activeWeapon.Equip.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
            if (activeWeapon)
            {
                WeaponStats newWeapon = Instantiate(weaponPrefab);
                activeWeapon.Equip(newWeapon, newWeapon.weaponLocalPosition);
            }
        }
    }
}

