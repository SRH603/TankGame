using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Minimap object types
/// </summary>
[Serializable]
public enum MinimapTypeEnum
{
    Player = 0,
    Enemy,
    Wall,
    DestructibleWall,
    PickupHealing,
    PickupLaser,
    Num
}

/// <summary>
/// Icon data helps minimap build the lookup dictionary
/// </summary>
[Serializable]
public class IconData
{
    public MinimapTypeEnum type;
    public Sprite sprite;
}

/// <summary>
/// Display objects that belong to the detectLayer on the minimap.
/// </summary>
public class Minimap : MonoBehaviour
{
    public static Minimap Instance;
    [SerializeField] Collider arena;
    // Icon configs
    [SerializeField] List<IconData> iconDataList;
    Dictionary<MinimapTypeEnum, IconData> iconDataDic;
    // UI icon template for minimap icons
    [SerializeField] GameObject minimapIconPrefab;

    // Tracked object list
    private HashSet<IMinimapObject> minimapObjects;

    private float arenaWidth, arenaHeight;
    private float mapWidth, mapHeight;

    // Scaler for mapping a 3D object's position to a 2D position on the minimap.
    private Vector3 scaler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Init the minimap by assign all the size information and create the dictionary for 
    /// getting icon sprite by MinimapTypeEnum.
    /// </summary>
    private void Init()
    {
        arenaWidth = arena.bounds.size.x;
        arenaHeight = arena.bounds.size.z;
        mapWidth = GetComponent<RectTransform>().rect.size.x;
        mapHeight = GetComponent<RectTransform>().rect.size.y;

        // the scaler can be set at the beginning unless the map changes size at runtime.
        scaler.x = mapWidth / arenaWidth;
        scaler.y = mapHeight / arenaHeight;

        // build a lookup dictionary for icon data
        iconDataDic = new Dictionary<MinimapTypeEnum, IconData>();
        foreach (var iconData in iconDataList)
        {
            iconDataDic[iconData.type] = iconData;
        }

        minimapObjects = new();
    }

    /// <summary>
    /// Add this 3D object to the hashset. If this object already in the hashset, it won't
    /// create the icon on the map.
    /// </summary>
    /// <param name="minimapObject">3D object to add into the hashset.</param>
    /// <returns></returns>
    public bool AddToObjectHashSet(IMinimapObject minimapObject)
    {
        if (minimapObjects.Add(minimapObject))
        {
            minimapObject.onDestroyed += () => { minimapObjects.Remove(minimapObject); };
            CreateMinimapIcon(minimapObject);
            return true;
        }
        else
        {
            // object already in the hashset
            return false;
        }
    }

    /// <summary>
    /// Create a icon on the minimap for the tracked object.
    /// </summary>
    /// <param name="minimapObject">3D object to track.</param>
    private void CreateMinimapIcon(IMinimapObject minimapObject)
    {
        // Create a minimapIcon that tracks this object.
        MinimapIcon minimapIcon = Instantiate(minimapIconPrefab, transform).GetComponent<MinimapIcon>();
        minimapIcon.IconType = minimapObject.MinimapType;

        // Initialize the icon.
        minimapIcon.Init(minimapObject, arena.transform, scaler, iconDataDic[minimapObject.MinimapType].sprite);
    }

    /// <summary>
    /// Debug purpose, when the minimap is selected, a red box of the bounds will show up.
    /// </summary>
    void OnDrawGizmos()
    {
        if (arena == null) return;
        Bounds arenaBounds = arena.bounds;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(arenaBounds.center, arenaBounds.size);
    }
}

