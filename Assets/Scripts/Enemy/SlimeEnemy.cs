using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : EnemyBase
{
    
    [SerializeField] private LayerMask _spikeLayer;
    [SerializeField] private List<GameObject> _atkList;
    private int _listIndex = 0;
    private void OnEnable() {
        foreach (GameObject obj in _atkList) {
            obj.SetActive(false);
        }
    }
    private void Attack()
    {
       _atkList[_listIndex].transform.position = transform.position;
       _atkList[_listIndex].SetActive(true);
        _listIndex = (_listIndex + 1) % _atkList.Count;
    }
    protected override void Move(Vector3 newPos)
    {

        float dis = Vector2.Distance(transform.position, _nextPos);

        if ((dis <= 1.1f && dis >= 0.9f))
        {
            RaycastHit2D atkHit = Physics2D.Raycast(transform.position, _dir, 0.1f, _spikeLayer);
            if (atkHit.collider == null)
            {
                Attack();
            }

        }
        base.Move(newPos);
    }
}
