using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }

    public GameObject Player;
    public Vehicle Car;
    public GameObject needle;
    public TMPro.TextMeshProUGUI kph;
    public TMPro.TextMeshProUGUI gearNum;
    private float startPosition = 218f, endPosition = -40f;
    private float desiredPosition;

    public float vehicleSpeed;

    void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
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
        vehicleSpeed = Car.KPH;
        kph.text = vehicleSpeed.ToString("KPH \n\n\n 0");
        updateNeedle(); 
    }

    public void updateNeedle()
    {
        desiredPosition = startPosition - endPosition;
        float temp = vehicleSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0,0,(startPosition - temp * desiredPosition));
    }

    public void changeGear()
    {
        gearNum.text = (Car.gearNum + 1).ToString();
    }
}
