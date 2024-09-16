using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : MonoBehaviour
{
    protected bool eaten = false;
    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            eaten = true;
        }
    }
    protected void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            eaten = false;
        }
    }
    protected void eatItem(){
        if (eaten && Input.GetKeyDown(KeyCode.E)) {
            itemEffect();
        }

        
    }
    protected abstract void itemEffect();
}
