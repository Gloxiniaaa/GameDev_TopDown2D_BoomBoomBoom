using UnityEngine;
using DG.Tweening;

public class CamShake : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] private float _duration;
    [SerializeField] private float _intensity;
    
    [Header("Listen on channel:")]
    [SerializeField] private VoidEventChannelSO _camShakeChannel;


    private void OnEnable()
    {
        _camShakeChannel.OnEventRaised += Shake;
    }

    public void Shake() {
        _cam.DOShakePosition(_duration, _intensity);
        _cam.DOShakeRotation(_duration, _intensity);
    }

    private void OnDisable()
    {
        _camShakeChannel.OnEventRaised -= Shake;
    }
    
}
