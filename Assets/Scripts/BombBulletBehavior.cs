using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Defines the behavior of a bullet.
/// The bullet bounces off walls and explodes on player or enemy tanks.
/// It deals damage by interacting with objects that implement the IDamageable interface.
/// If all bounces are used, it will also damage a wall on the final impact.
/// </summary>
public class BombBulletBehaviour : BulletBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        IDamageable hitResponse = other.gameObject.GetComponent<IDamageable>();
        if (hitResponse != null)
        {
            hitResponse.OnHit(3);
        }
        if (other.collider.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}

