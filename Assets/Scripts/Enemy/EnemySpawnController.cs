using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawnController : MonoBehaviour, IOnUpdate, IOnStart
{
    public List< Wave> wave;
    private TimeRemaining _spawnInvoker;
    private float delayWave;  
    private int previousWaveTime=2;
    private int countWave= 3;
    public int CountEnemy;

    public void OnStart()
    {
        wave = new List<Wave>();
        for (int i = 0;i<countWave;i++)
        {
            wave.Add(new Wave());
            delayWave += wave[i].DelayWave;
            _spawnInvoker = new TimeRemaining(wave[i].CreateWave, delayWave);
            _spawnInvoker.AddTimeRemaining();
            CountEnemy += wave[i].CountEnemy;
        }
       
    }    

    public void OnUpdate()
    {
        if (wave[wave.Count - 1].WasEnd)
        {
            MainController.Instance.EndGame(true);
        }
    }    
}
