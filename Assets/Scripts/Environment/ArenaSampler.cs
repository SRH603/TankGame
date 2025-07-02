using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class ArenaSampler : MonoBehaviour
{
    [SerializeField] string navAreaName = "Walkable";
    [SerializeField] float retryCnt = 10;
    [SerializeField] float minDisranceDiff = 1f;
    [SerializeField] LayerMask layerToAvoid;

    [SerializeField] List<Transform> spawnPointList;
    
    Collider arena;

    void Awake()
    {
        arena = GetComponent<Collider>();
    }

    public Vector3 GetNavMeshPointFromList()
    {
        int randomIndex = Random.Range(0, spawnPointList.Count);
        return spawnPointList[randomIndex].position;
    }

    public Vector3 GetNavMeshPointInBound()
    {
        
        //Vector3 ret = new Vector3();
        int areaMask = 1 << NavMesh.GetAreaFromName(navAreaName);
        for (int i = 0; i <= retryCnt; i++)
        {
            if (NavMesh.SamplePosition(GetRandomPointInBound(), out NavMeshHit hitInfo, 10f, areaMask))
            {
                if (Physics.OverlapSphere(hitInfo.position, minDisranceDiff, layerToAvoid).Length == 0)
                {
                    return hitInfo.position;
                }
            }
        }
        

        Debug.LogError("Can't find a valid position");
        return Vector3.zero;
    }

    public Vector3 GetRandomPointInBound()
    {
        Bounds bounds = arena.bounds;
        Vector3 ret = new Vector3();
        ret.x = Random.Range(bounds.min.x, bounds.max.x);
        ret.y = Random.Range(bounds.min.y, bounds.max.y);
        ret.z = Random.Range(bounds.min.z, bounds.max.z);
        return ret;
    }
}
