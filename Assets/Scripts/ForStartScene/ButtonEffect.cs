    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;
    using UnityEngine.EventSystems;
    public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // Start is called before the first frame update
        [SerializeField] private RectTransform _butRect;
        private Sequence _se;
        private void Awake() {
            _se = DOTween.Sequence();
        }
        private void OnEnable() {

            ButtonOfEffect();
        }
        
        private void ButtonOfEffect()
        {
           
            _se.Append(_butRect.DORotate(new Vector3(0f, 0f, 5f), 1f).SetEase(Ease.InOutQuad));
            _se.Append(_butRect.DORotate(new Vector3(0f, 0f, -5f), 1f).SetEase(Ease.InOutQuad));
            _se.SetLoops(-1, LoopType.Yoyo);   
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
            _se.Kill();
            _se.Append(_butRect.DORotate(Vector3.zero, 0.1f));
            _se.Join(_butRect.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.5f).SetDelay(0.01f).SetEase(Ease.InOutQuad));
        
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           
            _se.Append(_butRect.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).SetEase(Ease.InOutQuad));
            _se.Kill();
            _se = DOTween.Sequence();
            ButtonOfEffect();
        }
        private void OnDisable(){
            _butRect.DOKill();
        }
    }
