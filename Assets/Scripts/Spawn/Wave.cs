using System.IO;
using UnityEngine;


public class Wave
{
    private Enemy enemy;
    private EnemyData enemyData;
    private bool wasEnd;
    private GameObject[] spawnPosition;
    public int CountEnemy;
    public float EnemySpawnTime;
    public float DelayWave;
    public bool WasEnd { get => wasEnd; }
    
    public Wave()
    {
        spawnPosition = GameObject.FindGameObjectsWithTag(StringManager.Respawn);
        CountEnemy = Random.Range(3,30);
        EnemySpawnTime = 3;//перенести в ScriptableObject
        DelayWave = 7;
    }

    public void CreateWave()
    {
        enemyData = Load<EnemyData>(StringManager.EnemyDataPath);
        for (int i = 0; CountEnemy > i; i++)
        {
            Spawn(spawnPosition[Random.Range(0, spawnPosition.Length)].transform.position);          
        }
    }
    public void Spawn(Vector3 point)
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

    private static T Load<T>(string resourcesPath) where T : Object => CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));
}
