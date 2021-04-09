using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    // Vehicle Settings
    public Transform centerOfMass;
    public string VehicleName;
    public int VehicleNumber;
    public bool ActivateVehicle = false;
    public bool VehiclePurchased = true;
    public int VehicleCost;
    public int Scrap = 0;

    // Vehicle Stats
    public float KPH;
    public float Health = 100f;
    public float Fuel = 100f;
    public float FuelUsage = 0.25f;
    public float FrameStrength = 1f;
    public float motorTorque = 1500f;
    public float maxSteer = 20f;

    // Upgrade Levels
    public int EngineLevel = 0;
    public int TiresLevel = 0;
    public int FuelLevel = 0;
    public int FrameLevel = 0;

    // Upgrade Values
    public float[] EngineUpgrade = new float[6];
    public float[] SuspensionUpgrade = new float[6];
    public float[] FuelUpgrade = new float[6];
    public float[] FrameUpgrade = new float[6];

    //Vehicle Select Stats
    public float SpeedStat;
    public float HandlingStat;
    public float FuelStat;
    public float FrameStat;

    public float Steer { get; set; }
    public float Throttle { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
        LoadPlayer();
        if (ActivateVehicle)
        {
            AssignUpgrades();
        }
    }

    private void FixedUpdate()
    {
        UpdateVehicleStats();
        if (ActivateVehicle)
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

    void UpdateVehicleStats()
    {
        KPH = _rigidbody.velocity.magnitude;
    }

    void AssignUpgrades()
    {
        motorTorque = EngineUpgrade[EngineLevel];
        FuelUsage = FuelUpgrade[FuelLevel];
        FrameStrength = FrameUpgrade[FrameLevel];
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.suspensionDistance = SuspensionUpgrade[TiresLevel];
        }
    }

    public void SavePlayer()
    {
        //Debug.Log("Saving Player Data...");
        SaveSystem.SavePlayerData(this, false);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null)
        {
            VehiclePurchased = data.VehiclePurchased[VehicleNumber];
            EngineLevel = data.EngineLevel[VehicleNumber];
            TiresLevel = data.TiresLevel[VehicleNumber];
            FuelLevel = data.FuelLevel[VehicleNumber];
            FrameLevel = data.FrameLevel[VehicleNumber];
            Scrap = data.Scrap;
        }
        //else
        //{
        //   // Debug.Log("Failed To Load Data: No Save File Found");
        //}
    }
}
