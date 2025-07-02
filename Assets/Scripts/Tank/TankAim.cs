using UnityEngine;
using UnityEngine.InputSystem;

public class TankAim : MonoBehaviour
{
    public Vector3 lookTarget;

    public void UpdateAim()
    {
        transform.LookAt(lookTarget);
    }
}
