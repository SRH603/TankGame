using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] int maxBounceCount = 3;

    private int currentBounceCount = 0;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            if (currentBounceCount >= maxBounceCount)
            {
                Destroy(gameObject);
            }
            else
            {
                currentBounceCount++;
            }
        }
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
