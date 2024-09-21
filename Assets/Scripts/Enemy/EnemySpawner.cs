using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    private Coroutine _cur = null;

    private void OnEnable() {
        InvokeRepeating(nameof(SpawnEnemy), 1, 10f);
    }
    private void SpawnEnemy() {
        Instantiate(_enemy,transform.position, Quaternion.identity);
    }
    

}
