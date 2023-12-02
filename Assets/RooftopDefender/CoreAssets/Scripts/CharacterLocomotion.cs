using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayosGames.RooftopDefender.Player
{
    public class CharacterLocomotion : MonoBehaviour
    {
        [Header("Character Settings")]
        [Tooltip("Height character can jump")]
        public float jumpHeight;
        [Tooltip("Force pulling character down")]
        public float gravity;
        [Tooltip("value to allow character to walk down steps/slopes, opposite of Step Offset")]
        public float stepDown; 
        [Tooltip("Controls how much the character can move while in air")]
        public float airControl;
        [Tooltip("value to adjust character speed while moving in air. 1 = nuetral | >1 = slow down | <1 = speed up")]
        public float jumpDamp; 
        [Tooltip("value to adjust character speed while moving on the ground. 1 = nuetral | >1 = slow down | <1 = speed up")]
        public float groundSpeed;


        [Header("Character Components")]
        public Animator animator;
        public CharacterController characterController;

        [Header("Movement Input")]
        [Tooltip("movement input value")]
        private Vector2 _move;
        [Tooltip("stores the rootMotion from the Animator Component")]
        private Vector3 _rootMotion;
        [Tooltip("refers to the speed and direction at which an object is moving")]
        private Vector3 _velocity;
        [SerializeField]
        [Tooltip("Jumping State")]
        private bool _isJumping;

        #region ProgramUpdates
        void Start()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
        }
        void Update()
        {
            animator.SetFloat("InputX", _move.x, 0.05f, Time.deltaTime);
            animator.SetFloat("InputY", _move.y, 0.05f, Time.deltaTime);
        }

        /// <summary>
        /// calculations that require calculations involving physics
        /// </summary>
        private void FixedUpdate()
        {
            if (_isJumping) //if is in air state
            {
                UpdateInAir();
            }
            else //if grounded state
            {
                UpdateOnGround();
            }
        }

        #endregion

        #region Input Events
        public void MovementEvent(Vector2 move)
        {
            _move.x = move.x;
            _move.y = move.y;
        }
        public void JumpEvent()
        {
            Debug.Log("Jump event called from LocoMotion");
            Jump();
        }

        /// <summary>
        /// Overrides the Animator's abiltiy to move the character. Pushes responsibility of movement on Character Controller via script.
        /// </summary>
        private void OnAnimatorMove()
        {
            _rootMotion += animator.deltaPosition;
        }
        #endregion


        /// <summary>
        /// Controls how the character acts while grounded. Also calls SetInAir() if character walks off ledge.
        /// </summary>
        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = _rootMotion * groundSpeed;
            Vector3 stepDownAmount = Vector3.down * stepDown;

            characterController.Move(stepForwardAmount + stepDownAmount);
            _rootMotion = Vector3.zero;

            if (!characterController.isGrounded)
            {
                AirborneState(0);
            }
        }

        /// <summary>
        /// Controls how the character acts while in air
        /// </summary>
        private void UpdateInAir()
        {
            //Added CalculateAirControl() to displacement so that the character stays a steady pace while in air unlike gravity which has the player fall faster and faster as the y axis declines
            _velocity.y -= gravity * Time.fixedDeltaTime; //brings character back to ground (Gravity Logic)
            Vector3 displacement = _velocity * Time.fixedDeltaTime;
            displacement += CalculateAirControl();

            characterController.Move(displacement);
            _isJumping = !characterController.isGrounded;
            _rootMotion = Vector3.zero;
            animator.SetBool("isJumping", _isJumping);
        }

        /// <summary>
        /// Calculates the initial vertical velocity required for a jump then passes that value to SetInAir()
        /// </summary>
        void Jump()
        {
            if (!_isJumping)
            {
                float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight); //Research how this equation mocks the force of gravity
                AirborneState(jumpVelocity);
            }
        }

        /// <summary>
        /// Controls how the character acts while airborne.
        /// </summary>
        /// <param name="jumpVelocity"> value used to set the vertical velocity. </param>
        private void AirborneState(float jumpVelocity)
        {
            _isJumping = true;
            _velocity = animator.velocity * jumpDamp * groundSpeed; //associates velocity with animator to keep root motion moving while jumping
            _velocity.y = jumpVelocity;
            animator.SetBool("isJumping", true);
        }

        /// <summary>
        /// scales movement input to control airborne movement
        /// </summary>
        /// <returns>modified mvoement value</returns>
        private Vector3 CalculateAirControl()
        {
            return ((transform.forward * _move.y) + (transform.right * _move.x)) * (airControl / 100);
        }

    }
}
