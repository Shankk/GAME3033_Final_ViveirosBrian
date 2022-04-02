using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    public GameObject[] CarList = new GameObject[3]; 
    public GameObject PlayerCar;
    public Vehicle CarEngine;
    public GameObject needle;
    public Canvas pause;
    public Canvas gameOver;
    public Transform StartOrigin;

    public TMPro.TextMeshProUGUI TimerUi;
    public TMPro.TextMeshProUGUI DistanceUi;
    public TMPro.TextMeshProUGUI ScrapUi;
    public Slider HealthSlider;
    public Slider FuelSlider;
    public TMPro.TextMeshProUGUI speedometer;
    public TMPro.TextMeshProUGUI gearNum;

    public bool SetToMPH;
    public float vehicleSpeed;
    public float distance;
    float TimerThreshold;
    float Timer = 0f;
    private float startPosition = 218f, endPosition = -40f;
    private float desiredPosition;


    private void Start()
    {
        ChooseVehicle();
        PlayerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        CarEngine = PlayerCar.GetComponent<Vehicle>();
    }

    private void FixedUpdate()
    {
        UpdateTime();
        UpdateDistance();
        UpdatePlayerStats();
        UpdateNeedle();
    }

    void UpdateTime()
    {
        TimerThreshold += Time.deltaTime;
        CarEngine.Fuel -= CarEngine.FuelUsage * Time.deltaTime;
        if(TimerThreshold > 1)
        {
            Timer += 1f;
            TimerThreshold = 0;
            TimerUi.text = "Time: " + Timer;
        }
    }

    void UpdateDistance()
    {
        distance = Mathf.Round(Vector3.Distance(StartOrigin.position, PlayerCar.transform.position));
        DistanceUi.text = "Distance: " + distance;
    }

    public void UpdateNeedle()
    {
        desiredPosition = startPosition - endPosition;
        float temp = vehicleSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPosition));
        vehicleSpeed = SetToMPH ? CarEngine.MPH : CarEngine.KPH;
        speedometer.text = SetToMPH ? vehicleSpeed.ToString("MPH \n\n\n 0") :
            vehicleSpeed.ToString("KPH \n\n\n 0");
    }

    public void changeGear()
    {
        gearNum.text = (CarEngine.gearNum + 1).ToString();
    }

    void UpdatePlayerStats()
    {
        HealthSlider.value = CarEngine.Health / 100;
        FuelSlider.value = CarEngine.Fuel / 100;
        ScrapUi.text = "Scrap: " + CarEngine.Scrap;

        // Lose Condition
        if(CarEngine.Fuel <= 0 || CarEngine.Health <= 0)
        {
            Time.timeScale = 1;
            CarEngine.ActivateVehicle = false;
            CarEngine.Throttle = 0;
            gameOver.enabled = true;
            SaveSystem.SavePlayerData(CarEngine, true);
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
        SaveSystem.SavePlayerData(CarEngine, true);
        SceneManager.LoadScene("Game");
    }

    public void Garage()
    {
        SaveSystem.SavePlayerData(CarEngine, true);
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
