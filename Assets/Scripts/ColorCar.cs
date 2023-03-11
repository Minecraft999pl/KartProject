using UnityEngine;
using UnityEngine.UI;
public class ColorCar : MonoBehaviour
{
    public Renderer rend;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Text redSliderText;
    public Text greenSliderText;
    public Text blueSliderText;
    public Text nameText;
    public Color col;
    void Start()
    {
        col = IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"),
        PlayerPrefs.GetInt("Blue"));
        rend.material.color = col;
        redSlider.value = (int)(col.r * 255f);
        greenSlider.value = (int)(col.g * 255f);
        blueSlider.value = (int)(col.b * 255f);
    }
    void Update()
    {
        SetCarColor((int)redSlider.value, (int)greenSlider.value, (int)blueSlider.value);
        redSliderText.text = redSlider.value.ToString();
        greenSliderText.text = greenSlider.value.ToString();
        blueSliderText.text = blueSlider.value.ToString();
        nameText.color = col;
    }
    public static Color IntToColor(int red, int green, int blue)
    {
        float r = (float)red / 255;
        float g = (float)green / 255;
        float b = (float)blue / 255;
        Color col = new Color(r, g, b);
        return col;
    }
    void SetCarColor(int red, int green, int blue)
    {
        Color col = IntToColor(red, green, blue);
        rend.material.color = col;
        PlayerPrefs.SetInt("Red", red);
        PlayerPrefs.SetInt("Green", green);
        PlayerPrefs.SetInt("Blue", blue);
    }
}