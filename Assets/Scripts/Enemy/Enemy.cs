using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class Enemy : ISetDamage
{

    #region Fields

    private GameObject enemy;//префаб 
    private List<Transform> arsenalAttack;//Оружие - Руки
    private float hp;
    private float damage;
    private float speedAttack;
    private float couldDawn;
    private EnemyData enemyData;//настройки
    private float attackDistance;
    private Animator animator;
    public bool IsAttack;

    #endregion


    #region Properties

    private NavMeshAgent agent { get; }
    public Transform Target { get; }
    public GameObject EnemyField { get => enemy; set => enemy = value; }
    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    #endregion


    #region ClassLifeCycle

    public Enemy(EnemyData EnemyData)
    {
        enemyData = EnemyData;
        Target = MainController.Instance.Player;//Цель
        attackDistance = enemyData.AttackDistance;//дистанция атаки
        enemy = enemyData.Prefab;//префаб
        hp = enemyData.Hp;
        damage = enemyData.Damage;
        speedAttack = enemyData.SpeedAttack;
        enemy = GameObject.Instantiate(enemy);
        agent = enemy.GetComponent<NavMeshAgent>();
        animator = enemy.GetComponent<Animator>();        
        arsenalAttack = new List<Transform>();//способы атаки (чем атакует)
        arsenalAttack.Add(enemy.transform.Find("Hand"));
        arsenalAttack.Add(enemy.transform.Find("Hand1"));
    }

    #endregion


    #region Method 

    public void SetDamage(float damage)//получение урона
    {
        if (hp > 0)
        {
            hp = hp - damage;
            if (hp <= 0)
            {
                hp = 0;
                animator.SetBool(StringManager.Die, true);
                Die(2);
            }
        }
    }

    private void Die(float dieTime)//смерть
    {
        MainController.Instance.Enemyes.Remove(this);
        GameObject.Destroy(enemy.gameObject, dieTime);
        enemy.GetComponentInChildren<Collider>().enabled = false;
        EventDie?.Invoke();
    }

    public void MovePoint()
    {      
        if (Target != null)
        {
            animator.SetBool(StringManager.Stay, false);
            float distance = Vector3.Distance(enemy.transform.position, Target.position);
            agent.SetDestination(Target.position);
            if (distance < attackDistance)
            {
                animator.SetBool(StringManager.Attack, true);
                Attack();
            }
            else
            {
                animator.SetBool(StringManager.Attack, false);
                couldDawn = 0;
                IsAttack = false;
            }
        }
        else
        {
            animator.SetBool(StringManager.Attack, false);
            animator.SetBool(StringManager.Stay, true);
            couldDawn = 0;
            IsAttack = false;
        }
    }

    public void Attack()
    {
        for (int a = 0; a < arsenalAttack.Count; a++)//перебираем способы атаки
        {
            var colliders = Physics.OverlapSphere(arsenalAttack[a].position, 3);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == MainController.Instance.Player.position)//если игрок попал в зону OverlapSphere
                {
                    if (!IsAttack)//если не атакует
                    {
                        GiveDamage(MainController.Instance.Character);//наносим урон
                        IsAttack = true;
                    }
                    else//если в атаке
                    {
                        couldDawn += Time.deltaTime;
                        if (couldDawn > speedAttack)
                        {
                            couldDawn = 0;
                            IsAttack = false;
                        }
                    }
                }
            }
        }
    }

    public void GiveDamage (ISetDamage Object)
    {
        Object.SetDamage(damage);//наносим урон
    }

    #endregion
}
