using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyTankObj;
    [SerializeField] GameObject tankTemplateObj;
    [SerializeField] ArenaSampler sampler;
    [SerializeField] private TankConfig tankConfig;
    
    [SerializeField] List<TankConfig> tankConfigs;

    public GameObject SpawnTankInArena()
    {
        TankConfig config = GetRandomItemFromList(tankConfigs);
        /*foreach (var eachConfig in tankConfigs)
        {
            SpawnTank(eachConfig, sampler.GetNavMeshPointInBound(), Quaternion.identity, false);
        }*/
        return SpawnTank(tankConfig, sampler.GetNavMeshPointInBound(), Quaternion.identity, false);
    }

    private T GetRandomItemFromList<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    
    private GameObject SpawnTank(TankConfig config, Vector3 spawnPos, Quaternion spawnRot, bool isPlayer)
    {
        GameObject tankObj = Instantiate(tankTemplateObj, spawnPos, spawnRot);
        TankRef tankRef = tankObj.GetComponent<TankRef>();

        if (null == tankRef)
        {
            throw new NullReferenceException("Tank template should have a TankRef");
        }

        // TankColor
        tankRef.renderer.material.color = config.color;

        // TankMove
        TankMove tankMove = tankRef.AddComponent<TankMove>();
        tankMove.moveSpeed = config.MoveSpeed;
        tankMove.rotSpeed = config.RotSpeed;
        
        // TankAttack
        TankAttack tankAttack = null;
        switch (config.AttackType)
        {
            case AttackType.Straight:
                tankAttack = tankRef.AddComponent<StraightTankAttack>();
                break;
            case AttackType.Arc:
                tankAttack = tankRef.AddComponent<ArcAttack>();
                break;
            case AttackType.Bomb:
                tankAttack = tankRef.AddComponent<BombAttack>();
                break;
            case AttackType.Laser:
                tankAttack = tankRef.AddComponent<LaserAttack>();
                tankRef.GetComponent<LaserAttack>().lineRendererPrefab = tankRef.laserRenderer;
                break;
            case AttackType.Fan:
                tankAttack = tankRef.AddComponent<FanAttack>();
                break;
            default:
                throw new Exception("You should have a valid attack type");
        }
        tankAttack.SetFirePower(config.FirePower);
        tankAttack.SetFirePoint(tankRef.firePoint);
        tankAttack.SetFireCenter(tankRef.fireCenter);
        
        // Tank Aim
        TankAim tankAim = tankRef.turret.AddComponent<TankAim>();
        
        if (isPlayer)
        {
            
        }
        else
        {
            tankObj.AddComponent<EnemyController>();
            tankObj.GetComponent<EnemyController>().Health = 20;
            tankObj.GetComponent<EnemyController>().obstacleMask = LayerMask.GetMask("Environment");
            tankObj.GetComponent<EnemyController>().tankMove = tankMove;
            tankObj.GetComponent<EnemyController>().tankAttack = tankAttack;
            tankObj.GetComponent<EnemyController>().tankAim = tankAim;
            tankObj.GetComponent<EnemyController>().firePoint = tankRef.firePoint;
            if (tankRef.GetComponent<LaserAttack>())
            {
                tankObj.GetComponent<EnemyController>().fireInterval = 3f;
            }
            //Debug.Log(tankRef.firePoint);

            //GameObject instantTank = Instantiate(enemyTankObj, spawnPos, spawnRot);
            //instantTank.GetComponent<TankController>().Init();
        }

        return tankObj;
    }

    public void SetTankConfigs(List<TankConfig> configs)
    {
        this.tankConfigs = configs;
    }
    
    public void SetTankConfig(TankConfig config)
    {
        this.tankConfig = config;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnTankInArena();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
