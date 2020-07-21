using System.Collections.Generic;
using UnityEngine;


public class EnemySpawnController : MonoBehaviour, IOnUpdate, IOnStart
{
    #region Fields

    private TimeRemaining spawnInvoker;
    private float delayWave;  
    private int previousWaveTime=2;
    private int countWave;
    public List< Wave> wave;//Волна
    public int CountEnemy;

    #endregion


    #region Method 

    public void OnStart()
    {
        wave = new List<Wave>();
        countWave = MainController.Instance.WavesSatting.CountWave;
        for (int i = 0;i<countWave;i++)
        {
            wave.Add(new Wave());
            delayWave += wave[i].DelayWave;
            spawnInvoker = new TimeRemaining(wave[i].CreateWave, delayWave);
            spawnInvoker.AddTimeRemaining();
            CountEnemy += wave[i].CountEnemy;
        }       
    }    

    public void OnUpdate()
    {
        if (wave.Count!=0)
        {
            if (wave[wave.Count - 1].WasEnd)
            {
                MainController.Instance.EndGame(true);
            }
        }        
    }

    #endregion

}
