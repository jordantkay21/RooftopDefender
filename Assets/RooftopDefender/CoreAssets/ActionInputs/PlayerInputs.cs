using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snipe.PlayerInputs
{
    public class PlayerInputs : MonoBehaviour
    {
        private PlayerInputActions _input;

        [Header("Character Input Values")]
        public Vector2 move;

        private void Start()
        {
            _input = new PlayerInputActions();
            _input.Player.Enable();

            _input.Player.Movement.performed += Movement_performed;
            _input.Player.Movement.canceled += Movement_canceled;
        }

        private void Movement_canceled(InputAction.CallbackContext obj)
        {
            move = obj.ReadValue<Vector2>();
        }

        private void Movement_performed(InputAction.CallbackContext obj)
        {
            move = obj.ReadValue<Vector2>();
        }
    }
}