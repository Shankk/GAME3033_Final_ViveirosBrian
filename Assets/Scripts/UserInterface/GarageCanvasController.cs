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
    public VehicleSystem VehicleSys;
    
    public Vector3 GarageVector;
    public Vector3 VehicleVector;
    public Vector3 UpgradeVector;
    public float positionError;
    public float smooth;
    
    private void Start()
    {
        VehicleSys.CarList[VehicleSys.CarCount].GetComponent<Vehicle>().LoadPlayer();   
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
        VehicleSys.CarList[VehicleSys.CarCount].GetComponent<Vehicle>().SavePlayer();
        SceneManager.LoadScene("Game");
    }

    public void UpgradeScreen()
    {
        SetNewScreen(UpgradeCanvas);
        StopAllCoroutines();
        StartCoroutine(SetCamera(UpgradeVector));
        UpgradeCanvas.GetComponent<UpgradeSystem>().OnEnter(VehicleSys.CarList[VehicleSys.CarCount].GetComponent<Vehicle>());
    }

    public void VehicleScreen()
    {
        SetNewScreen(VehicleCanvas);
        StopAllCoroutines();
        StartCoroutine(SetCamera(VehicleVector));
        VehicleSys.AdjustOnEnter();
    }

    public void GarageScreen()
    {
        SetNewScreen(GarageCanvas);
        StopAllCoroutines();
        StartCoroutine(SetCamera(GarageVector));
        VehicleSys.AdjustOnExit();
        VehicleSys.CarList[VehicleSys.CarCount].GetComponent<Vehicle>().SavePlayer();
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
}
