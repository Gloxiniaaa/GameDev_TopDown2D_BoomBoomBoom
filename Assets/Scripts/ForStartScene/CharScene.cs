using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharScene : MonoBehaviour
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
            _curCo = StartCoroutine(charEffect());
        }
    }
    IEnumerator charEffect() {
        _itself.DOAnchorPosY(155f, 1f);
        yield return new WaitForSeconds(1f);
        _itself.DOAnchorPosY(140f, 1f);
        yield return new WaitForSeconds(1f);
        _curCo = null;
    }
}
