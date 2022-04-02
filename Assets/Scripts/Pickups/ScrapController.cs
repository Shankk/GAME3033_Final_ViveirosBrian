using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : PickupBehaviour
{
    public int ScrapAmount = 50;

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
            CarObject.Scrap += ScrapAmount;
            gameObject.SetActive(false);
        }
    }
}
