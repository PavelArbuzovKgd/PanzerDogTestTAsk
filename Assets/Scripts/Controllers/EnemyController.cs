﻿
public class EnemyController : IOnUpdate
{
    #region Method 

    public void OnUpdate()
    {        
        for (int i = 0;i < MainController.Instance.Enemyes.Count; i++)
        {
            MainController.Instance.Enemyes[i].MovePoint();
        }
    }

    #endregion
}