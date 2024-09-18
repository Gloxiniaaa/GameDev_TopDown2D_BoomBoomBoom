using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Name : MonoBehaviour
{
     [SerializeField] private RectTransform[] _letters;
    private Coroutine _curCo;
    private void Update() {
        if (_curCo == null) {
            _curCo = StartCoroutine(LetterEffect());
        }
    }
    IEnumerator LetterEffect() {
        for(int i = 0; i < _letters.Length; i++){
            
            _letters[i].DOAnchorPosY(-30, 1f);
            yield return new WaitForSeconds(0.2f);
        }  

        for(int i = 0; i < _letters.Length; i++){
           
            _letters[i].DOAnchorPosY(0, 1f);
            yield return new WaitForSeconds(0.2f);
        }  
        _curCo = null;
    }
}
