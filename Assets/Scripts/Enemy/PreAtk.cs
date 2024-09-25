using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PreAtk : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spren;
    private void Appear() {
        _spren.DOFade(1, 0.2f);
    }
    private void Disapear(){
        _spren.DOFade(0, 0.2f);
    }
    private void Inactive() {
        gameObject.SetActive(false);
    }
}
