using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float damageScaler = 0.25f;

    private void OnCollisionEnter(Collision collision)
    {
        
        var CarTag = collision.collider.GetComponentInParent<Vehicle>();
        if (CarTag != null)
        {
            var appliedDamage = CarTag.FrameStrength * (collision.relativeVelocity.magnitude * damageScaler);
            CarTag.Health -= appliedDamage;
            //Debug.Log("Relative Velocity: " + collision.relativeVelocity.magnitude + " Damage Inflicted: " + appliedDamage);
        }
    }
}
