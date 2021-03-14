using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchController : MonoBehaviour
{
    public float RepairAmount = 50f;

    private void OnTriggerEnter(Collider other)
    {
        var CarTag = other.tag;
        var CarObject = other.gameObject.GetComponentInParent<Car>();
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
