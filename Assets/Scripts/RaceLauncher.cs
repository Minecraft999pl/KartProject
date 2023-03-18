using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public InputField playerName;
    public Text nameText;
    byte maxPlayerPerRoom = 3;
    bool isConnecting;
    public Text networkText;
    string gameVersion = "2";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PlayerPrefs.HasKey("PlayerName")) playerName.text =
        PlayerPrefs.GetString("PlayerName");
    }

    void Update()
    {
        nameText.text = PlayerPrefs.GetString("PlayerName");
    }

    public void SetName(Text nameText)
    {
        string name = nameText.text;
        PlayerPrefs.SetString("PlayerName", name);
    }
    public void StartTrial()
    {
        SceneManager.LoadScene(1);
    }

    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            networkText.text += "OnConnectedToMaster...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to do join radom room.\n";
        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = this.maxPlayerPerRoom});
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected because " + cause + "\n";
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        networkText.text += "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players.\n";
        PhotonNetwork.LoadLevel("KartTest");
    }

    public void ConnectNetwork()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;
        if(PhotonNetwork.IsConnected)
        {
            networkText.text += "Joining Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting...\n";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}