using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool leftTire;
    public bool steer;
    public bool invertSteer;
    public bool power;
    public float radius = 6; 

    public float SteerAngle { get; set; }

    public float Torque { get; set; }

    public WheelCollider wheelCollider { get; set; }
    private Transform wheelTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Gathers Wheel Collider and Transform
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updates Wheel position and rotation so the wheels are actually working.
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        //Ackerman steering formula
        //steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontalInput;

        if (steer)
        {
            if (SteerAngle > 0)
            {
                if (leftTire)
                {
                    wheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * SteerAngle * (invertSteer ? -1 : 1);
                }
                else
                {
                    wheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * SteerAngle * (invertSteer ? -1 : 1);
                }
            }
            else if (SteerAngle < 0)
            {
                if (leftTire)
                {
                    wheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * SteerAngle * (invertSteer ? -1 : 1);
                }
                else
                {
                    wheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * SteerAngle * (invertSteer ? -1 : 1);
                }
            }
            else
            {
                wheelCollider.steerAngle = 0;
            }
            //wheelCollider.steerAngle = SteerAngle * (invertSteer ? -1 : 1);
        }

        if(power)
        {
            wheelCollider.motorTorque = Torque;
        }
    }
}
