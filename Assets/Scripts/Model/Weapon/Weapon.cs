﻿using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour // базовый класс дЛя всех оружий
{

    #region Fields

    [SerializeField] protected Transform _barrel;///откуда вылетает пуля
    [SerializeField] protected int _maxCountAmmunition = 40;/// максимально количество пуль
    [SerializeField] protected float _force = 999;// сила вылета
    [SerializeField] protected float _rechergeTime = 0.2f;// время между выстрелом
    protected AmmunitionType[] _ammunitionType = { AmmunitionType.Bullet };//  определяем какой пулей(тип) стреляет оружие
    protected bool isReady = true; // котовность к стрельбе
    private int minCountAmmunition = 20;//минимально количество пуль///TODO/настройки перенести scriptObj
    private int countClip = 25;///количество обоим
    private Queue<Clip> clips = new Queue<Clip>();///очередь обоим
    public Clip Clip;//  обоима 

    #endregion


    #region Method

    protected void Start()
    {
        for (var i = 0; i <= countClip; i++)
        {
            AddClip(new Clip { CountAmmunition = _maxCountAmmunition });
        }
        ReloadClip();
    }
    
    public abstract void Fire();

    public void TryFire(Ammunition Ammunition) 
    {
        if (!isReady) return;//не готов к стрельбе
        if (Clip.CountAmmunition <= 0) return; //если нет обоимы
        if (!Ammunition) return;    //если не чем стрелять                              
        var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);// создаем Amunition в спаун позиции 
        temAmmunition.AddForce(_barrel.forward * _force);//придаем исилу вылета
        Clip.CountAmmunition--;// отнимаем пулю 
        isReady = false;// выстрел произведен
        Invoke(nameof(ReadyShoot), _rechergeTime);// вызываем готовность к стрельбе через опр время                                                  
    }

    protected void ReadyShoot()//метод готовности к стрельбе
    {
        isReady = true;
    }

    protected void AddClip(Clip clip)// метод добавления обоимы в оружие
    {
        clips.Enqueue(clip);
    }

    public void ReloadClip()// метед перезарядке обоимы
    {        
        if (Clip.CountAmmunition == _maxCountAmmunition) return;//если обоима полная
        if (CountClip <= 0) return;// не перзаряжаемся
        Clip = clips.Dequeue(); // в противно случае перезарежаемся
    }

    public int CountClip => clips.Count;//TODO реализовать  вывод количество в интерфейс

    #endregion
}
