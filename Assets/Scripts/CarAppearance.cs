using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarAppearance : MonoBehaviour
{
    public string playerName;
    public Color carColor;
    public Text nameText;
    public Renderer carRenderer;
    public int playerNumber;
    public ColorCar colorCar;

    // Start is called before the first frame update
    void Start()
    {
        if(playerNumber == 0)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            carColor = ColorCar.IntToColor(PlayerPrefs.GetInt("Red"),
            
            PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));
        }
        else
        {
            playerName = "Random " + playerNumber;
            carColor = new Color(Random.Range(0f,255f) / 255, Random.Range(0f, 255f) / 255,

            Random.Range(0f, 255f) / 255);
        }
        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
