using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageCanvasController : MonoBehaviour
{
    // Canvases
    public Camera MainCamera;
    public Canvas GarageCanvas;
    public Canvas UpgradeCanvas;
    public Canvas VehicleCanvas;

    public UpgradeSystem UpgradeSys;
    public GameObject[] CarList;

    public Vector3 GarageVector;
    public Vector3 VehicleVector;
    public Vector3 UpgradeVector;
    public float positionError;
    public float smooth;
    public int CarCount;

    private void Start()
    {
        CarList[CarCount].GetComponent<Car>().LoadPlayer();
    }

    void SetNewScreen(Canvas Screen)
    {
        GarageCanvas.enabled = Screen.enabled;
        UpgradeCanvas.enabled = Screen.enabled;
        VehicleCanvas.enabled = Screen.enabled;

        if (!Screen.enabled)
        {
            Screen.enabled = true;
        }
    }

    public void StartNewRun()
    {
        SceneManager.LoadScene("Game");
    }

    public void UpgradeScreen()
    {
        SetNewScreen(UpgradeCanvas);
        StopAllCoroutines();
        StartCoroutine(SetCamera(UpgradeVector));
        UpgradeCanvas.GetComponent<UpgradeSystem>().UpdateTextVisual();
    }

    public void VehicleScreen()
    {
        SetNewScreen(VehicleCanvas);
        StopAllCoroutines();
        StartCoroutine(SetCamera(VehicleVector));
    }

    public void GarageScreen()
    {
        SetNewScreen(GarageCanvas);
        StopAllCoroutines();
        StartCoroutine(SetCamera(GarageVector));
        CarList[CarCount].GetComponent<Car>().SavePlayer();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator SetCamera(Vector3 target)
    {
        while (Vector3.Distance(MainCamera.transform.position, target) > positionError)
        {
            //Debug.Log("Camera Distance: " + Vector3.Distance(MainCamera.transform.position, target));
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, target, Time.deltaTime * smooth);
            MainCamera.transform.LookAt(new Vector3(0f, 1f, 0f));
            yield return null;
        }
    }

    public void NextCar()
    {
        if(CarList[CarCount + 1] != null)
        {
            
            CarList[CarCount].SetActive(false);
            CarCount++;
            CarList[CarCount].SetActive(true);
            UpgradeSys.CurrentVehicle = CarList[CarCount].GetComponent<Car>();
        }
    }

    public void PreviousCar()
    {
        if (CarList[CarCount - 1] != null)
        {

            CarList[CarCount].SetActive(false);
            CarCount--;
            CarList[CarCount].SetActive(true);
            UpgradeSys.CurrentVehicle = CarList[CarCount].GetComponent<Car>();
        }
    }
}
