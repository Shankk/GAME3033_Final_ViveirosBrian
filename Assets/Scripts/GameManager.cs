using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }
    public GameCanvasController CanvasController { get; private set; }
    public DifficultyManager DifficultyManager { get; private set; }

    public GameObject Player;
    public Vehicle Car;

    void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
        CanvasController = GetComponentInChildren<GameCanvasController>();
        DifficultyManager = GetComponentInChildren<DifficultyManager>();
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("PlayerCar").gameObject;
            Car = Player.GetComponent<Vehicle>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Player == null)
        //{
        //    Player = GameObject.FindGameObjectWithTag("PlayerCar").gameObject;
        //    Car = Player.GetComponent<Vehicle>();
        //}

        //vehicleSpeed = SetToMPH ? Car.MPH : Car.KPH;
        //speedometer.text = SetToMPH ? vehicleSpeed.ToString("MPH \n\n\n 0") : vehicleSpeed.ToString("KPH \n\n\n 0");

        //updateNeedle(); 
    }

  

}
