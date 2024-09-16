using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomManager : MonoBehaviour
{
    public static BoomManager boomInstance;
    public int size = 1;
    private void Awake() {
        if(boomInstance == null) {
            boomInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    public void increaseRange() {
        size++;
    }
}
