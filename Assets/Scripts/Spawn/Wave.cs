using UnityEngine;
using System.IO;


public class Wave 
{
    private Enemy enemy;
    private EnemyData enemyData;
    public int countEnemy;
    public Vector3 position;
    public float enemySpawnTime;
    public float DelayWave;    
    public Wave()
    {
        countEnemy = 6;
        enemySpawnTime = 3;//перенести в ScriptableObject
        DelayWave = 7;
    }

    public void CreateWave()
    {
        enemyData = Load<EnemyData>("Data/Enemy/EnemyData");
        for (int i = 0; countEnemy > i; i++)
        {
            Spawn(new Vector3(i+1,0,i+4));          
        }
    }
    public void Spawn(Vector3 point)
    {
        enemy = new Enemy(enemyData);
        enemy.EnemyField.transform.position += point;
        MainController.Instance.enemyes.Add(enemy);
    }

    private static T Load<T>(string resourcesPath) where T : Object => CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));
}
