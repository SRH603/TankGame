using System;
using UnityEngine;

public class Pickups : MonoBehaviour, IMinimapObject
{
    public PickupSO pickup;

    public MinimapTypeEnum MinimapType => pickup.MinimapType;
    
    public Action onDestroyed
    {
        get;
        set;
    }
    public void Start()
    {
        Minimap.Instance.AddToObjectHashSet(this);
        //MinimapType = pickup.MinimapType;
    }

    void OnTriggerEnter(Collider other)
    {
        TankController playerRef = other.gameObject.GetComponentInParent<PlayerController>();
        Debug.Log(other.gameObject);
        if (playerRef != null)
        {
            Debug.Log("999");
            pickup.DoEffect(playerRef);
            onDestroyed?.Invoke();
            Destroy(this.gameObject);
        }
    }

}
