using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KayosGames.RooftopDefender.Player
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField]
        private CharacterLocomotion characterLocomotion;
        [SerializeField]
        private CharacterAim characterAim;

        private void Start()
        {
            if (characterLocomotion == null)
                characterLocomotion = gameObject.GetComponent<CharacterLocomotion>();
            if (characterAim == null)
                characterAim = gameObject.GetComponent<CharacterAim>();
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
            characterAim.FireEvent();
        }
    }
}