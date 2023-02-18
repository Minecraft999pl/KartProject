using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;
    public CheckPointController[] carsController;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("--------------------------------");
        InvokeRepeating("CountDown", 1, 1);
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController =  new CheckPointController[cars.Length];
        for(int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        int finishedLaps = 0;
        foreach(CheckPointController controller in carsController)
        {
            if(controller.lap == totalLaps + 1)
            {
                finishedLaps++;
            }
            if(finishedLaps == carsController.Length && racing)
            {
                Debug.Log("FinishRace");
                racing = false;
            }
        }
    }

    void CountDown()
    {
        if(timer != 0)
        {
            Debug.Log("Rozpoczęcie wyścigu za: " + timer);
            timer--;
        }
        else
        {
            Debug.Log("Start");
            racing = true;
            CancelInvoke("CountDown");
        }
    }
}
