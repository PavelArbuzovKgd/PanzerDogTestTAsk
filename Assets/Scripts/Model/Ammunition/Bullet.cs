using UnityEngine;

public sealed class Bullet : Ammunition
{ 
    #region Method    

    private void OnCollisionEnter(Collision collision)
    {       
        for (int i = 0; i < MainController.Instance.Enemyes.Count; i++)//перебор врагов
        {
            if (MainController.Instance.Enemyes[i].EnemyField.transform == collision.transform)//если совпадает - наносим урон при условие нахождения компонента ISetDamage
            {
                MainController.Instance.Enemyes[i].SetDamage(Random.Range(minDamage,maxDamage));//рондомный урон в заданых пределах
                Destroy(gameObject);// удаляем пулю    
            }
        }
    }

    #endregion

}