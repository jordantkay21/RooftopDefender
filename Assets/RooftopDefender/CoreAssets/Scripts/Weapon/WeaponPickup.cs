using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayosGames.RooftopDefender.Player.Weapon;

namespace KayosGames.RooftopDefender.Weapon
{
    public class WeaponPickup : MonoBehaviour
    {
        public RaycastWeapon weaponPrefab;

        private void OnTriggerEnter(Collider other)
        {
            ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
            if (activeWeapon)
            {
                RaycastWeapon newWeapon = Instantiate(weaponPrefab);
                activeWeapon.Equip(newWeapon, newWeapon.weaponLocalPosition);
            }
        }
    }
}

