
public sealed class AutomaticGun : Weapon
{
    public Ammunition Ammunition;

    public override void Fire() 
    {
        TryFire(Ammunition);
    }
}
