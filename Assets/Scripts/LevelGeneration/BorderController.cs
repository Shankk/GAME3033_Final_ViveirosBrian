using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    public BoxCollider bCollider;

    private void OnTriggerExit(Collider other)
    {
        var Car = other.GetComponentInParent<Vehicle>();
        if(Car != null)
        bCollider.isTrigger = false;
    }
}
