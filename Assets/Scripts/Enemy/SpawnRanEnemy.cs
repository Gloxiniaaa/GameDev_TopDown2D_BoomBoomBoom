using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRanEnemy : MonoBehaviour
{
   [SerializeField] private List<GameObject> _eneList;
    [SerializeField] private float _cdTime;
    [SerializeField] private float _startTime;
    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnEnemy), _startTime, _cdTime);
    }

    private void SpawnEnemy()
    {
        int num = Random.Range(0, _eneList.Count);
        Instantiate(_eneList[num], transform.position, Quaternion.identity);
    }
}
