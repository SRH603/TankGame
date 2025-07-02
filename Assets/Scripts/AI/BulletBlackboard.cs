using System.Collections.Generic;
using UnityEngine;

public class BulletBlackboard : MonoBehaviour
{
    public static BulletBlackboard Instance { get; private set; }

    readonly List<Rigidbody> bullets = new();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Register(Rigidbody rb)    => bullets.Add(rb);
    public void Unregister(Rigidbody rb)  => bullets.Remove(rb);

    public IReadOnlyList<Rigidbody> ActiveBullets => bullets;
}