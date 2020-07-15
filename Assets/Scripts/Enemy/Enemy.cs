using UnityEngine;
using UnityEngine.AI;

public sealed class Enemy :  ISetDamage
{
    private GameObject enemy;//префаб
    private float hp;
    private float damage;
    private int speed;
    private NavMeshAgent agent { get; }
    private EnemyData enemyData;
    private float attackDistance;
    private Animator animator;
    private RaycastHit raycast;
    public Transform Target { get; }
    public GameObject EnemyField { get => enemy; set => enemy = value; }

    public Enemy(EnemyData EnemyData)
    {
        enemyData = EnemyData;
        Target = MainController.Instance.Player;
        attackDistance = enemyData.AttackDistance;
        enemy = enemyData.Prefab;
        hp = enemyData.Hp;
        damage = enemyData.Damage;
        speed = enemyData.Speed;
        enemy = GameObject.Instantiate(enemy);
        agent = enemy.GetComponent<NavMeshAgent>();
        animator = enemy.GetComponent<Animator>();         
    }

    public void SetDamage(float damage)
    {
        if (hp > 0)
        {
            hp = hp - damage;
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    private void Die()
    {
        animator.SetBool("Die", true);       
        GameObject.Destroy(enemy.gameObject, 1);
        MainController.Instance.CountEnemy -= 1;
        MainController.Instance.enemyes.Remove(this);
    }

    public void MovePoint()
    {
        if (Target != null)
        {
            animator.SetBool("Stay", false);
            float distance = Vector3.Distance(enemy.transform.position, Target.position);
            agent.SetDestination(Target.position);
            if (distance < attackDistance)
            {
                animator.SetBool("Attack", true);
                Attack();
            }
            else animator.SetBool("Attack", false);
        }
        else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Stay", true);
        } 

    }

    public void Attack()
    {
        if (Physics.Raycast(enemy.transform.position, -Vector3.forward, out raycast, 100))
        {
            if (raycast.transform == MainController.Instance.Player)
            {

                MainController.Instance.Character.SetDamage(damage);
            }
        }
    }
}
