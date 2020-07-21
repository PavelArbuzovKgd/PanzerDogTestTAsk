
public sealed class AutomaticGun : Weapon
{
    public Ammunition Ammunition;

    public override void Fire() ///метод аттаки
    {
        TryFire(Ammunition);//метод на возможмость аттаки
    }
}
