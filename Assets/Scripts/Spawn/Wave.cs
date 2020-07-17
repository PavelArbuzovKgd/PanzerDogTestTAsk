using UnityEngine;
using System.IO;


public class Wave 
{
    private Enemy enemy;
    private EnemyData enemyData;
    private bool wasEnd;
    public int CountEnemy;
    public Vector3 Position;
    public float EnemySpawnTime;
    public float DelayWave;
    public bool WasEnd { get => wasEnd; }

    
    public Wave()
    {
        CountEnemy = 1;
        EnemySpawnTime = 3;//перенести в ScriptableObject
        DelayWave = 7;
    }

    public void CreateWave()
    {
        enemyData = Load<EnemyData>("Data/Enemy/EnemyData");
        for (int i = 0; CountEnemy > i; i++)
        {
            Spawn(new Vector3(i+1,0,i+4));          
        }
    }
    public void Spawn(Vector3 point)
    {
        enemy = new Enemy(enemyData);
        enemy.EnemyField.transform.position += point;
        MainController.Instance.enemyes.Add(enemy);        
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
