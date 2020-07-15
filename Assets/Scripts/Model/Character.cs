using UnityEngine;


public class Character 
{
    #region Fields
    
    private GameObject gfx;//префаб
    private Weapon weapon;
    private Vector3 rightHand = new Vector3(0,0,1);//координаты правой руки (фиктивной)
    private Vector3 moveVector;   
    private float hp;
    private float speedMove;
    private CharacterController characterController;     
    private Quaternion cameraTargetRot;       
    public GameObject Gfx {get => gfx;}
    public Weapon Weapon { get => weapon;}    

    #endregion

    public Character(CharacterData characterData)
    {
        weapon = characterData.Weapon;
        gfx = characterData.Prefab;
        gfx = GameObject.Instantiate(gfx);
        weapon = GameObject.Instantiate(Weapon);
        weapon.transform.parent = gfx.transform;
        weapon.transform.position = gfx.transform.position+ rightHand;
        characterController = gfx.GetComponent<CharacterController>();
        hp = characterData.Hp;
        speedMove = characterData.Speed;
        cameraTargetRot = Gfx.transform.localRotation;
    }

    #region Method 

    public void Die()
    {
        MainController.Instance.EndGame();
        //Gfx.SetActive(false); // отключаем графику при смерти        
        GameObject.Destroy(gfx);
    }

    public void SetDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void CharecterMove(Vector2 _input)
    {        
        Vector3 desiredMove = gfx.transform.forward * _input.y + gfx.transform.right * _input.x;
        moveVector.x = desiredMove.x * speedMove;
        moveVector.z = desiredMove.z * speedMove;
        characterController.Move(moveVector * Time.deltaTime);
        gfx.transform.rotation = Quaternion.LookRotation(desiredMove);
    }

    #endregion
}
