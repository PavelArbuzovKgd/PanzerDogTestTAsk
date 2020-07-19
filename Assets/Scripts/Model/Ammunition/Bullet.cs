using UnityEngine;

public sealed class Bullet : Ammunition
{
    public Bullet()
    {
        minDamage = 48;//TODO/настройки перенести scriptObj
        maxDamage = 100;
    }
    private void OnCollisionEnter(Collision collision)
    {       
        for (int i = 0; i < MainController.Instance.Enemyes.Count; i++)//перебор
        {
            if (MainController.Instance.Enemyes[i].EnemyField.transform == collision.transform)//если совпадает - наносим урон при условие нахождения компонента ISetDamage
            {
                MainController.Instance.Enemyes[i].SetDamage(Random.Range(minDamage,maxDamage));
                Destroy(gameObject);// удаляем пулю    
            }
        }
    }
}