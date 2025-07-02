using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : TankController, IMinimapObject
{
    [SerializeField] Camera cam;
    public float fireInterval = 0.01f;
    [SerializeField] private bool isFiring = false;

    public MinimapTypeEnum MinimapType => MinimapTypeEnum.Player;

    public Action onDestroyed
    {
        get => onDeath;
        set => onDeath = value;
    }

    private Action onDeath;

    private float fireTimer;
    public void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        Minimap.Instance.AddToObjectHashSet(this);
    }

    public void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray mouseRay = cam.ScreenPointToRay(mousePos);
        //Vector3 newOrigin = new Vector3(mouseRay.origin.x, mouseRay.origin.y, -0f);
        //Ray modifiedRay = new Ray(newOrigin, mouseRay.direction);
        //Debug.Log("外面" + mouseRay);
        
        /*
        if (Physics.Raycast(modifiedRay, out RaycastHit hitInfo, 100f))
        {
            Vector3 target = hitInfo.point;
            target.y = tankAim.transform.position.y;
            tankAim.lookTarget = target;
            //transform.LookAt(target);
        }
        */
        
        float y = transform.position.y;
        Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));

        if (plane.Raycast(mouseRay, out float enter))
        {
            Vector3 hitPoint = mouseRay.GetPoint(enter);
            hitPoint.y = tankAim.transform.position.y;
            tankAim.lookTarget = hitPoint;
        }
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 100f, Color.green, 0.05f);

        tankAim.UpdateAim();
        
        
        if (isFiring)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                fireTimer = fireInterval;
                tankAttack.Attack(tankID);
            }
        }
    }
    
    public void OnAttack(InputValue value)
    {
        isFiring = value.isPressed;
        
        if (isFiring) fireTimer = 0f;
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
