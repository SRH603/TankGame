using System;
using UnityEngine;

/// <summary>
/// Base class for all tanks.
/// Tank movement should be handled by the player or enemy controller, as the tank functions like a vehicle.
/// This base class centralizes shared features between player and enemy tanks, improving code management
/// and reducing duplication.
/// All tanks must be damageable, so this class implements the IDamageable interface.
/// </summary>
public abstract class TankController : MonoBehaviour, IDamageable
{
    // To distinguish each tank.
    public int tankID; 

    // Handle tank movement
    public TankMove tankMove; 

    // Handle tank aim behavior
    public TankAim tankAim; 
    
    // Handle tank attack behavior
    public TankAttack tankAttack;

    public Action<TankController> onDead;

    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;  // Initial health
    [SerializeField] protected float speed; // Initial speed
    [SerializeField] protected float rotSpeed; // Initial rotation speed
    
    
    public int Health { get => health;
        set
        {
            health = value;
            maxHealth = health;
        }
    }
    public abstract bool OnHit(int damage);

    protected void OnDestroy()
    {
        onDead?.Invoke(this);
    }

    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }
    
    
    public void Init()
    {
        tankID = 999;
        GetComponent<TankMove>().moveSpeed = 8.2f;
    }
}
