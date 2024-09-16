using UnityEngine;

[CreateAssetMenu(fileName = "StaticFireBallSO", menuName = "StaticFireBallSO", order = 0)]
public class StaticFireBallSO : ScriptableObject
{
    public int InitCapacity;
    public float InitCountDownTime;
    public int InitRange;
    public GameObject ExplosionPrefab;
}