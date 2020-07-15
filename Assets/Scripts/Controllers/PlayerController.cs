using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController: IOnUpdate, IOnStart
{
    private Camera cam;
    private Vector2 input;
    private NavMeshAgent agent;
    private Character character;

    public void OnUpdate()
    {
        if (character != null)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                character.Weapon.ReloadClip();
            }     
            if (Input.GetMouseButtonDown(0))
            {
                character.Weapon.Fire();                         
            }
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 300f))
                {
                    agent.isStopped = false;
                    agent.SetDestination(hit.point);
                }
            }
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                agent.isStopped = true;
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");
                input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                character.Gfx.transform.Rotate(Vector3.up * x * 100 * Time.deltaTime);
                character.Gfx.transform.Translate(Vector3.forward * y * 5 * Time.deltaTime);                
            }
        }
    }

    public void OnStart()
    {
        character = MainController.Instance.Character;
        cam = Camera.main; 
        agent = character.Gfx.GetComponent<NavMeshAgent>();
    }
}
