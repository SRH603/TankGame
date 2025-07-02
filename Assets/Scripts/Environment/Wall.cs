using UnityEngine;

/// <summary>
/// Damageable wall. OnHit is triggered by a bullet and receives the damage value.
/// </summary>
public class Wall : MonoBehaviour, IDamageable
{
    // The amount of health that this wall has.
    [SerializeField] private int health = 2;

    // Health property
    public int Health { get => health; set => health = value; }

    /// <summary>
    /// Implementation of the OnHit method inherited from IDamageable.
    /// </summary>
    /// <param name="damage">Damage caused by the caller.</param>
    /// <returns>Returns true if the wall is destroyed after receiving damage.</returns>
    public bool OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<DesWall>().onDeath?.Invoke();
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Set the wall to the "Environment" layer at start.
    /// </summary>
    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Environment");
    }
}