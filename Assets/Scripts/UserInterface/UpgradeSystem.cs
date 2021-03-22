using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    // Player Vehicle
    public Car CurrentVehicle;

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
    public int Scrap;

    private void Start()
    {
        ScrapTxt.text = "Scrap: " + Scrap;
    }

    public void UpdateTextVisual()
    {
        UpgradeLevelList[0].text = CurrentVehicle.EngineLevel +"/5";
        UpgradeLevelList[1].text = CurrentVehicle.TiresLevel + "/5";
        UpgradeLevelList[2].text = CurrentVehicle.FuelLevel + "/5";
        UpgradeLevelList[3].text = CurrentVehicle.FrameLevel + "/5";
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

        CostText.text = "Cost: " + UpgradeCost[UpgradeLevel];
    }

    public void PurchaseUpgrade()
    {
        if(Scrap >= UpgradeCost[UpgradeLevel])
        {
            Scrap -= UpgradeCost[UpgradeLevel];

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
            ScrapTxt.text = "Scrap: " + Scrap;
            CostText.text = "Cost: " + UpgradeCost[UpgradeLevel];
        }
    }
}
