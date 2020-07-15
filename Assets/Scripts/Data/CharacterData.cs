using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character/CharacterData")]
public sealed class CharacterData : ScriptableObject
{
    public GameObject Prefab;//префаб 
    public  Weapon Weapon;
    public float Hp;//здоровье
    public float Speed; // скорость
}
