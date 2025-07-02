using UnityEngine;

public abstract class PickupSO : ScriptableObject
{
    
    public MinimapTypeEnum MinimapType;
    public abstract void DoEffect(TankController tankController);
    
}
