using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    //[SerializeField] private string starName;
    private int starNum = 0;

    private void Start()
    {
        RefreshStarDisplay();
    }

    public void UpdateStarNum(int currentStars)
    {
        starNum = currentStars;
        RefreshStarDisplay();
    }

    private void RefreshStarDisplay()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < starNum; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
