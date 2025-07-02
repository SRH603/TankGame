/// <summary>
/// Interface for objects that are damageable. This provides a common way to apply damage by calling OnHit. 
/// The return value tells the attacker if this object is detroyed or not.
/// </summary>
public interface IDamageable
{
    public int Health { get; set; }
    public bool OnHit(int damage);
}