using System;
using System.Collections;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Target target;
    [field: SerializeField] public Transform TargetStart { get; private set; }
    [field: SerializeField] public float TargetMoveDownDist { get; private set; }

    private Coroutine spawnTargetsRoutine;

    private void OnEnable()
    {
        EventManager.OnGameEnded += StopSpawningTargets;
        EventManager.OnGameRestarted += ResetTargets;
    }
    private void OnDisable()
    {
        EventManager.OnGameEnded -= StopSpawningTargets;
        EventManager.OnGameRestarted -= ResetTargets;
    }

    void Start()
    {
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

    private void SpawnTarget<T>() where T : TargetBuilder, new()
    {
        TargetBuilder targetBuilder = new T();
        targetBuilder.Target = Instantiate(target); // replace with Get() from object pool ???
        targetBuilder.Target.Init(this);
        targetBuilder.Construct();
        targetBuilder.Target.transform.position = TargetStart.position;
    }

    public void TargetHit(Target target) => EventManager.TargetHit(target);

    private void StopSpawningTargets()
    {
        StopCoroutine(spawnTargetsRoutine);
    }

    private void ResetTargets()
    {
        // clear object pool
    }

}
