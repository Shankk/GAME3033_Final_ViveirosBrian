using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : MonoBehaviour
{
    public int ScrapAmount = 50;

    private void OnTriggerEnter(Collider other)
    {
        var CarTag = other.tag;
        var CarObject = other.gameObject.GetComponentInParent<Car>();
        if (CarTag == "CarCollider")
        {
            CarObject.Scrap += ScrapAmount;
            gameObject.SetActive(false);
        }
    }
}
