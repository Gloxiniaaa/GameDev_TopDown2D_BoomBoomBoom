using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _cdTime;

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1, _cdTime);
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemy, transform.position, Quaternion.identity);
    }
}
