using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSatting 
{
    private int countWave;
    private int minCountEnemy;
    private int maxCountEnemy;
    private int delayWave;

    public int CountWave { get => countWave; }
    public int MinCountEnemy { get => minCountEnemy;}
    public int MaxCountEnemy { get => maxCountEnemy;}
    public int DelayWave { get => delayWave;}

    public void SetSettings(int CountWave,int MinCountEnemy,int MaxCountEnemy, int DelayWave )
    {
        countWave = CountWave;
        minCountEnemy = MinCountEnemy;
        maxCountEnemy = MaxCountEnemy;
        delayWave = DelayWave;
    }
}
