using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private AudioGroupSO _enemyDieSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] private AudioEventChannelSO _sfxChannel;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private Grid _grid;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _timeToChangeDir = 3f;
    private Vector3 _dir;
    private Vector3 _nextPos;
    private int _dirXHash = Animator.StringToHash("MoveHori");
    private int _dirYHash = Animator.StringToHash("MoveVerti");
    private int _dieForAnim = Animator.StringToHash("DieAnim");
    private int _getDie = Animator.StringToHash("GetDie");
    private bool _changeDir = false;
    private bool _checkDie = false;
    private Coroutine _curCo = null;


    private void Awake()
    {
        _grid = GameObject.Find("Grid").GetComponent<Grid>();
        StartCoroutine(Init());
    }
    IEnumerator Init()
    {
        _rb.velocity = Vector3.zero;
        _checkDie = true;
        yield return new WaitForSeconds(1.6f);
        _checkDie = false;
    }
    private void Start()
    {
        Vector3Int curPos = _grid.WorldToCell(transform.position);

        int initDir = Random.Range(0, 4);
        if (initDir == 0) _dir = Vector3.down;
        else if (initDir == 1) _dir = Vector3.left;
        else if (initDir == 2) _dir = Vector3.right;
        else _dir = Vector3.up;

        _nextPos = _grid.CellToWorld(curPos) + _dir;
    }

    private void Update()
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

    private void MoveAnim()
    {
        _anim.SetFloat(_dirXHash, _rb.velocity.x);
        _anim.SetFloat(_dirYHash, _rb.velocity.y);
    }

    private IEnumerator TimeCounter(float time)
    {

        yield return new WaitForSeconds(time);
        _curCo = null;
        _changeDir = true;
    }
    private void DetectNextStep()
    {
        RaycastHit2D hit = Physics2D.Raycast(_nextPos, _dir, 0.5f, _layer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(Constant.UntaggedTag) || hit.collider.CompareTag(Constant.DestroyableTag))
            {
                _dir = GetRandomDir(_dir);

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

    private void Move(Vector3 newPos)
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Constant.ExplosionTag) && !_checkDie)
        {
            _checkDie = true;
            _rb.velocity = Vector3.zero;
            DieEffect(other.transform.position);
        }
    }
    private void DieEffect(Vector3 explodePos)
    {
        _sfxChannel.RaiseEvent(_enemyDieSfx);
        Vector3 disDirection = (transform.position - explodePos).normalized;
        Vector3Int _diePos = _grid.WorldToCell(disDirection);

        transform.DOMove(Vector3Int.RoundToInt(transform.position) - _grid.GetCellCenterWorld(_diePos), 1f);
        GetComponent<SpriteRenderer>().DOFade(0, 1f).SetDelay(1f);

        int idx = (disDirection.x > 0) ? 1 : 0;
        _anim.SetTrigger(_getDie);
        _anim.SetFloat(_dieForAnim, idx);
    }
    private void InactiveAfterDie()
    {
        gameObject.SetActive(false);
    }
}