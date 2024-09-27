using UnityEngine;

public class SoundOnJump : MonoBehaviour
{
    [SerializeField] private AudioGroupSO _jumpSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] protected AudioEventChannelSO _sfxChannel;

    private void PlayuJumpSfx()
    {
        _sfxChannel.RaiseEvent(_jumpSfx);
    }
}