using UnityEngine;
using UnityEngine.InputSystem;

public class StraightTankAttack : TankAttack
{
    public override void Attack(int sourceID)
    {
        Vector3 direction = (firePoint.position - fireCenter.position).normalized;
            
        //GameObject bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));
        //ebug.Log(direction * firePower);
        //bulletInstance.GetComponent<Rigidbody>().AddForce(direction * firePower, ForceMode.Impulse);
            
        GameObject bulletInstance = PoolManager.instance.GetRocket();
        bulletInstance.transform.position = firePoint.position;
        bulletInstance.transform.rotation = Quaternion.LookRotation(direction);
        bulletInstance.SetActive(true);
        
        BulletBehaviour bullet = bulletInstance.GetComponent<BulletBehaviour>();
        bullet.Init(GetComponent<TankController>().tankID,8,1, direction * firePower);

    }
}
