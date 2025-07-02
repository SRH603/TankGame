using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyController : TankController, IMinimapObject
{
    NavMeshPath navPath;
    public LayerMask obstacleMask;
    public Transform player;
    public Rigidbody playerRb;
    
    public Transform firePoint;
    public float predictTime  = 0.7f;
    public float fireInterval = 1.0f;
    float fireTimer;
    Vector3 lastPlayerPos;
    
    public MinimapTypeEnum MinimapType => MinimapTypeEnum.Enemy;

    public Action onDestroyed
    {
        get => onDeath;
        set => onDeath = value;
    }

    private Action onDeath;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navPath = new NavMeshPath();
        player = GameObject.Find("PlayerTank").transform;
        playerRb = GameObject.Find("PlayerTank").GetComponent<Rigidbody>();
        Minimap.Instance.AddToObjectHashSet(this);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > 10 || Physics.Linecast(transform.position, player.position, obstacleMask) || GetComponent<ArcAttack>())
        {
            
            if (NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, navPath))
            {
                float sign = Vector3.Dot(transform.right, navPath.corners[1] - transform.position);

                if (sign < -0.01f)
                {
                    tankMove.rotVal = -1f;
                }
                else if (sign > 0.01f)
                {
                    tankMove.rotVal = 1f;
                }
                else
                {
                    tankMove.rotVal = 0f;
                }
                tankMove.moveVal = Mathf.Abs(sign) < 0.75f ? 1f : 0f;
            }
        }
        else
        {
            tankMove.moveVal = 0f;
            tankMove.rotVal = 0f;
            //Debug.Log("111");
        }
        //UpdateAim();
        AutoShoot();
    }

    private void AutoShoot()
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
            tankAttack.Attack(GetComponent<TankController>().tankID);
            fireTimer = fireInterval;
        }
    }

    void UpdateAim()
    {
        Vector3 hitPoint = player.position;
        hitPoint.y = tankAim.transform.position.y;
        tankAim.lookTarget = hitPoint;

        tankAim.UpdateAim();
    }
    public void OnAttack()
    {
        tankAttack.Attack(tankID);
    }

    public void OnTankRotate(InputValue value)
    {
        tankMove.rotVal = value.Get<float>();
    }

    public void OnTankMove(InputValue value)
    {
        tankMove.moveVal = value.Get<float>();
    }
    
    // Health property
    //public int Health { get => health; set => health = value; }

    /// <summary>
    /// Implementation of the OnHit method inherited from IDamageable.
    /// </summary>
    /// <param name="damage">Damage caused by the caller.</param>
    /// <returns>Returns true if the wall is destroyed after receiving damage.</returns>
    public override bool OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            onDeath?.Invoke();
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
