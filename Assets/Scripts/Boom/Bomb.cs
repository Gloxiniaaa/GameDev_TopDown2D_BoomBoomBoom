using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private BombAttributeSO _bombAttribute;
    [SerializeField] private AudioGroupSO _explodeSfx;
    private List<GameObject> _explosions;
    private int _explosionIndex = 0;
    private bool _isExploded = false;
    private Sequence _bubbleEffect;


    [Header("Broadcast on channel:")]
    [SerializeField] private GameObjectEventChannelSO _returnBombToPoolChannel;
    [SerializeField] private AudioEventChannelSO _sfxChannel;

    private void Awake()
    {
        InitilizeExplosions();
    }

    private void InitilizeExplosions()
    {
        _explosions = new List<GameObject>(_bombAttribute.Range * 4 + 1);
        for (int i = 0; i < _bombAttribute.Range * 4 + 1; i++)
        {
            GameObject explosion = Instantiate(_bombAttribute.ExplosionPrefab);
            explosion.SetActive(false);
            _explosions.Add(explosion);
        }
    }

    private void Start()
    {
        SetBubbleEffect();
    }

    private void SetBubbleEffect()
    {
        _bubbleEffect = DOTween.Sequence();
        _bubbleEffect.Append(transform.DOScaleY(0.9f, 0.2f).SetEase(Ease.InOutQuad));
        _bubbleEffect.Append(transform.DOScaleY(1.1f, 0.2f).SetEase(Ease.InOutQuad));
        _bubbleEffect.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        _isExploded = false;
        _bubbleEffect.Play();
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(_bombAttribute.CountDownTime);
        _sfxChannel.RaiseEvent(_explodeSfx);
        Explode();
    }

    public void Explode()
    {
        if (_isExploded)
            return;

        _isExploded = true;

        if (_explosions.Count < _bombAttribute.Range * 4)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject explosion = Instantiate(_bombAttribute.ExplosionPrefab);
                explosion.SetActive(false);
                _explosions.Add(explosion);
            }
        }

        // center
        _explosions[_explosionIndex].SetActive(true);
        _explosions[_explosionIndex++].transform.position = new Vector2(transform.position.x, transform.position.y);
        ExtendExplosion(Vector2.up);
        ExtendExplosion(Vector2.down);
        ExtendExplosion(Vector2.right);
        ExtendExplosion(Vector2.left);

        // done
        gameObject.SetActive(false);
    }

    private void ExtendExplosion(Vector3 direction)
    {
        for (int i = 0; i < _bombAttribute.Range; i++)
        {
            RaycastHit2D obstacleDetector = Physics2D.Raycast(transform.position + direction * (i + 1), direction, 0.1f, 1 << Constant.SolidLayer);
            if (obstacleDetector.collider)
            {
                if (obstacleDetector.collider.CompareTag(Constant.DestroyableTag) || obstacleDetector.collider.CompareTag(Constant.BombTag))
                {
                    _explosions[_explosionIndex].SetActive(true);
                    _explosions[_explosionIndex++].transform.position = transform.position + direction * (i + 1);
                }
                return;
            }
            else
            {
                _explosions[_explosionIndex].SetActive(true);
                _explosions[_explosionIndex++].transform.position = transform.position + direction * (i + 1);
            }
        }
    }

    private void OnDisable()
    {
        _bubbleEffect.Kill();
        _returnBombToPoolChannel.RaiseEvent(gameObject);
        _explosionIndex = 0;
    }
}