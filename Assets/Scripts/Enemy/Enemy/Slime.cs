using UnityEngine;
using System.Collections.Generic;

public class Slime : BaseEnemy
{
    [SerializeField] private EnemyState _initState;
    [SerializeField] private GameObject _spike;
    private Queue<GameObject> _spikePool;
    private Vector2 _previousCrell;

    private new void Awake()
    {
        base.Awake();
        _spikePool = new Queue<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject spike = Instantiate(_spike);
            _spikePool.Enqueue(spike);
        }
    }

    private void OnEnable()
    {
        _previousCrell = GridExtensions.MapToGrid(Grid, transform.position);
        _state = _initState;
        _state.Enter();
    }

    private void Update()
    {
        _state.Do();
        Vector2 currentCell = GridExtensions.MapToGrid(Grid, transform.position);
        if (_previousCrell != currentCell)
        {
            SpawnSpike();
            _previousCrell = currentCell;
        }
    }

    private void SpawnSpike()
    {
        GameObject spikeToSpawn = _spikePool.Peek();
        if (spikeToSpawn.activeInHierarchy)
        {
            // means that all spikes are active, we have to add another one
            spikeToSpawn = Instantiate(_spike);
            _spikePool.Enqueue(spikeToSpawn);
        }
        else
        {
            spikeToSpawn = _spikePool.Dequeue();
            _spikePool.Enqueue(spikeToSpawn);
            spikeToSpawn.SetActive(true);
        }
        spikeToSpawn.transform.position = _previousCrell;
    }
}