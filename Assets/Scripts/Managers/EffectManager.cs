using UnityEngine;

/// <summary>
/// Responsible for all effects in the scene. This manager does not need to persist across scenes.
/// </summary>
public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    [SerializeField] ParticleSystem explosionEffect;

    [SerializeField] ParticleSystem bulletSparkEffect;

    [SerializeField] ParticleSystem wallDestroyEffect;

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

    /// <summary>
    /// Plays the explosion effect at the specified position.
    /// </summary>
    /// <param name="pos">The world position where the effect should take place.</param>
    public void PlayExplosion(Vector3 pos)
    {
        
    }

    /// <summary>
    /// Plays the bullet spark effect at the specified position.
    /// </summary>
    /// <param name="pos">The world position where the effect should take place.</param>
    public void PlayBulletSpark(Vector3 pos)
    {

    }

    /// <summary>
    /// Plays the wall destroy effect at the specified position.
    /// </summary>
    /// <param name="pos">The world position where the effect should take place.</param>
    public void PlayWallDestroy(Vector3 pos)
    {

    }
}