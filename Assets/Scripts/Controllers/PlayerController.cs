using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController: IOnUpdate, IOnStart
{
    #region Fields

    private Camera cam;
    private Vector2 input;
    private NavMeshAgent agent;
    private Character character;

    #endregion


    #region Method 

    public void OnUpdate()
    {
        if (character != null)
        {
            if (Input.GetKeyUp(KeyCode.Q))//перезарятка
            {
                character.Weapon.ReloadClip();
            }     
            if (Input.GetMouseButtonDown(0))//атака
            {
                character.Weapon.Fire();                         
            }
            if (Input.GetMouseButtonDown(1))//перемешение мышкой
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 300f))
                {
                    agent.isStopped = false;
                    agent.SetDestination(hit.point);
                }
            }
            if (Input.GetAxis(StringManager.InputHorizontal) != 0 || Input.GetAxis(StringManager.InputVertical) != 0)//перемещение клавишами
            {
                agent.isStopped = true;
                float x = Input.GetAxis(StringManager.InputHorizontal);
                float y = Input.GetAxis(StringManager.InputVertical);
                input = new Vector3(Input.GetAxis(StringManager.InputHorizontal), Input.GetAxis(StringManager.InputVertical));
                character.Gfx.transform.Rotate(Vector3.up * x * 100 * Time.deltaTime);
                character.Gfx.transform.Translate(Vector3.forward * y * 5 * Time.deltaTime);                
            }
        }
        MainController.Instance.UI.ShowCount();//выводем хп 
    }

    public void OnStart()
    {
        character = MainController.Instance.Character;
        cam = Camera.main;
        agent = character.Gfx.GetComponent<NavMeshAgent>();
    }

    #endregion
}
