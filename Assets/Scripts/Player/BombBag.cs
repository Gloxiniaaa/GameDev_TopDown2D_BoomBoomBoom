using System.Collections.Generic;
using MyObjectPool;
using UnityEngine;

public class BombBag : MonoBehaviour
{
    [SerializeField] private PlayerAttributeSO _playerAttribute;
    private Queue<GameObject> _pool;
    private int usedAmount = 0;

    [SerializeField] private AudioGroupSO _bombInstallSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] private AudioEventChannelSO _sfxChannel;

    [Header("Listen on channel:")]
    [SerializeField] private GameObjectEventChannelSO _returnBombToPoolChannel;

    private void Awake()
    {
        _pool = new Queue<GameObject>(_playerAttribute.BombAmount);
        for (int i = 0; i < _playerAttribute.BombAmount; i++)
        {
            GameObject bomb = Instantiate(_playerAttribute.BombPrefab);
            bomb.SetActive(false);
            _pool.Enqueue(bomb);
        }
    }

    private void OnEnable()
    {
        _returnBombToPoolChannel.OnEventRaised += ReturnToPool;
    }

    public void InstallBomb(Vector3 position)
    {
        if (usedAmount < _playerAttribute.BombAmount)
        {
            _sfxChannel.RaiseEvent(_bombInstallSfx);
            usedAmount++;
            GameObject bomb;
            if (_pool.Count == 0)
            {
                bomb = Instantiate(_playerAttribute.BombPrefab);
            }
            else
            {
                bomb = _pool.Dequeue();
            }
            bomb.transform.position = position;
            bomb.SetActive(true);
        }
    }

    private void ReturnToPool(GameObject bomb)
    {
        usedAmount--;
        bomb.SetActive(false);
        _pool.Enqueue(bomb);
    }

    private void OnDisable()
    {
        _returnBombToPoolChannel.OnEventRaised -= ReturnToPool;
    }
}