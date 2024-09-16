using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private float _countDownTime;
    [SerializeField] private GameObject _explosionPrefab;
    private int _range = 3;
    private List<GameObject> _explosions;

    private void Awake()
    {
        Initilize();
    }

    private void OnEnable()
    {
        BubbleEffect();
        Invoke(nameof(Explode), _countDownTime);
    }

    private void Initilize()
    {
        _explosions = new List<GameObject>(_range * 4 + 1);
        for (int i = 0; i < _range * 4 + 1; i++)
        {
            GameObject explosion = Instantiate(_explosionPrefab);
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
        int index = 0;

        // center
        _explosions[index].SetActive(true);
        _explosions[index++].transform.position = new Vector2(transform.position.x, transform.position.y);

        for (int i = 0; i < _range; i++)
        {
            // up
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
        gameObject.SetActive(false);
    }
}