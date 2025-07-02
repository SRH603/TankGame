using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupHealing", menuName = "Scriptable Objects/PickupHealing")]
public class PickupHealing : PickupSO
{
    [SerializeField] private int healingValue;
    //public MinimapTypeEnum MinimapType => MinimapTypeEnum.PickupHealing;
    public override void DoEffect(TankController tankController)
    {
        tankController.Health += healingValue;
        Destroy(this.GameObject());
    }
}
