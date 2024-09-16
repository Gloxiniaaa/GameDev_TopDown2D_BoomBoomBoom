using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    [SerializeField] GameObject delObj;
    private void deleteObj() {
        Destroy(delObj);
    }
}
