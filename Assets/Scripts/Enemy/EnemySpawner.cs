using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _cdTime;
    [SerializeField] private float _startTime;
    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnEnemy), _startTime, _cdTime);
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemy, transform.position, Quaternion.identity);
    }
}
