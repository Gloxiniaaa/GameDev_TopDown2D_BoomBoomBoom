using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
      // Start is called before the first frame update
    public RectTransform but_rect;
    private Coroutine curCo = null;
    private bool isPoint = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curCo == null && !isPoint)
        {
            curCo = StartCoroutine(ButtonOfEffect());
        }
        if(isPoint )
        {
            but_rect.DORotate(Vector3.zero, 0.1f);
        }
    }
    IEnumerator ButtonOfEffect()
    {
        but_rect.DORotate(new Vector3(0f, 0f, 4f), 1f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(1f);
        but_rect.DORotate(new Vector3(0f, 0f, -4f), 1f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(1f);
        curCo = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopCoroutine(curCo);
        
        isPoint = true;
        but_rect.Rotate(Vector3.zero);
        but_rect.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.5f).SetDelay(0.01f).SetEase(Ease.InOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        but_rect.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).SetEase(Ease.InOutQuad);
        curCo = null;
        isPoint = false;
    }
}
