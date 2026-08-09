using UnityEngine;

[RequireComponent(typeof(GameFlowController))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerDeathController playerDeathController;

    public GameObject firstBlock;
    GameObject temp;

    private GameFlowController gameFlowController;

    //variable que hace referencia al manager
    public static GameManager SharedInstance;

    //variable para indicar en que estado se encuentra el juego
    public GameState CurrentGameState => gameFlowController.CurrentState;

    //referencia al canvas del menu
    public Canvas MenuCanvas;
    //referencia al canvas del juego
    public Canvas GameCanvas;
    //referencia al canvas del gameover
    public Canvas GameOverCanvas;
    //referencia la canvas de pausa
    public Canvas PauseCanvas;
    //referencia la canvas de Opciones en el menu
    public Canvas OptionsMenuCanvas;
    //cantidad de coleccionales recogidos
    public float CollectedObjects = 0;

    private void Awake()
    {
        SharedInstance = this;
        gameFlowController = GetComponent<GameFlowController>();
    }

    private void OnEnable()
    {
        if (playerDeathController != null)
        {
            playerDeathController.Died += GameOver;
        }

        gameFlowController.StateChanging += HandleStateChanging;
    }

    private void OnDisable()
    {
        if (playerDeathController != null)
        {
            playerDeathController.Died -= GameOver;
        }

        gameFlowController.StateChanging -= HandleStateChanging;
    }

    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if (Input.GetButton("Pause"))
        {
            Pause();
        }
    }

    //metodo para iniciar el juego
    public void StartGame()
    {
        //creo el primer bloque del nivel
        transform.position = new Vector3(0, 0, 0);
        temp = Instantiate(firstBlock, transform.position, transform.rotation);
        gameFlowController.StartGame();
        //CameraFollow.SharedInstance.ResetCameraPosition();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();
        LevelGenerator.SharedInstance.RemoveAllTheBlock();
        LevelGenerator.SharedInstance.GenerateInitialBlock();
        //despues de generar el bloque genero el jugador
        PlayerController.sharedInstance.StartGame();
        this.CollectedObjects = 0f;
    }

    //metodo para reiniciar el juego
    public void RestartGame()
    {
        gameFlowController.RestartGame();
        PlayerController.sharedInstance.StartGame();
        LevelGenerator.SharedInstance.GenerateInitialBlock();
    }

    //metodo que se llamara al morir
    public void GameOver()
    {
        gameFlowController.GameOver();
        LevelGenerator.SharedInstance.RemoveAllTheBlock();
        Destroy(temp);
    }

    //metodo que llamara para volver al menu
    public void BackToMenu()
    {
        gameFlowController.BackToMenu();
    }

    //metodo que llamare para volver al juego desde las Opciones
    public void BackToGame()
    {
        gameFlowController.BackToGame();
    }

    public void Pause()
    {
        gameFlowController.PauseGame();
    }

    //metodo para continuar el juego
    public void ContinueGame()
    {
        gameFlowController.ResumeGame();
    }

    //metodo para entrar al menu del juego desde el menu de inicio
    public void OptionGameInMenu()
    {
        gameFlowController.OpenMenuOptions();
    }

    //metodo para finalizar la aplicacion
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void HandleStateChanging(GameState previousState, GameState newGameState)
    {
        if (newGameState == GameState.Menu)
        {
            //codigo para mostrar el menu
            //aqui activare el canvas
            MenuCanvas.enabled = true;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = false;
        }
        else if (newGameState == GameState.InGame)
        {
            //codigo para jugar
            MenuCanvas.enabled = false;
            GameCanvas.enabled = true;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = false;
        }
        else if (newGameState == GameState.GameOver)
        {
            //codigo para mostrar la escena de muerte
            MenuCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = true;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = false;
        }
        else if (newGameState == GameState.Pause)
        {
            //codigo para mostrar el menu de pausa
            MenuCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = true;
            OptionsMenuCanvas.enabled = false;
        }
        else if (newGameState == GameState.Options)
        {
            //codigo para mostrar el menu de pausa desde las opciones desde el menu del juego
            MenuCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameCanvas.enabled = false;
            GameOverCanvas.enabled = false;
            PauseCanvas.enabled = false;
            OptionsMenuCanvas.enabled = true;
        }
    }

    public void CollectObject(float ObjectValue)
    {
        this.CollectedObjects += ObjectValue;
    }
}
