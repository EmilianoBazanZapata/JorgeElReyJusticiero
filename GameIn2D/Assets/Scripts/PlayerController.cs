using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerMana))]
[RequireComponent(typeof(PlayerInputReader), typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerDeathController))]
public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition;
    private PlayerHealth playerHealth;
    private PlayerMana playerMana;
    private PlayerInputReader inputReader;
    private PlayerMovement playerMovement;
    private PlayerDeathController playerDeathController;

    public static PlayerController sharedInstance;

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

        playerHealth = GetComponent<PlayerHealth>();
        playerMana = GetComponent<PlayerMana>();
        inputReader = GetComponent<PlayerInputReader>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDeathController = GetComponent<PlayerDeathController>();
        startPosition = transform.position;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    public void StartGame()
    {
        playerMovement.ResetAnimationState();
        transform.position = startPosition;
        playerHealth.ResetHealth();
        playerMana.ResetMana();
        playerDeathController.ResetDeathState();
    }

    private void Update()
    {
        if (GameManager.SharedInstance.CurrentGameState != GameState.InGame) return;

        inputReader.ReadInput();
        playerMovement.Tick();
    }

    private void FixedUpdate()
    {
        if (GameManager.SharedInstance.CurrentGameState != GameState.InGame) return;

        playerMovement.FixedTick();
    }

    public float GetDistance()
    {
        float traveledDistance = Mathf.Abs(transform.position.x - startPosition.x);
        return traveledDistance;
    }

    public void CollectHealt(int points)
    {
        playerHealth.Recover(points);
    }

    public void CollectMana(int points)
    {
        playerMana.Recover(points);
    }

    public float GetHealth()
    {
        return playerHealth.CurrentHealth;
    }

    public float GetMana()
    {
        return playerMana.CurrentMana;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Enemy"))
        {
            playerHealth.ReceiveDamage(20);
        }
    }
}
