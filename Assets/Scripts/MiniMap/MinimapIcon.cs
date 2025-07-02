using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Minimap icon will track a 3D MinimapObject and update its position accordingly.
/// </summary>
[RequireComponent(typeof(Image))]
public class MinimapIcon : MonoBehaviour
{
    // Getter and setter of minimapIconType
    public MinimapTypeEnum IconType { get; set; }

    // local scaler, initialized in Init method
    private Vector2 scaler;

    // UI element for displaying the sprite
    private Image iconImage;

    // Transform of tacked 3D object and the center of the arena
    private Transform trackedObjTransform, arenaCenter;

    // Tracked 3D object
    private IMinimapObject miniMapObj;
    private RectTransform rectTransform;

    /// <summary>
    /// Set up all essential elements and have this icon listen to the 3D object's onDestroyed event  
    /// so it can destroy itself when the object is destroyed.
    /// </summary>
    public void Init(IMinimapObject miniMapObj, Transform arenaCenter, Vector3 scaler, Sprite sprite)
    {
        iconImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        this.arenaCenter = arenaCenter;
        this.scaler = scaler;
        this.miniMapObj = miniMapObj;
        this.miniMapObj.onDestroyed += OnTrackedObjectDestroyed;
        iconImage.sprite = sprite;
        this.trackedObjTransform = miniMapObj.gameObject.transform;
    }

    void FixedUpdate()
    {
        // If the trackedObj is valid
        if (trackedObjTransform != null)
        {
            // update its position
            Vector3 offset = trackedObjTransform.position - arenaCenter.position;
            rectTransform.anchoredPosition = new Vector2(offset.x * scaler.x, offset.z * scaler.y);
        }
    }

    void OnDestroy()
    {
        // unregister the onDestroyed event.
        miniMapObj.onDestroyed -= OnTrackedObjectDestroyed;
    }

    void OnTrackedObjectDestroyed()
    {
        // clear the reference to make fixedUpdate stop working.
        trackedObjTransform = null;
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
