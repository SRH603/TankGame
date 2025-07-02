using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupLaser", menuName = "Scriptable Objects/PickupLaser")]
public class PickupLaser : PickupSO
{
    [SerializeField] private int healingValue;
    //public MinimapTypeEnum MinimapType => MinimapTypeEnum.PickupHealing;
    public override void DoEffect(TankController tankController)
    {
        tankController.GetComponent<PlayerController>().tankAttack = tankController.AddComponent<LaserAttack>();
        tankController.GetComponent<LaserAttack>().SetFirePoint(tankController.GetComponent<StraightTankAttack>().GetFirePoint());
        tankController.GetComponent<LaserAttack>().SetFireCenter(tankController.GetComponent<StraightTankAttack>().GetFireCenter());
        tankController.GetComponent<LaserAttack>().lineRendererPrefab = GameObject.Find("LaserLine").GetComponent<LineRenderer>();
        Destroy(this.GameObject());
    }
}
