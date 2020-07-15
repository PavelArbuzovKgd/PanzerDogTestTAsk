using UnityEngine;

public abstract class Ammunition : MonoBehaviour /// класс снаряды базовый для пуль//TODO/настройки перенести scriptObj
{
    [SerializeField] protected float timeToDestruct = 10;//время жизни
    [SerializeField] protected float baseDamage = 10; // базовый урон    
    public AmmunitionType Type = AmmunitionType.Bullet;//обращаемся к типу снаряда
        
    private void Start()
    {      
        Destroy(gameObject, timeToDestruct);// уничтожаем объект       
    }

    public void AddForce(Vector3 dir) // метод придачи силы 
    {        
        if (!transform.GetComponent<Rigidbody>()) return;// проверка на РБ
        transform.GetComponent<Rigidbody>().AddForce(dir);// если есть то передаем силу     
    } 
}
