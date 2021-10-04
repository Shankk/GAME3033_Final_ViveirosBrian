using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    public GameObject[] CarList = new GameObject[3]; 
    public GameObject PlayerCar;
    public Canvas pause;
    public Canvas gameOver;
    public Transform StartOrigin;

    public TMPro.TextMeshProUGUI TimerUi;
    public TMPro.TextMeshProUGUI DistanceUi;
    public TMPro.TextMeshProUGUI ScrapUi;
    public Slider HealthSlider;
    public Slider FuelSlider;

    float TimerThreshold;
    float Timer = 0f;

    private void Start()
    {
        ChooseVehicle();
        PlayerCar = GameObject.FindGameObjectWithTag("PlayerCar");
    }

    private void FixedUpdate()
    {
        UpdateTime();
        UpdateDistance();
        UpdatePlayerStats();
    }

    void UpdateTime()
    {
        TimerThreshold += Time.deltaTime;
        PlayerCar.GetComponent<Vehicle>().Fuel -= PlayerCar.GetComponent<Vehicle>().FuelUsage * Time.deltaTime;
        if(TimerThreshold > 1)
        {
            Timer += 1f;
            TimerThreshold = 0;
            TimerUi.text = "Time: " + Timer;
        }
    }

    void UpdateDistance()
    {
        var Distance = Vector3.Distance(StartOrigin.position, PlayerCar.transform.position);
        var FinalDistance = Mathf.Round(Distance);
        DistanceUi.text = "Distance: " + FinalDistance;
    }

    void UpdatePlayerStats()
    {
        HealthSlider.value = PlayerCar.GetComponent<Vehicle>().Health / 100;
        FuelSlider.value = PlayerCar.GetComponent<Vehicle>().Fuel / 100;
        ScrapUi.text = "Scrap: " + PlayerCar.GetComponent<Vehicle>().Scrap;

        // Lose Condition
        if(PlayerCar.GetComponent<Vehicle>().Fuel <= 0 || PlayerCar.GetComponent<Vehicle>().Health <= 0)
        {
            Time.timeScale = 1;
            PlayerCar.GetComponent<Vehicle>().ActivateVehicle = false;
            PlayerCar.GetComponent<Vehicle>().Throttle = 0;
            gameOver.enabled = true;
            SaveSystem.SavePlayerData(PlayerCar.GetComponent<Vehicle>(), true);
        }
        
    }

    public void ChooseVehicle()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        CarList[data.SelectedVehicle].gameObject.SetActive(true);
    }

    public void PausedGame()
    {
        Time.timeScale = 0;
        pause.enabled = true;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pause.enabled = false;
    }

    public void RestartGame()
    {
        SaveSystem.SavePlayerData(PlayerCar.GetComponent<Vehicle>(), true);
        SceneManager.LoadScene("Game");
    }

    public void Garage()
    {
        SaveSystem.SavePlayerData(PlayerCar.GetComponent<Vehicle>(), true);
        Time.timeScale = 1;
        SceneManager.LoadScene("Garage");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
