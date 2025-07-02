using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Defines the behavior of a bullet.
/// The bullet bounces off walls and explodes on player or enemy tanks.
/// It deals damage by interacting with objects that implement the IDamageable interface.
/// If all bounces are used, it will also damage a wall on the final impact.
/// </summary>
public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public int sourceID;
    public bool hasBounced = false;
    public float speed;
    public int bouncesRemaining = 1;
    public int damage = 1;
    public GameObject vfx, vfxBomb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(int inputSourceID, int inputBouncesRemaining, int inputDamage, Vector3 velocity)
    {
        sourceID = inputSourceID;
        bouncesRemaining = inputBouncesRemaining;
        damage = inputDamage;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(velocity, ForceMode.Impulse);
    }

    /// <summary>
    /// Detect if the object hits something on the Environment (Wall), Player, or Enemy layers. 
    /// Perform OnHit detection, and if it hits a wall, also perform a bounce test.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        IDamageable hitResponse = other.gameObject.GetComponent<IDamageable>();
        if (hitResponse != null)
        {
            hitResponse.OnHit(1);
        }
        if (other.collider.CompareTag("Wall"))
        {
            if (bouncesRemaining <= 0)
            {
                gameObject.SetActive(false);
                Instantiate(vfxBomb, transform.position, transform.rotation);
            }
            else
            {
                bouncesRemaining--;
                Instantiate(vfx, transform.position, transform.rotation);
            }
        }
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Instantiate(vfxBomb, transform.position, transform.rotation);
        }
    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        if (BulletBlackboard.Instance)
            BulletBlackboard.Instance.Register(rb);
    }
    
    void OnDisable()
    {
        if (BulletBlackboard.Instance)
            BulletBlackboard.Instance.Unregister(rb);
    }
}

