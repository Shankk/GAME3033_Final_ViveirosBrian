using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    public Canvas pause;
    public Canvas gameOver;
    public GameObject PlayerCar;
    public Transform StartOrigin;

    public TMPro.TextMeshProUGUI TimerUi;
    public TMPro.TextMeshProUGUI DistanceUi;
    public TMPro.TextMeshProUGUI ScrapUi;
    public Slider HealthSlider;
    public Slider FuelSlider;

    float TimerThreshold;
    float Timer = 0f;

    private void FixedUpdate()
    {
        UpdateTime();
        UpdateDistance();
        UpdatePlayerStats();
    }

    void UpdateTime()
    {
        TimerThreshold += Time.deltaTime;
        PlayerCar.GetComponent<Car>().Fuel -= PlayerCar.GetComponent<Car>().FuelUsage * Time.deltaTime;
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
        HealthSlider.value = PlayerCar.GetComponent<Car>().Health / 100;
        FuelSlider.value = PlayerCar.GetComponent<Car>().Fuel / 100;
        ScrapUi.text = "Scrap: " + PlayerCar.GetComponent<Car>().Scrap;
        if(PlayerCar.GetComponent<Car>().Fuel <= 0 || PlayerCar.GetComponent<Car>().Health <= 0)
        {
            Time.timeScale = 0;
            //PlayerCar.GetComponent<Car>().isCarWorking = false;
            gameOver.enabled = true;
        }
        
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
        //PlayerCar.GetComponent<Car>().isCarWorking = true;
        SceneManager.LoadScene("Game");
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
