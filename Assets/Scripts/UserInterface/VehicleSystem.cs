using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSystem : MonoBehaviour
{
    public Vehicle CurrentVehicle;
    public GameObject[] CarList;
    public int CarCount;
    public int CarCycle = 0;

    public GameObject Select;
    public GameObject Unlock;
    public TMPro.TextMeshProUGUI Cost;
    public TMPro.TextMeshProUGUI ScrapTxt;

    // Vehicle Info
    public TMPro.TextMeshProUGUI Name;
    public Slider Speed;
    public Slider Handling;
    public Slider Fuel;
    public Slider Frame;

    // Start is called before the first frame update
    void Start()
    {
        CurrentVehicle = CarList[CarCount].GetComponent<Vehicle>();
    }
    
    public void SelectCar()
    {
        CarCount = CarCycle;
        CarList[CarCount].GetComponent<Vehicle>().LoadPlayer();

    }
     
    public void UnlockCar()
    {
        if(CurrentVehicle.Scrap >= CurrentVehicle.VehicleCost)
        {
            CurrentVehicle.Scrap -= CurrentVehicle.VehicleCost;
            CurrentVehicle.VehiclePurchased = true;
            SaveSystem.SavePlayerData(CurrentVehicle, true);
            UpdateVehicleInfo();
        }
    }

    public void AdjustOnEnter()
    {
        CarCycle = CarCount;
        UpdateVehicleInfo();
    }

    public void AdjustOnExit()
    {
        for(int i = 0; i < CarList.Length; i++)
        {
            if(i == CarCount)
            {
                CarList[i].SetActive(true);
            }
            else
            {
                CarList[i].SetActive(false);
            }
        }
    }

    void UpdateVehicleInfo()
    {
        Name.text = CurrentVehicle.VehicleName;
        Speed.value = CurrentVehicle.SpeedStat;
        Handling.value = CurrentVehicle.HandlingStat;
        Fuel.value = CurrentVehicle.FuelStat;
        Frame.value = CurrentVehicle.FrameStat;
        CurrentVehicle.LoadPlayer();
        ScrapTxt.text = "Scrap: " + CurrentVehicle.Scrap;
        if (CurrentVehicle.VehiclePurchased)
        {
            Select.SetActive(true);
            Unlock.SetActive(false);
            
        }
        else
        {
            Select.SetActive(false);
            Unlock.SetActive(true);
            Cost.text = "Cost: " + CurrentVehicle.VehicleCost;
        }
    }

    public void NextCar()
    {
        if ((CarCycle + 1) < CarList.Length && CarList[CarCycle + 1] != null)
        {
            
            CarList[CarCycle].SetActive(false);
            CarCycle++;
            CarList[CarCycle].SetActive(true);
            CurrentVehicle = CarList[CarCycle].GetComponent<Vehicle>();
            UpdateVehicleInfo();
        }
    }

    public void PreviousCar()
    {
        if ((CarCycle - 1) > -1 && CarList[CarCycle - 1] != null)
        {
            
            CarList[CarCycle].SetActive(false);
            CarCycle--;
            CarList[CarCycle].SetActive(true);
            CurrentVehicle = CarList[CarCycle].GetComponent<Vehicle>();
            UpdateVehicleInfo();
            
        }
    }
}
