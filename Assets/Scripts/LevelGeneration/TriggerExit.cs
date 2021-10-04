using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    CameraFollow MainCamera;
    public float delay = 5f;
    public Vector2 cameraOffset;

    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    private bool exited = false;

    private void Awake()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    private void OnTriggerExit(Collider other)
    {
        var carTag = other.tag;
        //Debug.Log("Exiting...  Tag: " + carTag);
        if (carTag == "CarCollider")
        {
            if (!exited)
            {
                exited = true;
                MainCamera.offset.x += cameraOffset.x;
                MainCamera.offset.z += cameraOffset.y;
                OnChunkExited();
                StartCoroutine(WaitAndDeactivate());
            }


        }
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);

        Destroy(transform.root.gameObject);
       
    }
}
