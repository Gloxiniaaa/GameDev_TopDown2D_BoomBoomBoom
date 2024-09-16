using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private BombAttributeSO _bombAttribute;
    private List<GameObject> _explosions;
    private bool _isExploded = false;

    [Header("Broadcast on channel:")]
    [SerializeField] private GameObjectEventChannelSO _returnBombToPoolChannel;

    private void Awake()
    {
        Initilize();
    }

    private void OnEnable()
    {
        _isExploded = false;
        BubbleEffect();
        Invoke(nameof(Explode), _bombAttribute.CountDownTime);
    }

    private void Initilize()
    {
        _explosions = new List<GameObject>(_bombAttribute.Range * 4 + 1);
        for (int i = 0; i < _bombAttribute.Range * 4 + 1; i++)
        {
            GameObject explosion = Instantiate(_bombAttribute.ExplosionPrefab);
            explosion.SetActive(false);
            _explosions.Add(explosion);
        }
    }

    private void BubbleEffect()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScaleY(0.9f, 0.2f).SetEase(Ease.InOutQuad));
        sequence.Append(transform.DOScaleY(1.1f, 0.2f).SetEase(Ease.InOutQuad));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    public void Explode()
    {
        if (_isExploded)
            return;

        _isExploded = true;
        int index = 0;

        // center
        _explosions[index].SetActive(true);
        _explosions[index++].transform.position = new Vector2(transform.position.x, transform.position.y);

        for (int i = 0; i < _bombAttribute.Range; i++)
        {
            if (index < _bombAttribute.Range * 4 + 1)
            {
                for (int j = 0; j < 4; j++)
                {
                    GameObject explosion = Instantiate(_bombAttribute.ExplosionPrefab);
                    explosion.SetActive(false);
                    _explosions.Add(explosion);
                }
            }
            _explosions[index].SetActive(true);
            _explosions[index++].transform.position = new Vector2(transform.position.x, transform.position.y + i + 1);
            // down
            _explosions[index].SetActive(true);
            _explosions[index++].transform.position = new Vector2(transform.position.x, transform.position.y - i - 1);
            // right
            _explosions[index].SetActive(true);
            _explosions[index++].transform.position = new Vector2(transform.position.x + i + 1, transform.position.y);
            // left
            _explosions[index].SetActive(true);
            _explosions[index++].transform.position = new Vector2(transform.position.x - i - 1, transform.position.y);
        }
        AfterExlode();
    }

    private void AfterExlode()
    {
        transform.DOKill();
        _returnBombToPoolChannel.RaiseEvent(gameObject);
        gameObject.SetActive(false);
    }
}