using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryCanController : PickupBehaviour
{
    public float FuelAmount = 50f;

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
            CarObject.Fuel += FuelAmount;
            if(CarObject.Fuel >= 100)
            {
                CarObject.Fuel = 100;
            }
            gameObject.SetActive(false);
        }
    }
}
