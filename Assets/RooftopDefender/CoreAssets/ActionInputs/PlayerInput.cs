using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoSingleton<PlayerInput>
{
    [Header("Character Input Values")]
    public Vector2 move;
    public bool jump;
    public bool sprint;

    public void OnMovement(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }
}
