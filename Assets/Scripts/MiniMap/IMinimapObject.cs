using System;
using UnityEngine;

/// <summary>
/// 3D object that can be displayed on the minimap.
/// </summary>
public interface IMinimapObject
{
    // Minimap icon Type of this object
    public MinimapTypeEnum MinimapType { get; }

    // Event when this object been destroyed.
    public Action onDestroyed { get; set; }

    // Provide interface the ability of returning gameObject.
    public GameObject gameObject { get; }
}
