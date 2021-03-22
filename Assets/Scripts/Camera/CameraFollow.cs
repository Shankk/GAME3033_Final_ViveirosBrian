using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 offset;
    public Quaternion rotOffset;
    public float damper;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = rotOffset;
        Target = GameObject.FindGameObjectWithTag("PlayerCar").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, Target.position + offset, damper * Time.deltaTime);
        transform.LookAt(Target);
        //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(Target.rotation.x + rotOffset.x, Target.rotation.y + rotOffset.y, Target.rotation.z + rotOffset.z, Target.rotation.w + rotOffset.w), damper * Time.deltaTime);
    }
}
