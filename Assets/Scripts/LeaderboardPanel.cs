using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : MonoBehaviour
{
    public List<Text> placeNumbers;

    // Start is called before the first frame update
    private void Start()
    {
        Leaderboard.Reset();
    }

    void LateUpdate()
    {
        List<string> places = Leaderboard.GetPlaces();

        for (int i = 0; i < placeNumbers.Count; i++)
        {
            if (i < places.Count)
            {
                placeNumbers[i].text = places[i];
            }
            else
            {
                placeNumbers[i].text = "";
            }
        }
    }
}
