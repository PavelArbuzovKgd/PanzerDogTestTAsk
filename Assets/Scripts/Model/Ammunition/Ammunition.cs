using UnityEngine;

public abstract class Ammunition : MonoBehaviour /// класс снаряды базовый для пуль
{
    #region Fields

    [SerializeField] protected float timeToDestruct;//время жизни
    [SerializeField] protected float minDamage; //мин урон
    [SerializeField] protected float maxDamage;//макс урон
    [SerializeField] public AmmunitionType Type ;//обращаемся к типу снаряда

    #endregion


    #region Method 

    private void Start()
    {      
        Destroy(gameObject, timeToDestruct);// уничтожаем объект  через timeToDestruct
    }

    public void AddForce(Vector3 dir) // метод придачи силы 
    {        
        if (!transform.GetComponent<Rigidbody>()) return;// проверка на РБ
        transform.GetComponent<Rigidbody>().AddForce(dir);// если есть то передаем силу     
    }
    
    #endregion

}
