using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }

    public Vehicle Car;
    public GameObject needle;
    private float startPosition = 218f, endPosition = -40f;
    private float desiredPosition;

    public float vehicleSpeed;

    void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
    }

    private void Start()
    {
        Car = GameObject.FindGameObjectWithTag("PlayerCar").GetComponent<Vehicle>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vehicleSpeed = Car.KPH;
        updateNeedle();
    }

    public void updateNeedle()
    {
        desiredPosition = startPosition - endPosition;
        float temp = vehicleSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0,0,(startPosition - temp * desiredPosition));
    }
}
