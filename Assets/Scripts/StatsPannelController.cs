using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPannelController : MonoBehaviour
{
    private StarController appearanceStars;
    private StarController interiorStars;
    private StarController saftyStars;
    private StarController speedStars;

    private Text modelTextBox;
    private Text sizeTextBox;
    private Text priceTextBox;

    // Start is called before the first frame update
    void Start()
    {
        appearanceStars = GameObject.Find("AppearenceStars").GetComponent<StarController>();
        interiorStars = GameObject.Find("InteriorStars").GetComponent<StarController>();
        saftyStars = GameObject.Find("SaftyStars").GetComponent<StarController>();
        speedStars = GameObject.Find("SpeedStars").GetComponent<StarController>();

        modelTextBox = GameObject.Find("ModelText").GetComponent<Text>();
        sizeTextBox = GameObject.Find("SizeText").GetComponent<Text>();
        priceTextBox = GameObject.Find("ShipPrice").GetComponent<Text>();
    }

    public void UpdateStats(string model, string size, int appearanceVal, int interiorVal, int saftyVal, int speedVal, int shipPrice)
    {

        appearanceStars.UpdateStarNum(appearanceVal);
        interiorStars.UpdateStarNum(interiorVal);
        saftyStars.UpdateStarNum(saftyVal);
        speedStars.UpdateStarNum(speedVal);

        modelTextBox.text = model;
        sizeTextBox.text = size;
        priceTextBox.text = shipPrice.ToString();
    }
}
