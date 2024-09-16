using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhaceRangeItem : ItemClass
{
    protected override void itemEffect()
    {
        BoomManager.boomInstance.increaseRange();
    }

    private void Update() {
        base.eatItem();
    }
    

}
