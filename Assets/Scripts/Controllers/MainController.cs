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
    public UI UI { get; private set; }
    public ITimeService TimeService { get; private set; }
    public Transform Player { get; private set; } 
    public Transform MainCamera { get; private set; }
    public Character Character;
    public List<Enemy> Enemyes;
    public GameObject LevelGame;

    void Awake()
    {
        Instance = this;
        controllers = new IOnUpdate[1];
    }

    void Start()
    {
        UI = GetComponent<UI>();
        TimeService = new UnityTimeService();       
        timeRemainingController = new TimeRemainingController();       
        LevelGame.SetActive(false);
        UI.GameUi.SetActive(false);
        UI.Menu.SetActive(true);
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
        Enemyes = new List<Enemy>();
        characterData = Load<CharacterData>(StringManager.CharacterDataPath);
        Character = new Character(characterData);
        playerController = new PlayerController();
        cameraController = new CameraController();
        enemySpawnController = new EnemySpawnController();
        enemyController = new EnemyController();
        MainCamera = Camera.main.transform; // камера
        Player = GameObject.FindGameObjectWithTag(StringManager.TagPlayer).transform;
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
        UI.GameUi.SetActive(true);
        UI.Menu.SetActive(false);
    }

    public void EndGame(bool winner)
    {      
        for (int i=0; i< Enemyes.Count;i++)
        {
            Destroy(Enemyes[i].EnemyField);
        }
        Enemyes.Clear();
        LevelGame.SetActive(false);
        UI.GameUi.SetActive(false);
        UI.Menu.SetActive(true);
        controllers = new IOnUpdate[0];
        Destroy(Character.Gfx);
        if (winner)
        {
            UI.MenuText.text= StringManager.UiTextWin;
        }
        else UI.MenuText.text = StringManager.UiTextWLose;        
    }

    private static T Load<T>(string resourcesPath) where T : Object => CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));//перенести ToDo
}
