using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] public float jumpForce = 5f;
    [SerializeField] public float superJumpForce = 5.5f;
    [SerializeField] public float runForce = 4.0f;
    [SerializeField] public float walkSpeed = 2f;
    [SerializeField] public float initialWalkSpeed = 2f;
    [SerializeField] public Animator animatorPlayer;
    [SerializeField] public LayerMask wallLayer;

    public BoxCollider2D boxCollider2d;
    private Rigidbody2D rigidBody;
    private UnityEngine.Vector3 startPosition;
    private float healtPoints, manaPoints;
    private bool isTouchingGround = false;
    private bool isCollidingWall = false;

    private const int INITIAL_HEALT = 90;
    private const int INITIAL_MANA = 90;
    private const int MAX_HEALTH = 100;
    private const int MAX_MANA = 100;
    private const int MANA_DECRASE = 5;
    private const float MANA_DECREASE_DELAY = 100;
    private const float MANA_DECREASE_AMOUNT = 0.5f;
    private const int MANA_DECREASE = 5;
    private int jumpsRemaining = 2;

    public static PlayerController sharedInstance;
    public LayerMask GroundLayer;
    public LayerMask PlatformLayer;

    #endregion

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void StartGame()
    {
        animatorPlayer.SetBool("IsGrounded", true);
        animatorPlayer.SetBool("Walk", false);
        this.transform.position = startPosition;
        healtPoints = INITIAL_HEALT;
        manaPoints = INITIAL_MANA;
    }

    private void Update()
    {
        if (GameManager.SharedInstance.CurrentGameState != GameState.InGame) return;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTouchingGround)
            {
                Jump();
            }
            else if (jumpsRemaining > 0 && manaPoints > 0)
            {
                SuperJump();
            }
        }

        SuperRun();
    }

    private void FixedUpdate()
    {
        if (GameManager.SharedInstance.CurrentGameState != GameState.InGame) return;

        float direction = Input.GetAxisRaw("HorizontalMove");

        // Verificar si hay una pared en la dirección en la que quieres mover al jugador
        bool isCollidingWall = CollisionWall(direction);

        if (direction != 0 && !isCollidingWall)
        {
            float targetVelocityX = direction * walkSpeed;
            float velocityChangeX = targetVelocityX - rigidBody.linearVelocity.x;
            rigidBody.AddForce(new UnityEngine.Vector2(velocityChangeX, 0), ForceMode2D.Impulse);
            transform.localScale = new UnityEngine.Vector2(direction, 1);
            animatorPlayer.SetBool("Walk", true);
        }
        else
        {
            animatorPlayer.SetBool("Walk", false);
        }
    }

    private bool CollisionWall(float direction)
    {
        // Calcular el punto de origen del raycast de caja
        UnityEngine.Vector2 raycastOrigin =
            new UnityEngine.Vector2(transform.position.x, transform.position.y - boxCollider2d.size.y / 2f);

        // Calcular el tamaño y dirección del raycast de caja
        UnityEngine.Vector2 raycastSize = new UnityEngine.Vector2(0.2f, 0.5f);
        UnityEngine.Vector2 raycastDirection = new UnityEngine.Vector2(direction, 0);

        // Realizar el raycast de caja
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new UnityEngine.Vector2(direction, 0), 0.5f,
            wallLayer);
        // Verificar si se detectó una colisión
        if (hit.collider != null)
            return true;

        return false;
    }

    private void SuperRun()
    {
        if (Input.GetButton("SuperRun") && manaPoints > 0)
        {
            walkSpeed = runForce;
            manaPoints -= MANA_DECREASE_AMOUNT;
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
            this.isTouchingGround = false;
            jumpsRemaining = 1;
        }
    }

    private void SuperJump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        manaPoints -= MANA_DECREASE;
        animatorPlayer.SetBool("IsGrounded", false);
        jumpsRemaining = 0;
    }

    bool IsTouchingTheGround()
    {
        //trazare un raycast hacia abajo para veirificar si toco el suelo hasta un maximo de 80cm
        if (Physics2D.Raycast(this.transform.position, UnityEngine.Vector2.down, 0.8f, GroundLayer) ||
            Physics2D.Raycast(this.transform.position, UnityEngine.Vector2.down, 0.8f, PlatformLayer))
            return true;

        return false;
    }

    public void Kill()
    {
        GameManager.SharedInstance.GameOver();
        float CurrentMaxDistance = PlayerPrefs.GetFloat("MaxDistance", 0);

        if (CurrentMaxDistance < this.GetDistance())
            PlayerPrefs.SetFloat("MaxDistance", this.GetDistance());

        boxCollider2d.isTrigger = false;
    }

    public float GetDistance()
    {
        float traveledDistance = Mathf.Abs(transform.position.x - startPosition.x);
        return traveledDistance;
    }

    public void CollectHealt(int points)
    {
        this.healtPoints = (this.healtPoints + points > MAX_HEALTH) ? MAX_HEALTH : this.healtPoints + points;
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public float GetHealth()
    {
        return this.healtPoints;
    }

    public float GetMana()
    {
        return this.manaPoints;
    }

    private void RemoveMana()
    {
        manaPoints -= MANA_DECRASE;
        if (manaPoints <= 0)
        {
            CancelInvoke("RemoveMana");
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Enemy"))
        {
            this.healtPoints -= 20;
            if (this.healtPoints <= 0)
            {
                Kill();
            }
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
            this.isTouchingGround = true;
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
            this.isTouchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            this.isCollidingWall = false;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            this.isTouchingGround = false;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isTouchingGround = false;
        }
    }
}