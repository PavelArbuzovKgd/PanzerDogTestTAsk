using UnityEngine;


public class Character : ISetDamage
{
    #region Fields
    
    private GameObject gfx;//префаб
    private Weapon weapon;//оружие
    private Vector3 rightHand = new Vector3(0,0,1);//координаты правой руки (фиктивной)
    private Vector3 moveVector;   
    private readonly float baseHp;//начальное здоровье
    private float currentHp;//текущее
    private float speedMove;//скорость
    private CharacterController characterController;//     characterController
                                                 
    #endregion


    #region Properties

    public GameObject Gfx {get => gfx;}
    public Weapon Weapon { get => weapon;}
    public float Hp { get => currentHp;}

    public delegate void HpDelegate();
    public event HpDelegate EventHp;

    #endregion


    #region ClassLifeCycle

    public Character(CharacterData characterData)
    {
        weapon = characterData.Weapon;
        gfx = characterData.Prefab;
        gfx = GameObject.Instantiate(gfx);
        weapon = GameObject.Instantiate(Weapon);
        weapon.transform.parent = gfx.transform;
        weapon.transform.position = gfx.transform.position+ rightHand;
        characterController = gfx.GetComponent<CharacterController>();
        baseHp = characterData.Hp;
        speedMove = characterData.Speed;
        currentHp = baseHp;
    }

    #endregion


    #region Method 

    public void Die()
    {
        MainController.Instance.EndGame(false);           
        GameObject.Destroy(gfx);
    }

    public void SetDamage(float damage)
    {
        currentHp -= damage;      

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();           
        }
        EventHp?.Invoke();
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
