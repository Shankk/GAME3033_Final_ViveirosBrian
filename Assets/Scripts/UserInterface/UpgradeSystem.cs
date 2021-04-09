using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    // Player Vehicle
    public Vehicle CurrentVehicle;

    //Upgrade Costs
    public int[] UpgradeCost;

    public TMPro.TextMeshProUGUI[] UpgradeLevelList;

    // Upgrade Text Visual
    public TMPro.TextMeshProUGUI UpgradeTarget;
    public TMPro.TextMeshProUGUI UpgradeLevelText;
    public TMPro.TextMeshProUGUI CostText;
    int UpgradeLevel;

    // Scrap and Scrap Text
    public TMPro.TextMeshProUGUI ScrapTxt;

    private void Start()
    {
    
    }

    public void UpdateTextVisual()
    {
        UpgradeLevelList[0].text = CurrentVehicle.EngineLevel + "/5";
        UpgradeLevelList[1].text = CurrentVehicle.TiresLevel + "/5";
        UpgradeLevelList[2].text = CurrentVehicle.FuelLevel + "/5";
        UpgradeLevelList[3].text = CurrentVehicle.FrameLevel + "/5";
        ScrapTxt.text = "Scrap: " + CurrentVehicle.Scrap;
    }

    public void SelectUpgrade(GameObject upgrade)
    {
        UpgradeTarget = upgrade.GetComponentInParent<TMPro.TextMeshProUGUI>();
        UpgradeLevelText = upgrade.GetComponentInChildren<TMPro.TextMeshProUGUI>();
       
            if (UpgradeTarget.text == "Engine")
            {
                UpgradeLevel = CurrentVehicle.EngineLevel;
            }
            else if (UpgradeTarget.text == "Tires")
            {
                UpgradeLevel = CurrentVehicle.TiresLevel;
            }
            else if (UpgradeTarget.text == "Fuel Tank")
            {
                UpgradeLevel = CurrentVehicle.FuelLevel;
            }
            else if (UpgradeTarget.text == "Frame")
            {
                UpgradeLevel = CurrentVehicle.FrameLevel;
            }
            if (UpgradeLevel < UpgradeCost.Length)
            {
                CostText.text = "Cost: " + UpgradeCost[UpgradeLevel];
            }
    }

    public void PurchaseUpgrade()
    {
        if(UpgradeLevel < UpgradeCost.Length  && CurrentVehicle.Scrap >= UpgradeCost[UpgradeLevel])
        {
            CurrentVehicle.Scrap -= UpgradeCost[UpgradeLevel];
            SaveScrap();
            if (UpgradeTarget.text == "Engine")
            {
                CurrentVehicle.EngineLevel++;
            }
            else if (UpgradeTarget.text == "Tires")
            {
                CurrentVehicle.TiresLevel++;
            }
            else if (UpgradeTarget.text == "Fuel Tank")
            {
                CurrentVehicle.FuelLevel++;
            }
            else if (UpgradeTarget.text == "Frame")
            {
                CurrentVehicle.FrameLevel++;
            }
            UpgradeLevel++;
            UpgradeLevelText.text = UpgradeLevel + "/5";
            ScrapTxt.text = "Scrap: " + CurrentVehicle.Scrap;
            CostText.text = "Cost: " + UpgradeCost[UpgradeLevel];
        }
    }

    public void OnEnter(Vehicle car)
    {
        CurrentVehicle = car;
        CurrentVehicle.LoadPlayer();
        UpdateTextVisual();
    }

    public void SaveScrap()
    {
        SaveSystem.SavePlayerData(CurrentVehicle, true);
    }
}
