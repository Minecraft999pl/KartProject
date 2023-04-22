using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class RaceController : MonoBehaviourPunCallbacks
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

    public GameObject startRace;
    public GameObject waitingText;

    public RawImage mirror;

    // Start is called before the first frame update
    void Awake()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);

        startRace.SetActive(true);
        waitingText.SetActive(true);

        int randomStartPosition = Random.Range(0, spawnPos.Length);
        Vector3 startPos = spawnPos[randomStartPosition].position;
        Quaternion startRot = spawnPos[randomStartPosition].rotation;
        GameObject playerCar = null;

        // for(int i = 0; i < playerCount; i++)
        // {
        //     GameObject car = Instantiate(carPrefarb);
        //     car.transform.position = spawnPos[i].position;
        //     car.transform.rotation = spawnPos[i].rotation;
        //     carPrefarb.GetComponent<CarAppearance>().playerNumber = i;

        //     if(car.GetComponent<CarAppearance>().playerNumber == 0)//remember this line
        //     {
        //         car.GetComponent<PlayerController>().enabled = true;
        //         GameObject.FindObjectOfType<AudioSource>().enabled = true;
        //         GameObject.FindObjectOfType<CameraController>().SetCameraProperties(car);
        //     }
        // }

        // GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        // carsController =  new CheckPointController[cars.Length];
        // for(int i = 0; i < cars.Length; i++)
        // {
        //     carsController[i] = cars[i].GetComponent<CheckPointController>();
        // }

        if(PhotonNetwork.IsConnected)
        {
            startPos = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1].position;
            startRot = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;

            object[] instanceData = new object[4];
            instanceData[0] = (string)PlayerPrefs.GetString("PlayerName");
            instanceData[1] = PlayerPrefs.GetString("Red");
            instanceData[2] = PlayerPrefs.GetString("Green");
            instanceData[3] = PlayerPrefs.GetString("Blue");

            if(OnlinePlayer.LocalPlayerInstance == null)
            {
                playerCar = PhotonNetwork.Instantiate(carPrefarb.name, startPos, startRot, 0, instanceData);
                playerCar.GetComponent<CarAppearance>().SetLocalPlayer();
            }

            if (PhotonNetwork.IsMasterClient)
            {
                waitingText.SetActive(true);
                startRace.SetActive(true);
            }
            else
            {
                waitingText.SetActive(true);
            }
        }

        playerCar.GetComponent<DrivingScript>().enabled = true;
        playerCar.GetComponent<PlayerController>().enabled = true;
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

    [PunRPC]
    public void StartGame()
    {
        InvokeRepeating("CountDown", 1, 1);
        startRace.SetActive(false);
        waitingText.SetActive(false);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    public void BeginGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartGame", RpcTarget.All, null);
        }
    }

    public void SetMirror(Camera backCamera)
    {
        mirror.texture = backCamera.targetTexture;
    }
}
