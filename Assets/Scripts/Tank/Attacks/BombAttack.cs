using UnityEngine;

public class BombAttack : TankAttack
{
    public override void Attack(int sourceID)
    {
        Vector3 direction = (firePoint.position - fireCenter.position).normalized;
            
        GameObject bulletInstance = PoolManager.instance.GetBombRocket();
        bulletInstance.transform.position = firePoint.position;
        bulletInstance.transform.rotation = Quaternion.LookRotation(direction);
        bulletInstance.SetActive(true);
        
        BulletBehaviour bullet = bulletInstance.GetComponent<BulletBehaviour>();
        bullet.Init(GetComponent<TankController>().tankID,8,1, direction * firePower);

    }
}
