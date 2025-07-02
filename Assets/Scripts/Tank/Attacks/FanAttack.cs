using UnityEngine;

public class FanAttack : TankAttack
{
    public override void Attack(int sourceID)
    {
        Vector3 baseDirection = (firePoint.position - fireCenter.position).normalized;
        
        float[] angles = { 0f, -22.5f, -45f, 22.5f, 45f };

        foreach (float angle in angles)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 rotatedDirection = rotation * baseDirection;
            
            Vector3 offsetPosition = firePoint.position + rotatedDirection * 1f;
            
            GameObject bulletInstance = PoolManager.instance.GetRocket();
            bulletInstance.transform.position = offsetPosition;
            bulletInstance.transform.rotation = Quaternion.LookRotation(rotatedDirection);
            bulletInstance.SetActive(true);
            
            BulletBehaviour bullet = bulletInstance.GetComponent<BulletBehaviour>();
            bullet.Init(GetComponent<TankController>().tankID, 8, 1, rotatedDirection * firePower);
        }
    }


}
