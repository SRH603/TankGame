using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages object pooling by instantiating all necessary pooled objects and supplying them to requesting components.
/// This manager does not need to persist across scenes.
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    // Check if this object is the only instance in the scene. (Singleton)
    private void Awake()
    {
        // if the current instance hasn't been assigned, that means this object is the first instance of this class.
        if (instance == null)
        {
            // Set the instance to this object.
            instance = this;
        }
        else
        {
            // if the instance already exist and it's not this object
            if (instance != this)
            {
                // destroy this one
                Destroy(gameObject);
            }
        }
    }

    // Projectiles to create and the pools to hold each type of projectile
    [SerializeField] GameObject rocketProjectile;
    [SerializeField] public GameObject bombRocketProjectile;
    [SerializeField] public GameObject arcRocketProjectile;
    [SerializeField] public GameObject laserRocketProjectile;
    [SerializeField] int rocketPoolSize = 200;
    private List<GameObject> rocketPool = new List<GameObject>();
    private List<GameObject> arcRocketPool = new List<GameObject>();
    private List<GameObject> bombRocketPool = new List<GameObject>();
    

    /// <summary>
    /// Initial the pool.
    /// </summary>
    void Start()
    {
        CreateObjectPool(rocketProjectile, rocketPool, rocketPoolSize);
        CreateObjectPool(arcRocketProjectile, arcRocketPool, rocketPoolSize);
        CreateObjectPool(bombRocketProjectile, bombRocketPool, rocketPoolSize);
    }

    /// <summary>
    /// Creates the object pool by instantiating objects and placing them into the container.
    /// </summary>
    /// <param name="obj">The object to be spawned.</param>
    /// <param name="pool">The container for the pooled objects.</param>
    /// <param name="count">The number of objects to include in the pool.</param>
    public void CreateObjectPool(GameObject obj, List<GameObject> pool, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject objInstance = Instantiate(obj);
            obj.SetActive(false);
            pool.Add(objInstance);
        }
    }

    /// <summary>
    /// Get a bullet from the pool if one is available.
    /// </summary>
    /// <returns>Return a bullet if one is available; otherwise, return null.</returns>
    public GameObject GetRocket()
    {
        foreach (GameObject obj in rocketPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject extra = Instantiate(rocketProjectile);
        extra.SetActive(false);
        rocketPool.Add(extra);
        return extra;
        //return null; // Placeholder makes compiler happy. 
    }
    
    public GameObject GetArcRocket()
    {
        foreach (GameObject obj in arcRocketPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject extra = Instantiate(arcRocketProjectile);
        extra.SetActive(false);
        arcRocketPool.Add(extra);
        return extra;
        //return null; // Placeholder makes compiler happy. 
    }
    
    public GameObject GetBombRocket()
    {
        foreach (GameObject obj in bombRocketPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject extra = Instantiate(bombRocketProjectile);
        extra.SetActive(false);
        bombRocketPool.Add(extra);
        return extra;
        //return null; // Placeholder makes compiler happy. 
    }
}
