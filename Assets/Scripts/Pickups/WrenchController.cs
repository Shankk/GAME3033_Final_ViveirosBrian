using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchController : PickupBehaviour
{
    public float RepairAmount = 50f;

    private void FixedUpdate()
    {
        FloatObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        var CarTag = other.tag;
        var CarObject = other.gameObject.GetComponentInParent<Vehicle>();
        if (CarTag == "CarCollider")
        {
            CarObject.Health += RepairAmount;
            if (CarObject.Health >= 100)
            {
                CarObject.Health = 100;
            }
            gameObject.SetActive(false);
        }
    }
}
