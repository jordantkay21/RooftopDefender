using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using KayosGames.RooftopDefender.Player.Weapon;
using KayosGames.RooftopDefender.Player.Movement;

namespace KayosGames.RooftopDefender.Player.Inputs
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField]
        private CharacterLocomotion characterLocomotion;
        [SerializeField]
        private ActiveWeapon activeWeapon;

        private void Start()
        {
            if (characterLocomotion == null)
                characterLocomotion = gameObject.GetComponent<CharacterLocomotion>();
            if (activeWeapon == null)
                activeWeapon = gameObject.GetComponent<ActiveWeapon>();
        }
        private void OnMovement(InputValue value)
        {
            Vector2 move = value.Get<Vector2>();
            characterLocomotion.MovementEvent(move);
        }

        private void OnJump()
        {
            characterLocomotion.JumpEvent();
        }

        private void OnFire()
        {
            activeWeapon.FireEvent();
        }
    }
}