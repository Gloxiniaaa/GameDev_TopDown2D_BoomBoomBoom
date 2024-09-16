using UnityEngine;

[CreateAssetMenu(fileName = "RuntimeFireBallSO", menuName = "RuntimeFireBallSO", order = 0)]
public class RuntimeFireBallSO  : ScriptableObject
{
    public float CountDownTime;
    public int Range;
    public GameObject ExplosionPrefab;
}