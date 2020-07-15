using UnityEngine;

public class EnemyController : MonoBehaviour, IOnUpdate
{
    public void OnUpdate()
    {
        var enemyes = MainController.Instance.enemyes;
        for (int i = 0;i < enemyes.Count; i++)
        {
            enemyes[i].MovePoint();
        }             
    }   
}
