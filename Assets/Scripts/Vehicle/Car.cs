using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public float Health = 100f;
    public float Fuel = 100f;
    public float FuelUsage = 0.25f;
    public float motorTorque = 1500f;
    public float maxSteer = 20f;

    public Transform centerOfMass;
    public bool ShouldVehicleWork = false;
    public int Scrap = 0;

    // Upgrade Levels
    public int EngineLevel = 1;
    public int TiresLevel = 1;
    public int FuelLevel = 1;
    public int FrameLevel = 1;

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
        if (ShouldVehicleWork)
        {
            Steer = GameManager.Instance.InputController.SteerInput;
            Throttle = GameManager.Instance.InputController.ThrottleInput;

            foreach (var wheel in wheels)
            {
                wheel.SteerAngle = Steer * maxSteer;
                wheel.Torque = Throttle * motorTorque;
            }
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SaveVehicle(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadVehicle();

        Scrap = data.Scrap;
        EngineLevel = data.EngineLevel;
        TiresLevel = data.TiresLevel;
        FuelLevel = data.FuelLevel;
        FrameLevel = data.FrameLevel;
    }
}
