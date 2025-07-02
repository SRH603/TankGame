using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public List<WaveConfigs> waveConfigs;
    private int curWaveIdx;

    private StateMachine stateMachine;
    private List<TankController> enemyList;

    public TankController playerTank;
    private bool isPlayerDead;

    [SerializeField] SpawnManager spawnManager;
    [SerializeField] GameObject countdownPanel;
    
    private float curCountdownTime;
    
    [Header("UI Elements")]
    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] Slider healthBarSlider;
    

    private enum WaveState
    {
        CheckWave = 0,
        DuringWave,
        EndWave,
        Num
    }

    private void Init()
    {
        InitStateMachine();
        enemyList = new List<TankController>();
        curWaveIdx = 0;
        isPlayerDead = false;
        playerTank.onDead += OnPlayerDead;
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine((int)WaveState.Num);

        stateMachine.SetStateFunctions((int) WaveState.CheckWave, EnterCheckWaveState, UpdateCheckWaveState, ExitCheckWaveState);
        stateMachine.SetStateFunctions((int) WaveState.DuringWave, EnterDuringWaveState, UpdateDuringWaveState, ExitDuringWaveState);
        stateMachine.SetStateFunctions((int) WaveState.EndWave, EnterEndWaveState, UpdateEndWaveState, ExitEndWaveState);
    }

    private void EnterCheckWaveState()
    {
        if (curWaveIdx < waveConfigs.Count)
        {
            int numOfEnemies = waveConfigs[curWaveIdx].numOfEnemies;
            spawnManager.SetTankConfigs(waveConfigs[curWaveIdx].tankConfigs);
            for (int i = 0; i < numOfEnemies; i++)
            {
                spawnManager.SetTankConfig(waveConfigs[curWaveIdx].tankConfigs[i]);
                TankController newEnemy = spawnManager.SpawnTankInArena().GetComponent<TankController>();
                newEnemy.onDead += OnEnemyDead;
                enemyList.Add(newEnemy);
            }
            stateMachine.SetState((int) WaveState.DuringWave);
        }
        else
        {
            GameManager.instance.CurrentGameData.levelProgressList[GameManager.instance.CurrentLevelIndex].isCleared = true;
            stateMachine.SetState((int) WaveState.EndWave);
            GameManager.instance.SetState(GameState.End);
        }
    }
    private void UpdateCheckWaveState()
    {
        // Countdown Feature
        float tempTime;
        if (curCountdownTime > 0)
        {
            
        }
    }
    
    private void ExitCheckWaveState()
    {
        
    }
    
    // DuringWave
    private void EnterDuringWaveState()
    {
        
    }
    private void UpdateDuringWaveState()
    {
        if (isPlayerDead || enemyList.Count == 0)
        {
            stateMachine.SetState((int) WaveState.EndWave);
        }
        
        enemyCountText.text = enemyList.Count.ToString();
        
    }
    
    private void ExitDuringWaveState()
    {
        
    }
    
    // EndWave
    private void EnterEndWaveState()
    {
        // is PlayerDead or not
        if (isPlayerDead)
        {
            // Bring me to the end scene
            GameManager.instance.SetState(GameState.End);
        }
        else // 
        {
            curWaveIdx++;
            stateMachine.SetState((int)WaveState.CheckWave);
        }
    }
    private void UpdateEndWaveState()
    {
        
    }
    
    private void ExitEndWaveState()
    {
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
        stateMachine.SetState((int)WaveState.CheckWave);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.UpdateState();
        healthBarSlider.value = playerTank.GetComponent<TankController>().GetHealthPercent();
        //Debug.Log(playerTank.GetComponent<TankController>().GetHealthPercent());
    }

    private void OnPlayerDead(TankController tankController)
    {
        tankController.onDead -= OnPlayerDead;
        isPlayerDead = true;
    }

    private void OnEnemyDead(TankController tankController)
    {
        tankController.onDead -= OnEnemyDead;
        enemyList.Remove(tankController);
    }
}
