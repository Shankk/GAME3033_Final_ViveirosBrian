using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public float Health = 100f;
    public float Fuel = 100f;
    public float FuelUsage = 0.25f;
    public float Scrap = 0f;

    public Transform centerOfMass;
    public float motorTorque = 1500f;
    public float maxSteer = 20f;
    public bool isCarWorking = true;
    public float Steer { get; set; }
    public float Throttle { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;

    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    private void Update()
    {
        //if (isCarWorking)
        
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;
        
        foreach(var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }
    }
}
