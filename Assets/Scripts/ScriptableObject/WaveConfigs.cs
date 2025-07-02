using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfigs", menuName = "Scriptable Objects/WaveConfigs")]
public class WaveConfigs : ScriptableObject
{
    public int numOfEnemies;
    public List<TankConfig> tankConfigs;
}
