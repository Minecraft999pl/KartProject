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

    public GameObject carPrefarb;
    public Transform[] spawnPos;
    public int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        Debug.Log("--------------------------------");
        InvokeRepeating("CountDown", 1, 1);

        for(int i = 0; i < playerCount; i++)
        {
            GameObject car = Instantiate(carPrefarb);
            car.transform.position = spawnPos[i].position;
            car.transform.rotation = spawnPos[i].rotation;
            carPrefarb.GetComponent<CarAppearance>().playerNumber = i;
            if(car.GetComponent<CarAppearance>().playerNumber == 0)//remember this line
            {
                car.GetComponent<PlayerController>().enabled = true;
                GameObject.FindObjectOfType<AudioSource>().enabled = true;
                GameObject.FindObjectOfType<CameraController>().SetCameraProperties(car);
            }
        }

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
