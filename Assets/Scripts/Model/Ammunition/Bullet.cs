using UnityEngine;

public sealed class Bullet : Ammunition
{
    private void OnCollisionEnter(Collision collision)
    {       
        for (int i = 0; i < MainController.Instance.enemyes.Count; i++)//перебор
        {
            if (MainController.Instance.enemyes[i].EnemyField.transform == collision.transform)//если совпадает - наносим урон при условие нахождения компонента ISetDamage
            {
                MainController.Instance.enemyes[i].SetDamage(baseDamage);
                Destroy(gameObject);// удаляем пулю    
            }
        }
    }
}