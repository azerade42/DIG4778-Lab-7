using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TargetController : MonoBehaviour, ISaveable
{
    [SerializeField] private Target target;
    [field: SerializeField] public Transform TargetStart { get; private set; }
    [field: SerializeField] public float TargetMoveDownDist { get; private set; }

    private Coroutine spawnTargetsRoutine;

    private ObjectPool<Target> targetPool = new ObjectPool<Target>();

    private void OnEnable()
    {
        EventManager.OnGameEnded += StopSpawningTargets;
        // EventManager.OnGameRestarted += ResetTargets;

        SaveManager.Instance.SaveableObjects.Add(this);
    }
    private void OnDisable()
    {
        EventManager.OnGameEnded -= StopSpawningTargets;
        // EventManager.OnGameRestarted -= ResetTargets;
    }

    void Start()
    {
        targetPool.InitalizePool(target, 40);
        spawnTargetsRoutine = StartCoroutine(SpawnRandomTargetSeries());
    }

    private IEnumerator SpawnRandomTargetSeries()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnRandomTargets());
        }
    }

    private IEnumerator SpawnRandomTargets()
    {
        int numTargets = UnityEngine.Random.Range(1, 3);

        if (UnityEngine.Random.Range(0,2) < 1)
        {
            yield return StartCoroutine(SpawnTargets<BlueTarget>(numTargets, 1f));
        }
        else
        {
            yield return StartCoroutine(SpawnTargets<RedTarget>(numTargets, 1f));
        }
    }

    private IEnumerator SpawnTargets<T>(int count, float waitBetweenSpawns) where T : TargetBuilder, new()
    {
        for (int i = 0; i < count; i++)
        {
            SpawnTarget<T>();
            yield return new WaitForSeconds(waitBetweenSpawns);
        }
    }

    private TargetBuilder SpawnTarget<T>() where T : TargetBuilder, new()
    {
        TargetBuilder targetBuilder = new T();
        targetBuilder.Target = targetPool.GetObject();
        targetBuilder.Target.Init(this);
        targetBuilder.Construct();
        targetBuilder.Target.transform.position = TargetStart.position;
        return targetBuilder;
    }

    public void TargetHit(Target target) => EventManager.TargetHit(target);

    private void StopSpawningTargets()
    {
        StopCoroutine(spawnTargetsRoutine);
    }

    public void Release(Target obj)
    {
        targetPool.ReturnObject(obj);
    }

    public void SaveData()
    {
        SaveManager.SaveData.Targets = new List<Vector3>();      

        foreach (Target target in targetPool.ActivePool)
        {
            print(target.transform.position);
            SaveManager.SaveData.Targets.Add(target.transform.position);
        }
    }

    public void LoadData()
    {
        targetPool.ReturnAllObjects();
        targetPool.InitalizePool(target, 40);

        foreach (Vector3 pos in SaveManager.SaveData.Targets)
        {
            Target target = SpawnTarget<RedTarget>().Target;
            target.transform.position = pos;
        }
    }

}
