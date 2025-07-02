using UnityEngine;

[CreateAssetMenu(fileName = "TankConfig", menuName = "Scriptable Objects/TankConfig")]
public class TankConfig : ScriptableObject
{
    public Color color;
    public float MoveSpeed;
    public float RotSpeed;
    public float FirePower;

    public AttackType AttackType;
}

[SerializeField]
public enum AttackType
{
    Basic,
    Straight,
    Arc,
    Bomb,
    Fan,
    Laser,
    Num
}