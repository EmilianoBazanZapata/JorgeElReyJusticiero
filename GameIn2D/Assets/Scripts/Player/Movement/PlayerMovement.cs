using UnityEngine;
using GameIn2D.Player.Input;
using GameIn2D.Player.Mana;

namespace GameIn2D.Player.Movement
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInputReader), typeof(PlayerMana))]
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float superJumpForce = 5.5f;
        [SerializeField] private float runForce = 4f;
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float initialWalkSpeed = 2f;
        [SerializeField] private Animator animatorPlayer;
        [SerializeField] private LayerMask wallLayer;
#pragma warning disable CS0414 // Retained to preserve existing serialized data pending a dedicated migration.
        [SerializeField] private LayerMask GroundLayer;
        [SerializeField] private LayerMask PlatformLayer;
#pragma warning restore CS0414

        private const float SUPER_RUN_MANA_PER_SECOND = 30f;
        private const float SUPER_JUMP_WINDOW_DURATION = 3f;
        private const int MANA_DECREASE = 5;

        private PlayerInputReader inputReader;
        private PlayerMana playerMana;
        private Rigidbody2D rigidBody;
        private BoxCollider2D boxCollider2d;
        private bool isTouchingGround;
        private int jumpsRemaining = 2;
        private float superJumpWindowRemaining;

        public bool IsGrounded => isTouchingGround;
        public bool IsMoving { get; private set; }
        public float VerticalVelocity => rigidBody.linearVelocity.y;

        private void Awake()
        {
            inputReader = GetComponent<PlayerInputReader>();
            playerMana = GetComponent<PlayerMana>();
            rigidBody = GetComponent<Rigidbody2D>();
            boxCollider2d = GetComponent<BoxCollider2D>();
        }

        public void Tick()
        {
            UpdateSuperJumpWindow();

            if (inputReader.JumpPressed)
            {
                if (isTouchingGround)
                {
                    Jump();
                }
                else if (jumpsRemaining == 1 && playerMana.HasMana)
                {
                    SuperJump();
                }
            }

            SuperRun();
        }

        public void FixedTick(float direction)
        {
            bool collidingWithWall = CollisionWall(direction);
            IsMoving = direction != 0 && !collidingWithWall;

            if (IsMoving)
            {
                float targetVelocityX = direction * walkSpeed;
                float velocityChangeX = targetVelocityX - rigidBody.linearVelocity.x;
                rigidBody.AddForce(new Vector2(velocityChangeX, 0), ForceMode2D.Impulse);
                transform.localScale = new Vector2(direction, 1);
                animatorPlayer.SetBool("Walk", true);
            }
            else
            {
                if (direction == 0f)
                {
                    rigidBody.linearVelocity = new Vector2(0f, rigidBody.linearVelocity.y);
                }

                animatorPlayer.SetBool("Walk", false);
            }
        }

        public void ResetAnimationState()
        {
            ResetJumpState();
            animatorPlayer.SetBool("IsGrounded", true);
            animatorPlayer.SetBool("Walk", false);
        }

        public void ResetJumpState()
        {
            jumpsRemaining = 2;
            superJumpWindowRemaining = 0f;
        }

        private bool CollisionWall(float direction)
        {
            Vector2 raycastDirection = new Vector2(direction, 0);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 0.5f,
                wallLayer);
            if (hit.collider != null)
                return true;

            return false;
        }

        private void SuperRun()
        {
            if (inputReader.SuperRunPressed && playerMana.HasMana)
            {
                walkSpeed = runForce;
                playerMana.Consume(SUPER_RUN_MANA_PER_SECOND * Time.deltaTime);
            }
            else
            {
                walkSpeed = initialWalkSpeed;
            }
        }

        private void Jump()
        {
            if (isTouchingGround)
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animatorPlayer.SetBool("IsGrounded", false);
                isTouchingGround = false;
                jumpsRemaining = 1;
                superJumpWindowRemaining = SUPER_JUMP_WINDOW_DURATION;
            }
        }

        private void SuperJump()
        {
            rigidBody.AddForce(Vector2.up * superJumpForce, ForceMode2D.Impulse);
            playerMana.Consume(MANA_DECREASE);
            animatorPlayer.SetBool("IsGrounded", false);
            ExpireSuperJumpWindow();
        }

        private void UpdateSuperJumpWindow()
        {
            if (jumpsRemaining != 1)
            {
                return;
            }

            superJumpWindowRemaining -= Time.deltaTime;
            if (superJumpWindowRemaining <= 0f)
            {
                ExpireSuperJumpWindow();
            }
        }

        private void ExpireSuperJumpWindow()
        {
            jumpsRemaining = 0;
            superJumpWindowRemaining = 0f;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                ContactPoint2D[] contacts = new ContactPoint2D[other.contactCount];
                other.GetContacts(contacts);
                foreach (ContactPoint2D contact in contacts)
                {
                    if (contact.normal.y > 0.5f)
                    {
                        isTouchingGround = true;
                        boxCollider2d.isTrigger = false;
                        animatorPlayer.SetBool("IsGrounded", isTouchingGround);
                        break;
                    }
                    else
                    {
                        boxCollider2d.isTrigger = true;
                        Invoke("EnableCollider", 0.5f);
                        break;
                    }
                }
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                boxCollider2d.isTrigger = false;
            }
        }

        private void EnableCollider()
        {
            boxCollider2d.isTrigger = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isTouchingGround = true;
                animatorPlayer.SetBool("IsGrounded", isTouchingGround);
                ResetJumpState();
            }

            if (other.gameObject.CompareTag("Platform"))
            {
                isTouchingGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                isTouchingGround = false;
            }

            if (collision.gameObject.CompareTag("Ground"))
            {
                isTouchingGround = false;
            }
        }
    }
}
