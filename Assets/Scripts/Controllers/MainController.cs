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
    private UI uI;
    private IOnUpdate[] controllers;
    public static MainController Instance { get; private set; }
    public ITimeService TimeService { get; private set; }
    public Transform Player { get; private set; } 
    public Transform MainCamera { get; private set; }
    public Character Character;
    public List<Enemy> enemyes;
    public GameObject LevelGame;

    void Awake()
    {
        Instance = this;
        controllers = new IOnUpdate[1];
    }

    void Start()
    {
        uI = GetComponent<UI>();
        TimeService = new UnityTimeService();       
        timeRemainingController = new TimeRemainingController();       
        LevelGame.SetActive(false);
        uI.GameUi.SetActive(false);
        uI.Menu.SetActive(true);
    }
   
    void Update()
    {       
        for (var index = 0; index < controllers.Length; index++)
        {
            var controller = controllers[index];
            controller?.OnUpdate();
        }        
    }

    public void InitGame()
    {      
        enemyes = new List<Enemy>();
        characterData = Load<CharacterData>("Data/Character/CharacterData");
        Character = new Character(characterData);
        playerController = new PlayerController();
        cameraController = new CameraController();
        enemySpawnController = new EnemySpawnController();
        enemyController = new EnemyController();
        MainCamera = Camera.main.transform; // камера
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        LevelGame.SetActive(true);
        controllers = new IOnUpdate[5];//можно List<>
        controllers[0] = playerController;
        controllers[1] = cameraController;
        controllers[2] = enemySpawnController;
        controllers[3] = enemyController;
        controllers[4] = timeRemainingController;
        enemySpawnController.OnStart();
        playerController.OnStart();
        cameraController.OnStart();
        uI.GameUi.SetActive(true);
        uI.Menu.SetActive(false);
    }

    public void EndGame(bool winner)
    {
      
        for (int i=0; i< enemyes.Count;i++)
        {
            Destroy(enemyes[i].EnemyField);

        }
        enemyes.Clear();
        LevelGame.SetActive(false);
        uI.GameUi.SetActive(false);
        uI.Menu.SetActive(true);
        controllers = new IOnUpdate[0];
        Destroy(Character.Gfx);
        if (winner)
        {
            uI.MenuText.text= "Congratulations you are the Best";
        }
        else uI.MenuText.text = "You lose! Try again";        
    }

    private static T Load<T>(string resourcesPath) where T : Object => CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));//перенести ToDo
}
