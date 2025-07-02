using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float predictTime  = 0.7f;
    public float fireInterval = 1.0f;
    public LayerMask obstacleMask;
    
    public Transform firePoint;
    public TankAim   tankAim;
    public Transform player;
    public Rigidbody playerRb;
    
    float fireTimer;
    StraightTankAttack attack;
    Vector3 lastPlayerPos;

    void Awake()
    {
        attack = GetComponent<StraightTankAttack>();
        lastPlayerPos = player.position;
    }

    void Update()
    {
        Vector3 vPlayer = playerRb
            ? playerRb.linearVelocity
            : (player.position - lastPlayerPos) / Time.deltaTime;
        lastPlayerPos = player.position;
        
        Vector3 futurePos = player.position + vPlayer * predictTime;
        if (Physics.Linecast(firePoint.position, futurePos, obstacleMask))
            return;
        futurePos.y = tankAim.transform.position.y;
        tankAim.lookTarget = futurePos;
        tankAim.UpdateAim();
        
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            attack.Attack(GetComponent<TankController>().tankID);
            fireTimer = fireInterval;
        }
    }
    
}
