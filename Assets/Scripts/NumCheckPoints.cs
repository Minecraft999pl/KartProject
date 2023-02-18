using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NumCheckPoints : MonoBehaviour
{
    public Transform[] checkpoints;

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = GetComponentsInChildren<Transform>();
        for (int i = 1; i < checkpoints.Length; i++)
        {
            checkpoints[i].gameObject.name = (i - 1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
