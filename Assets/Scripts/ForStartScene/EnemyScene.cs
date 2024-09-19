using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyScene : MonoBehaviour
{
    [SerializeField] private RectTransform _itself;
    private Coroutine _curCo = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_curCo == null) {
            _curCo = StartCoroutine(EnemyEffect());
        }
    }
    
    IEnumerator EnemyEffect() {
        _itself.DOScaleY(3.8f, 1f);
        yield return new WaitForSeconds(0.2f);
        _itself.DOAnchorPos(new Vector2(73f, -152f), 0.8f);
        yield return new WaitForSeconds(0.8f);

        _itself.DOScaleY(3.2f, 1f);
        yield return new WaitForSeconds(0.2f);
        _itself.DOAnchorPos(new Vector2(79f, -141f), 0.8f);
        yield return new WaitForSeconds(0.8f);

        _curCo = null;
    }
}
