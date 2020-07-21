using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullets/BulletData")]
public class BulletData : ScriptableObject
{
    public float TimeToDestruct ;//время жизни
    public float MinDamage; //мин урон
    public float MaxDamage;//макс урон
    public AmmunitionType Type ;//тип снаряда   
}
