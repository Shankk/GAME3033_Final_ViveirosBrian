using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f;

    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    private bool exited = false;

    private void Awake()
    {
        
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
                OnChunkExited();
                StartCoroutine(WaitAndDeactivate());
            }

            GameManager.Instance.DifficultyManager.CalculateGameDifficulty();
        }

    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);

        Destroy(transform.root.gameObject);
       
    }
}
