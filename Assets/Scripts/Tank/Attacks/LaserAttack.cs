using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : TankAttack
{
    [SerializeField] public LineRenderer lineRendererPrefab;
    [SerializeField] float laserDuration = 0.35f;
    [SerializeField] int maxReflections = 8;
    [SerializeField] float maxDistance = 999f;

    public override void Attack(int sourceID)
    {
        Vector3 origin = firePoint.position;
        Vector3 direction = (firePoint.position - fireCenter.position).normalized;

        LineRenderer lr = Instantiate(lineRendererPrefab);
        List<Vector3> points = new List<Vector3> { origin };

        int reflections = 0;
        Vector3 currentPosition = origin;
        Vector3 currentDirection = direction;

        while (reflections <= maxReflections)
        {
            if (Physics.Raycast(currentPosition, currentDirection, out RaycastHit hit, maxDistance))
            {
                points.Add(hit.point);

                if (hit.collider.CompareTag("Wall"))
                {
                    currentPosition = hit.point;
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    reflections++;
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    //Debug.Log("999");
                    IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
                    //Debug.Log(hit.collider.gameObject);
                    damageable.OnHit(7);

                    currentPosition = hit.point + currentDirection * 0.1f;
                    reflections++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                points.Add(currentPosition + currentDirection * maxDistance);
                break;
            }
        }

        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());

        StartCoroutine(DestroyLaserAfterTime(lr, laserDuration));
    }


    private IEnumerator DestroyLaserAfterTime(LineRenderer lr, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(lr.gameObject);
    }
}
