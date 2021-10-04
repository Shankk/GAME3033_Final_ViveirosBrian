using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    // Vehicle Settings
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField]private driveType drive;

    internal enum gearBox
    {
        automatic,
        manual
    }
    [SerializeField] private gearBox gearChange;

    public Transform centerOfMass;
    public string VehicleName;
    public int VehicleNumber;
    public bool ActivateVehicle = false;
    public bool VehiclePurchased = true;
    private bool AreUpgradesAssigned = false;
    public int VehicleCost;
    public int Scrap = 0;

    // Vehicle Stats
    [Header("Vehicle Engine Stats")]
    public AnimationCurve enginePower;
    public float totalPower;
    public float KPH;
    public float engineRPM;
    public float wheelsRPM;
    public float maxRPM, minRPM;
    public float smoothTime = 0.01f;
    public float[] gears;
    public int gearNum = 0;

    [Header("Vehicle Stats")]
    public float Health = 100f;
    public float Fuel = 100f;
    public float FuelUsage = 0.25f;
    public float FrameStrength = 1f;
    public float maxSteer = 20f;

    // Upgrade Levels
    public int EngineLevel = 0;
    public int TiresLevel = 0;
    public int FuelLevel = 0;
    public int FrameLevel = 0;

    // Upgrade Values
    public AnimationCurve[] EngineUpgrade = new AnimationCurve[6];
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
    public float Gear { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
        LoadPlayer();
        AssignDriveType();
        //GameManager.Instance.InputController.gearUp.performed += _ => ShiftUp();
        //GameManager.Instance.InputController.gearDown.performed += _ => ShiftDown();
    }

    private void FixedUpdate()
    {
        if (ActivateVehicle)
        {
            if (wheels != null && AreUpgradesAssigned == false)
            {
                AssignUpgrades();
                AreUpgradesAssigned = true;
            }
            UpdateVehicleStats();
            calculateEnginePower();
            gearShifter();
        }
    }

    void AssignDriveType()
    {
        if(drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < 4; i++)
            {
                wheels[i].power = true;
            }
        }
        else if(drive == driveType.rearWheelDrive)
        {
            for (int i = 0; i < 4; i++)
            {
                if(i < 2)
                    wheels[i].power = false;
                if(i > 1)
                    wheels[i].power = true;
            }
        }
        else if(drive == driveType.frontWheelDrive)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                    wheels[i].power = true;
                if (i > 1)
                    wheels[i].power = false;
            }
        }
        else // If all else fails, resort to Rear Wheel Drive
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < 3)
                    wheels[i].power = false;
                if (i > 2)
                    wheels[i].power = true;
            }

        }
    }

    void DriveVehicle()
    {
        if (ActivateVehicle)
        {
            Steer = GameManager.Instance.InputController.SteerInput;
            Throttle = GameManager.Instance.InputController.ThrottleInput;
        }

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer; //* maxSteer;
            wheel.Torque = totalPower; //Throttle * motorTorque;
        }
     
    }

    private void calculateEnginePower()
    {
        wheelRPM();
        totalPower = enginePower.Evaluate(engineRPM) * (gears[gearNum]) * Throttle;
        float velocity = 0.0f;
        engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);

        DriveVehicle();
    }

    private void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheels[i].wheelCollider.rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;
    }

    private void gearShifter()
    {
        // Automatic Gear Box
        if(gearChange == gearBox.automatic)
        {
            if (engineRPM > maxRPM && gearNum < gears.Length - 1)
            {
                gearNum++;
                GameManager.Instance.changeGear();
            }
        }

        if(engineRPM < minRPM && gearNum > 0)
        {
            gearNum--;
            GameManager.Instance.changeGear();
        }
    }
    void UpdateVehicleStats()
    {
        KPH = _rigidbody.velocity.magnitude;
    }

    void AssignUpgrades()
    {
        enginePower = EngineUpgrade[EngineLevel];
        FuelUsage = FuelUpgrade[FuelLevel];
        FrameStrength = FrameUpgrade[FrameLevel];
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.suspensionDistance = SuspensionUpgrade[TiresLevel];
        }
        //Debug.Log("Assigned Vehicle Upgrades...");
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
