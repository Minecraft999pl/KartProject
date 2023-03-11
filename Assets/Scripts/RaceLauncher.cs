using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RaceLauncher : MonoBehaviour
{
    public InputField playerName;
    public Text nameText;

    void Start()
    {
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
        SceneManager.LoadScene(0);
    }
}