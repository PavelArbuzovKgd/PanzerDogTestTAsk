using UnityEngine;


public class Wave
{
    #region Fields

    private Enemy enemy;
    private EnemyData enemyData;
    private bool wasEnd;
    private GameObject[] spawnPosition;//спауны
    public int CountEnemy;//
    public float EnemySpawnTime;
    public float DelayWave;
    public bool WasEnd { get => wasEnd; }

    #endregion


    #region Method 

    public Wave()
    {
        spawnPosition = GameObject.FindGameObjectsWithTag(StringManager.Respawn);//находим спауны на сцене
        CountEnemy = Random.Range(3,30);// рандомное кол-во врагов
        EnemySpawnTime = 3;//перенести в ScriptableObject
        DelayWave = 7;//задержка
    }

    public void CreateWave()
    {
        enemyData = CustomResources.Load<EnemyData>(StringManager.EnemyDataPath);//загрузка врага
        for (int i = 0; CountEnemy > i; i++)
        {
            Spawn(spawnPosition[Random.Range(0, spawnPosition.Length)].transform.position);   //спауе врага в случайном спауне      
        }
    }

    public void Spawn(Vector3 point)//спаун врага в опр точке
    {
        enemy = new Enemy(enemyData);
        enemy.EnemyField.transform.position += point;
        MainController.Instance.Enemyes.Add(enemy);        
        enemy.EventDie += UpdateCountEnemy;
    }

    public void UpdateCountEnemy()
    {
        CountEnemy -= 1;
        if (CountEnemy == 0)
        {
            wasEnd = true;            
        }
    }

    #endregion
}
