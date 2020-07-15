using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    private PlayerController playerController;
    private CameraController cameraController;
    private EnemySpawnController enemySpawnController;
    private EnemyController enemyController;
    private TimeRemainingController timeRemainingController;
    private CharacterData characterData;
    private IOnUpdate[] controllers;
    public static MainController Instance { get; private set; }
    public ITimeService TimeService { get; private set; }
    public Transform Player { get; private set; } 
    public Transform MainCamera { get; private set; }
    public Character Character;   
    public List<Enemy> enemyes;
    public int CountEnemy;

    void Awake()
    {
        Instance = this;
        controllers = new IOnUpdate[1];
    }

    void Start()
    {
        TimeService = new UnityTimeService();
        enemyes = new List<Enemy>();
        characterData = Load<CharacterData>("Data/Character/CharacterData");
        Character = new Character(characterData);
        timeRemainingController = new TimeRemainingController();
        playerController = new PlayerController();
        cameraController = new CameraController();
        enemySpawnController = new EnemySpawnController();
        enemyController = new EnemyController();
        MainCamera = Camera.main.transform; // камера
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        controllers = new IOnUpdate[5];//можно List<>
        controllers[0] = playerController;
        controllers[1] = cameraController;
        controllers[2] = enemySpawnController;
        controllers[3] = enemyController;
        controllers[4] = timeRemainingController;        
        enemySpawnController.OnStart();
        playerController.OnStart();
        cameraController.OnStart();        
    }
   
    void Update()
    {       
        for (var index = 0; index < controllers.Length; index++)
        {
            var controller = controllers[index];
            controller?.OnUpdate();
        }
        Debug.Log(CountEnemy);
        if (CountEnemy == 0)
        {
           EndGame();
        }
    }
    public void EndGame()
    {
        controllers = new IOnUpdate[0];
        SceneManager.LoadScene(1);
    }

    private static T Load<T>(string resourcesPath) where T : Object => CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));//перенести ToDo
}
