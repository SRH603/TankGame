using UnityEngine;
using UnityEngine.InputSystem;

public abstract class TankAttack : MonoBehaviour
{
    //[SerializeField] GameObject bullet;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Transform fireCenter;
    [SerializeField] protected float firePower;

    public abstract void Attack(int sourceID);

    public void SetFirePower(float power)
    {
        firePower = power;
    }

    public void SetFirePoint(Transform point)
    {
        firePoint = point;
    }
    
    public void SetFireCenter(Transform point)
    {
        fireCenter = point;
    }
    
    public float GetFirePower()
    {
        return firePower;
    }

    public Transform GetFirePoint()
    {
        return firePoint;
    }
    
    public Transform GetFireCenter()
    {
        return fireCenter;
    }
}
