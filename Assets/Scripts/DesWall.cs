using System;
using UnityEngine;

public class DesWall : MonoBehaviour, IMinimapObject
{
    public MinimapTypeEnum MinimapType => MinimapTypeEnum.DestructibleWall;

    public Action onDestroyed
    {
        get => onDeath;
        set => onDeath = value;
    }

    public  Action onDeath;
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