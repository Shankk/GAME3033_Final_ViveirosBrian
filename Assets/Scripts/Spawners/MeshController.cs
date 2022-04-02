using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MeshController : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public ObjectSpawner spawner;

    private void Awake()
    {
        AssignComponentsForEditor();

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            meshRenderer.enabled = false;
        }
        else
        {
            meshRenderer.enabled = true;
            UpdateMesh(spawner.ObjectToSpawn);
        }
        
    }

    void AssignComponentsForEditor()
    {
        if (GetComponent<ObjectSpawner>() != null)
        {
            spawner = GetComponent<ObjectSpawner>();
            if(GetComponent<MeshFilter>() != null)
            {
                meshFilter = GetComponent<MeshFilter>();
                if (GetComponent<MeshRenderer>() != null)
                {
                    meshRenderer = GetComponent<MeshRenderer>();

                    Debug.Log("Success! All Components Found!");
                }
                else
                {
                    Debug.Log("Failure! No MeshRenderer Component Found!");
                }
            }
            else
            {
                Debug.Log("Failure! No MeshFilter Component Found!");
            }
        }
        else
        {
            Debug.Log("Failure! No Spawner Component Found!");
        }
    }

    void UpdateMesh(SpawnPreset preset)
    {
        if(Application.isPlaying != true && preset != null)
        {
            if (preset.Object.GetComponent<MeshFilter>() != null)
            {
                this.gameObject.name = preset.ObjectName;
                meshFilter.mesh = preset.Object.GetComponent<MeshFilter>().sharedMesh;
            }
            else
            {
                Debug.Log("Cant Change Mesh! Invalid Mesh Filter!");
            }
        }
        else
        {
            Debug.Log("Cant Change Mesh! Invalid Preset!");
        }
    }
}
