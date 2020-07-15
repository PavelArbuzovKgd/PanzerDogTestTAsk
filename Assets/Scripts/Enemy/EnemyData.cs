using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject Prefab;//префаб    
    public float Hp;//здоровье
    public float Damage;
    public float AttackDistance;
    public int Speed; // скорость
}
