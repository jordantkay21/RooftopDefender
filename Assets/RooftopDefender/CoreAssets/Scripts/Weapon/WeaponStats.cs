using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayosGames.RooftopDefender.Player.Weapon;

namespace KayosGames.RooftopDefender.Weapon
{
    public class WeaponStats : MonoBehaviour
    {
        [Header("Bullet Effects")]
        public ParticleSystem[] muzzleFlash;
        public ParticleSystem hitEffect;
        public TrailRenderer tracerEffect;

        [Header("Bullet Raycast")]
        [Tooltip("Start of bullet Raycast | Barrel of Gun | raycastOrigin GameObject")]
        public Transform bulletRaycastOrigin;
        [Tooltip("End of bullet Raycast | CrosshairTarget GameObject")]
        public Transform bulletRaycastDestination;

        [Header("Components")]
        public ActiveWeapon.WeaponSlot weaponSlot;
        public string weaponName;

        
        Ray _bulletRay;
        RaycastHit _bulletHitInfo;

        public void FireBullet()
        {
            foreach (ParticleSystem particle in muzzleFlash)
                particle.Emit(1);

            _bulletRay.origin = bulletRaycastOrigin.position;
            _bulletRay.direction = bulletRaycastDestination.position - bulletRaycastOrigin.position;

            TrailRenderer tracer = Instantiate(tracerEffect, _bulletRay.origin, Quaternion.identity);
            tracer.AddPosition(_bulletRay.origin);

            if (Physics.Raycast(_bulletRay, out _bulletHitInfo))
            {

                hitEffect.transform.position = _bulletHitInfo.point;
                hitEffect.transform.forward = _bulletHitInfo.normal;
                hitEffect.Emit(1);

                tracer.transform.position = _bulletHitInfo.point;

                Rigidbody rb2d = _bulletHitInfo.collider.GetComponent<Rigidbody>();
                if (rb2d)
                    rb2d.AddForceAtPosition(_bulletRay.direction * 20, _bulletHitInfo.point, ForceMode.Impulse);

                
            }
        }

    }

}
