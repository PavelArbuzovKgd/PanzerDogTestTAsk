using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class Enemy : ISetDamage
{
    private GameObject enemy;//префаб 
    private List<Transform> arsenalAttack;//Оружие - Руки
    private float hp;
    private float damage;
    private float speedAttack;
    private float timer;
    private NavMeshAgent agent { get; }
    private EnemyData enemyData;//настройки
    private float attackDistance;
    private Animator animator;
    public Transform Target { get; }
    public GameObject EnemyField { get => enemy; set => enemy = value; }
    public delegate void DieDelegate();
    public event DieDelegate EventDie;
    public bool IsAttack;

    public Enemy(EnemyData EnemyData)
    {
        enemyData = EnemyData;
        Target = MainController.Instance.Player;
        attackDistance = enemyData.AttackDistance;
        enemy = enemyData.Prefab;
        hp = enemyData.Hp;
        damage = enemyData.Damage;
        speedAttack = enemyData.SpeedAttack;
        enemy = GameObject.Instantiate(enemy);
        agent = enemy.GetComponent<NavMeshAgent>();
        animator = enemy.GetComponent<Animator>();        
        arsenalAttack = new List<Transform>();
        arsenalAttack.Add(enemy.transform.Find("Hand"));
        arsenalAttack.Add(enemy.transform.Find("Hand1"));
    }

    public void SetDamage(float damage)
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

    private void Die(float dieTime)
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
                timer = 0;
                IsAttack = false;
            }
        }
        else
        {
            animator.SetBool(StringManager.Attack, false);
            animator.SetBool(StringManager.Stay, true);
            timer = 0;
            IsAttack = false;
        }
    }

    public void Attack()
    {
        for (int a = 0; a < arsenalAttack.Count; a++)
        {
            var colliders = Physics.OverlapSphere(arsenalAttack[a].position, 3);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == MainController.Instance.Player.position)
                {
                    if (!IsAttack)
                    {
                        GiveDamage(MainController.Instance.Character);
                        IsAttack = true;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                        if (timer > speedAttack)
                        {
                            timer = 0;
                            IsAttack = false;
                        }
                    }
                }
            }
        }
    }

    public void GiveDamage (ISetDamage Object)
    {
        Object.SetDamage(damage);
    }
}
