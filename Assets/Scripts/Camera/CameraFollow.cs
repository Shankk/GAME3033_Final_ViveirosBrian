using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 offsetBase;
    public Vector3 offsetMax;
    public Quaternion rotOffset;
    public float damper;
    Vector3 CarRotation;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = rotOffset;
        //Target = GameObject.FindGameObjectWithTag("PlayerCar").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("PlayerCar").transform;
        }
        //Debug.Log("Raw Rotation Value: " + Target.eulerAngles.y);


        // Rotate Camera to The Right
        if (Target.eulerAngles.y > 0 && Target.eulerAngles.y < 90)
        {
            CarRotation = new Vector3(Mathf.Lerp(offsetBase.x, -offsetMax.x, Target.eulerAngles.y / 90), offsetBase.y,
                Mathf.Lerp(offsetBase.z, offsetMax.z, Target.eulerAngles.y / 90));

        }
        // Revert Back to Original Position
        else if (Target.eulerAngles.y > 90 && Target.eulerAngles.y < 180)
        {
            CarRotation = new Vector3(Mathf.Lerp(-offsetMax.x, offsetBase.x, (Target.eulerAngles.y - 90) / 90), offsetBase.y,
                Mathf.Lerp(offsetMax.z, offsetBase.z, (Target.eulerAngles.y - 90) / 90));
        }

        // Rotate Camera to The Left
        if (Target.eulerAngles.y < 360 && Target.eulerAngles.y > 270)
        {
            CarRotation = new Vector3(Mathf.Lerp(offsetMax.x, offsetBase.x, (Target.eulerAngles.y - 270) / 90), offsetBase.y,
                Mathf.Lerp(offsetMax.z, offsetBase.z, (Target.eulerAngles.y - 270) / 90));
        }
        // Revert Back to Original Position
        else if (Target.eulerAngles.y < 270 && Target.eulerAngles.y > 180)
        {
            CarRotation = new Vector3(Mathf.Lerp(offsetBase.x, offsetMax.x, (Target.eulerAngles.y - 180) / 90), offsetBase.y,
                Mathf.Lerp(offsetBase.z, offsetMax.z, (Target.eulerAngles.y - 180) / 90));
        }

        transform.position = Vector3.Lerp(transform.position, Target.position + CarRotation, damper * Time.deltaTime);
        transform.LookAt(Target);
        //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(Target.rotation.x + rotOffset.x, Target.rotation.y + rotOffset.y, Target.rotation.z + rotOffset.z, Target.rotation.w + rotOffset.w), damper * Time.deltaTime);
    }
}
