using UnityEngine;

public class EnemyATK : MonoBehaviour
{
    protected virtual void OnEnable() {
    }

    protected virtual void SetInactive() {
        gameObject.SetActive(false);
    }
}
