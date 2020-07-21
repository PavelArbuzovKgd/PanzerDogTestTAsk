using UnityEngine;


public class Wave
{
    #region Fields

    private Enemy enemy;
    private EnemyData enemyData;
    private bool wasEnd;
    private GameObject[] spawnPosition;//спауны
    public int CountEnemy;
    public int MinCountEnemy;
    public int MaxCountEnemy;
    public float DelayWave;
    public bool WasEnd { get => wasEnd; }

    #endregion


    #region Method 

    public Wave()
    {
        MinCountEnemy = MainController.Instance.WavesSatting.MinCountEnemy;
        MaxCountEnemy = MainController.Instance.WavesSatting.MaxCountEnemy;
        spawnPosition = GameObject.FindGameObjectsWithTag(StringManager.Respawn);//находим спауны на сцене
        CountEnemy = Random.Range(MinCountEnemy, MaxCountEnemy);// рандомное кол-во врагов       
        DelayWave = MainController.Instance.WavesSatting.DelayWave; ;//задержка
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
        enemy.EnemyField.transform.position = point;
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
