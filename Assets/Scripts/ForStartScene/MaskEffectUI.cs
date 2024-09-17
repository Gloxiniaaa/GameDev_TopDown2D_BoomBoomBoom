using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MaskEffectUI : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public RectTransform rect_itself;
    private Coroutine curCo = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curCo == null)
        {
            curCo = StartCoroutine(MaskButtonEffect(time));
        }
    }
    IEnumerator MaskButtonEffect(float time)
    {
        rect_itself.DOAnchorPosX(rect_itself.anchoredPosition.x + 94f, time).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(time);
        rect_itself.DOAnchorPosX(rect_itself.anchoredPosition.x - 94f, time).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(time);
        curCo = null;
    }
}
