using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CamShake : MonoBehaviour
{
    [SerializeField] Camera _cam;
   
    public void Shake(float time, float intensity) {
        _cam.DOShakePosition(time, intensity);
        _cam.DOShakeRotation(time, intensity);
    }
    
}
