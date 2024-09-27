using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private AudioGroupSO _enemyDieSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] protected AudioEventChannelSO _sfxChannel;
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Animator _anim;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _timeToChangeDir = 3f;
    protected Grid _grid;
    protected Vector3 _dir;
    protected Vector3 _nextPos;
    private int _dirXHash = Animator.StringToHash("MoveHori");
    private int _dirYHash = Animator.StringToHash("MoveVerti");
    private int _dieForAnim = Animator.StringToHash("DieAnim");
    private int _getDie = Animator.StringToHash("GetDie");
    private bool _changeDir = false;
    protected bool _checkDie = false;
    private bool _meetBoom = false;
    private Coroutine _curCo = null;


    private void Awake()
    {
        _grid = GameObject.Find("Grid").GetComponent<Grid>();
        StartCoroutine(Init());
    }
    private IEnumerator Init()
    {
        _rb.velocity = Vector3.zero;
        _checkDie = true;
        yield return new WaitForSeconds(1.6f);
        _checkDie = false;
    }
    protected void Start()
    {
        Vector3Int curPos = _grid.WorldToCell(transform.position);

        int initDir = Random.Range(0, 4);
        if (initDir == 0) _dir = Vector3.down;
        else if (initDir == 1) _dir = Vector3.left;
        else if (initDir == 2) _dir = Vector3.right;
        else _dir = Vector3.up;

        _nextPos = _grid.GetCellCenterWorld(curPos) + _dir;
    }

    protected virtual void Update()
    {
        if (!_checkDie)
        {
            MoveAnim();
            Move(_nextPos);
            if (!_changeDir && _curCo == null)
            {
                _curCo = StartCoroutine(TimeCounter(_timeToChangeDir));
            }
        }
    }

    protected virtual void MoveAnim()
    {
        _anim.SetFloat(_dirXHash, _rb.velocity.x);
        _anim.SetFloat(_dirYHash, _rb.velocity.y);
    }

    private IEnumerator TimeCounter(float time)
    {
        yield return new WaitForSeconds(time);
        _curCo = null;
        _meetBoom = false;
        _changeDir = true;
    }
    protected void DetectNextStep()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir, 0.5f, _layer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(Constant.UntaggedTag) || hit.collider.CompareTag(Constant.DestroyableTag))
            {
                _dir = GetRandomDir(_dir);
            }
            if (!_meetBoom && hit.collider.CompareTag(Constant.BombTag))
            {
                _dir = GetRandomDir(_dir);
                _meetBoom = true;
            }
        }
        else
        {
            float dis = Vector2.Distance(transform.position, _nextPos);
            if (_changeDir && (dis <= 1.1f && dis >= 0.9f))
            {
                _dir = GetRandomDir(_dir);
                _changeDir = false;
            }
        }
        Vector3Int nextCell = _grid.WorldToCell(transform.position + _dir);
        _nextPos = _grid.GetCellCenterWorld(nextCell);
    }

    protected virtual void Move(Vector3 newPos)
    {
        _rb.velocity = (newPos - transform.position).normalized * _speed;
        DetectNextStep();

    }

    private Vector3 GetRandomDir(Vector3 direct)
    {
        int ranNum = Random.Range(0, 3);
        if (direct == Vector3.down)
        {
            if (ranNum == 0) direct = Vector3.up;
            else if (ranNum == 1) direct = Vector3.left;
            else direct = Vector3.right;
        }
        else if (direct == Vector3.up)
        {
            if (ranNum == 0) direct = Vector3.down;
            else if (ranNum == 1) direct = Vector3.left;
            else direct = Vector3.right;
        }
        else if (direct == Vector3.right)
        {
            if (ranNum == 0) direct = Vector3.down;
            else if (ranNum == 1) direct = Vector3.left;
            else direct = Vector3.up;
        }
        else
        {
            if (ranNum == 0) direct = Vector3.down;
            else if (ranNum == 1) direct = Vector3.up;
            else direct = Vector3.right;
        }
        return direct;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Constant.ExplosionTag) && !_checkDie)
        {
            _checkDie = true;
            _rb.velocity = Vector3.zero;
            DieEffect(other.transform.position);
        }
    }
    protected void DieEffect(Vector3 explodePos)
    {
        _sfxChannel.RaiseEvent(_enemyDieSfx);
        Vector3 disDirection = (transform.position - explodePos).normalized;
        Vector3Int _diePos = _grid.WorldToCell(disDirection);

        transform.DOMove(Vector3Int.RoundToInt(transform.position) - _grid.GetCellCenterWorld(_diePos), 1f);
        SpriteRenderer sr = GetComponent<SpriteRenderer>() as SpriteRenderer;
        if (sr)
        {
            GetComponent<SpriteRenderer>().DOFade(0, 1f).SetDelay(1f);
        }

        int idx = (disDirection.x > 0) ? 1 : 0;
        _anim.SetTrigger(_getDie);
        _anim.SetFloat(_dieForAnim, idx);
        _enemyDeathChannel.RaiseEvent();
    }
    private void InactiveAfterDie()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}