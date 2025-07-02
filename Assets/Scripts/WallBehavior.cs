using System;
using UnityEngine;

public class WallBehavior : MonoBehaviour, IMinimapObject
{
    public MinimapTypeEnum MinimapType => MinimapTypeEnum.Wall;

    public Action onDestroyed
    {
        get => onDeath;
        set => onDeath = value;
    }

    private Action onDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Minimap.Instance.AddToObjectHashSet(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
