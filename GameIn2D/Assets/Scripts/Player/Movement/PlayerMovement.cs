using UnityEngine;

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
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask PlatformLayer;

    private const float MANA_DECREASE_DELAY = 100;
    private const float MANA_DECREASE_AMOUNT = 0.5f;
    private const int MANA_DECREASE = 5;

    private PlayerInputReader inputReader;
    private PlayerMana playerMana;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider2d;
    private bool isTouchingGround;
    private bool isCollidingWall;
    private int jumpsRemaining = 2;

    public bool IsGrounded => isTouchingGround;
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
        if (inputReader.JumpPressed)
        {
            if (isTouchingGround)
            {
                Jump();
            }
            else if (jumpsRemaining > 0 && playerMana.HasMana)
            {
                SuperJump();
            }
        }

        SuperRun();
    }

    public void FixedTick(float direction)
    {
        bool collidingWithWall = CollisionWall(direction);

        if (direction != 0 && !collidingWithWall)
        {
            float targetVelocityX = direction * walkSpeed;
            float velocityChangeX = targetVelocityX - rigidBody.linearVelocity.x;
            rigidBody.AddForce(new Vector2(velocityChangeX, 0), ForceMode2D.Impulse);
            transform.localScale = new Vector2(direction, 1);
            animatorPlayer.SetBool("Walk", true);
        }
        else
        {
            animatorPlayer.SetBool("Walk", false);
        }
    }

    public void ResetAnimationState()
    {
        animatorPlayer.SetBool("IsGrounded", true);
        animatorPlayer.SetBool("Walk", false);
    }

    private bool CollisionWall(float direction)
    {
        Vector2 raycastOrigin =
            new Vector2(transform.position.x, transform.position.y - boxCollider2d.size.y / 2f);
        Vector2 raycastSize = new Vector2(0.2f, 0.5f);
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
            playerMana.Consume(MANA_DECREASE_AMOUNT);
            Invoke("DecreaseMana", MANA_DECREASE_DELAY);
        }
        else
        {
            walkSpeed = initialWalkSpeed;
            CancelInvoke("DecreaseMana");
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
        }
    }

    private void SuperJump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playerMana.Consume(MANA_DECREASE);
        animatorPlayer.SetBool("IsGrounded", false);
        jumpsRemaining = 0;
    }

    private bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.8f, GroundLayer) ||
            Physics2D.Raycast(transform.position, Vector2.down, 0.8f, PlatformLayer))
            return true;

        return false;
    }

    private void RemoveMana()
    {
        playerMana.Consume(MANA_DECREASE);
        if (!playerMana.HasMana)
        {
            CancelInvoke("RemoveMana");
        }
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
            jumpsRemaining = 2;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            isCollidingWall = true;
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            isCollidingWall = true;
            isTouchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollidingWall = false;
        }

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
