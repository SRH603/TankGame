using UnityEngine;

public class ArcAttack : TankAttack
{
    public override void Attack(int sourceID)
    {
        Vector3 flatDirection = (firePoint.position - fireCenter.position);
        flatDirection.y = 0;
        flatDirection = flatDirection.normalized;

        float angleInDegrees = -30f;
        Vector3 launchDirection = Quaternion.AngleAxis(-angleInDegrees, Vector3.Cross(flatDirection, Vector3.up)) * flatDirection;
        
        GameObject bulletInstance = PoolManager.instance.GetArcRocket();
        bulletInstance.transform.position = firePoint.position + launchDirection * 0.2f;
        bulletInstance.transform.rotation = Quaternion.LookRotation(launchDirection);
        bulletInstance.SetActive(true);

        BulletBehaviour bullet = bulletInstance.GetComponent<BulletBehaviour>();
        bullet.Init(GetComponent<TankController>().tankID, 8, 1, launchDirection * firePower);

    }
}
