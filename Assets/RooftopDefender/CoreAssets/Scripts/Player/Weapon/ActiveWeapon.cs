using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using KayosGames.RooftopDefender.Weapon;

namespace KayosGames.RooftopDefender.Player.Weapon
{
    public class ActiveWeapon : MonoBehaviour
    {
        public Transform crosshairTarget;
        public UnityEngine.Animations.Rigging.Rig handIk;

        public Transform weaponParent;
        public Transform weaponLeftGrip;
        public Transform weaponRightGrip;
        
        [SerializeField]
        private RaycastWeapon _weapon;
        [SerializeField]
        private Animator _anim;
        [SerializeField]
        private AnimatorOverrideController _animOverride;
        [SerializeField]
        private Vector3 _weaponPosition;

        // Start is called before the first frame update
        void Start()
        {
            _anim = GetComponent<Animator>();
            _animOverride = _anim.runtimeAnimatorController as AnimatorOverrideController;

            RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
            if (existingWeapon)
                Equip(existingWeapon, existingWeapon.weaponLocalPosition);
            else
            {
                handIk.weight = 0.0f;
                _anim.SetLayerWeight(1, 0.0f);
            }
            
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

        public void Equip(RaycastWeapon newWeapon, Vector3 position)
        {

             
            if (_weapon)
                Destroy(_weapon.gameObject);


            _weapon = newWeapon;
            _weaponPosition = position;
            _weapon.bulletRaycastDestination = crosshairTarget;

            _weapon.transform.parent = weaponParent;
            _weapon.transform.localPosition = position;
            _weapon.transform.localRotation = Quaternion.identity;

            handIk.weight = 1.0f;
            _anim.SetLayerWeight(1, 1.0f);

            Invoke(nameof(SetAnimationDelayed), 0.001f);
        }

        private void SetAnimationDelayed()
        {
            _animOverride["weapon_anim_empty"] = _weapon.weaponAnimation;
        }

        [ContextMenu("Save Weapon Pose")]
        private void SaveWeaponPose()
        {
            GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
            recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
            recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
            recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
            recorder.TakeSnapshot(0.0f);
            recorder.SaveToClip(_weapon.weaponAnimation);
        }
    }
}
