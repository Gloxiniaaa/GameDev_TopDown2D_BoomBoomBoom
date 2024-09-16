using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("Fireball")){
            BoomEffect activeBoom = other.GetComponent<BoomEffect>();
            if(activeBoom != null){
                activeBoom.explosion();
                activeBoom.deleteObj();
            }
            
        }
        
    }
    
}
