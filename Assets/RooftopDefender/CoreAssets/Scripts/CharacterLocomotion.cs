using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snipe.PlayerInputs
{
    public class CharacterLocomotion : MonoBehaviour
    {
        [Header("Character Components")]
        public Animator animator;
        public PlayerInputs playerInputs;

        [Header("Movement Input")]
        public Vector2 move;

        // Start is called before the first frame update
        void Start()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            if (playerInputs == null)
                playerInputs = GetComponent<PlayerInputs>();

        }

        // Update is called once per frame
        void Update()
        {
            move.x = playerInputs.move.x;
            move.y = playerInputs.move.y;

            animator.SetFloat("InputX", move.x, 0.05f, Time.deltaTime);
            animator.SetFloat("InputY", move.y, 0.05f, Time.deltaTime);
        }                                                  
    }
}
