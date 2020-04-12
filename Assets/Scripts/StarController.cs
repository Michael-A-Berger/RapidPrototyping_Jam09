using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public string starName;
   [SerializeField] private int starNum = 0;
    private GameObject Star1;
    private GameObject Star2;
    private GameObject Star3;
    private GameObject Star4;
    private GameObject Star5;

    private void Start()
    {
        Star1 = GameObject.Find("Star1");
        Star2 = GameObject.Find("Star2");
        Star3 = GameObject.Find("Star3");
        Star4 = GameObject.Find("Star4");
        Star5 = GameObject.Find("Star5");
        RefreshStarDisplay();
    }

    public void UpdateStarNum(int currentStars)
    {
        starNum = currentStars;
        RefreshStarDisplay();
    }

    private void RefreshStarDisplay()
    {
        switch (starNum)
        {
            case 1:
                Star1.SetActive(true);
                Star2.SetActive(false);
                Star3.SetActive(false);
                Star4.SetActive(false);
                Star5.SetActive(false);
                break;
            case 2:
                Star1.SetActive(true);
                Star2.SetActive(true);
                Star3.SetActive(false);
                Star4.SetActive(false);
                Star5.SetActive(false);
                break;
            case 3:
                Star1.SetActive(true);
                Star2.SetActive(true);
                Star3.SetActive(true);
                Star4.SetActive(false);
                Star5.SetActive(false);
                break;
            case 4:
                Star1.SetActive(true);
                Star2.SetActive(true);
                Star3.SetActive(true);
                Star4.SetActive(true);
                Star5.SetActive(false);
                break;
            case 5:
                Star1.SetActive(true);
                Star2.SetActive(true);
                Star3.SetActive(true);
                Star4.SetActive(true);
                Star5.SetActive(true);
                break;
            default:
                Star1.SetActive(false);
                Star2.SetActive(false);
                Star3.SetActive(false);
                Star4.SetActive(false);
                Star5.SetActive(false);
                break;
        }
    }
}
