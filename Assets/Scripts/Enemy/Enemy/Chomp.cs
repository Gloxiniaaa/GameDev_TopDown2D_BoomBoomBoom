using UnityEngine;

public class Chomp : BaseEnemy
{
    private EnemyState _state;
    [SerializeField] private RandomMoveState _randomMoveState;
    [SerializeField] private FlyState _flyState;


}