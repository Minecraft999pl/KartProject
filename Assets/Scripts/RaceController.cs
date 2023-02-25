using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;
    public CheckPointController[] carsController;

    public Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;

    public GameObject endPanel;

    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        Debug.Log("--------------------------------");
        audioSource = GetComponent<AudioSource>();
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
                endPanel.SetActive(true);
                racing = false;
            }
        }
    }

    void CountDown()
    {
        startText.gameObject.SetActive(true);
        if(timer != 0)
        {
            Debug.Log("Rozpoczęcie wyścigu za: " + timer);
            startText.text = timer.ToString();
            audioSource.PlayOneShot(count);
            timer--;
        }
        else
        {
            Debug.Log("Start");
            startText.text = "START!";
            audioSource.PlayOneShot(start);
            racing = true;
            CancelInvoke("CountDown");
            Invoke("HideStartText", 1);
        }
    }

    void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
