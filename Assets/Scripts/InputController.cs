using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputAction inputThrottleAxis;
    public InputAction inputSteerAxis;

    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }

    private void OnEnable()
    {
        inputSteerAxis.Enable();
        inputThrottleAxis.Enable();
    }

    private void OnDisable()
    {
        inputSteerAxis.Disable();
        inputThrottleAxis.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        SteerInput = inputSteerAxis.ReadValue<float>();
        ThrottleInput = inputThrottleAxis.ReadValue<float>();
    }
}
