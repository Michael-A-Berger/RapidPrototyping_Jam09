using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    // List of prefabs used
    private GameObject[] shipPrefabs, customerPrefabs;
    // List of data for ships
    private List<ShipStats> ships;
    // Initial value for previous customer, set to -1 since the first current customer is at 0
    private int previousCustomerIndex = -1;
    // Customer have 5 preference and are ranked from 1 to 5 where 5 is the most important
    // When customer is first interviewed, they will start by saying the most important thing to them and time customer is interviewed again they will say the next most important thing
    // currentInterviewRank represent current 
    private int currentInterviewRank;

    private StatsPannelController statsPannelController;

    // UI used to display available dealer actions remaining
    private Text actionsText;
    // Current count of actions left, an int between 0 and 5, starting at 5 with each new customer
    private int dealerActions;
    private int maxActions = 5;

    public static GameManager instance;

    public ShipStats currentShip;
    public CustomerStats currentCustomer;

    public float finalBuyingPrice;

    public Button inter;
    public Button boa;
    public Button sna;
    public Button offer;
    public GameObject BoastPanel;

    public Text incomeText;
    public Text totalIncomeText;
    private float income;
    private float totalShipValue;
    private Text feedbackText;

    private AudioManager audioMng = null;

    public int currentSelectedShipIndex = -1;

    void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    void Start()
    {
        income = 0.0f;
        totalShipValue = 0.0f;
        incomeText = GameObject.Find("CurentIncome").GetComponent<Text>();
        totalIncomeText = GameObject.Find("TotalIncome").GetComponent<Text>();
        actionsText = GameObject.Find("DealerActions").GetComponent<Text>();
        dealerActions = maxActions;
        actionsText.text = "Dealer Actions: " + dealerActions + "/5";
        statsPannelController = FindObjectOfType<StatsPannelController>();
        ships = new List<ShipStats>();
        feedbackText = GameObject.Find("FeedbackLine").GetComponent<Text>();
        feedbackText.text = "";
        audioMng = FindObjectOfType<AudioManager>();
        if (audioMng == null)
            Debug.LogError("\tNo GameObject with the [ AudioManager ] script was found in the current scene!");

        SpawnShips();
        SpawnCustomer();
        BoastPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interview()
    {
        currentCustomer.Interview(currentInterviewRank);
        currentInterviewRank--;

        if (currentInterviewRank == 2)
            GameObject.Find("Interview").GetComponent<Button>().interactable = false;

        DealerActionCountdown();
    }

    public void ActivateBoastPanel()
    {
        BoastPanel.SetActive(true);
    }

    public void Boast(int stat)
    {
        currentCustomer.Boast(stat);
        BoastPanel.SetActive(false);

        DealerActionCountdown();
    }

    public void Snacks()
    {
        currentCustomer.OfferSnacks();
        GameObject.Find("Snacks").GetComponent<Button>().interactable = false;

        DealerActionCountdown();
    }

    public void Offer()
    {
        // THIS IS A PLACEHOLDER AND DOES NOT ALLOW FOR PLAYER CHOICE
        float amount = 1000;
        ShipStats ship = currentShip;
        currentCustomer.MakeOffer(amount, ship);

        DealerActionCountdown();
    }

    private void SpawnShips()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("ShipSpawnPoint");
        //Transform mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
        HashSet<int> exclude = new HashSet<int>();

        foreach (GameObject spawn in spawnPoints)
        {
            IEnumerable<int> range = Enumerable.Range(0, shipPrefabs.Length).Where(i => !exclude.Contains(i));

            int randomIndex = Random.Range(0, shipPrefabs.Length - exclude.Count);
            int shipIndex = range.ElementAt(randomIndex);

            GameObject spawnedShip = Instantiate(shipPrefabs[shipIndex], spawn.transform.position, Quaternion.identity);
            exclude.Add(shipIndex);

            spawnedShip.transform.localScale = new Vector3(spawnedShip.transform.localScale.x * 45f, spawnedShip.transform.localScale.y * 45f, 1f);
            spawnedShip.transform.SetParent(spawn.transform.parent);

            totalShipValue += spawnedShip.GetComponent<ShipStats>().value;
        }

        AddIncome(0.0f);
        totalIncomeText.text = "/ " + totalShipValue;
    }

    public void SpawnCustomer()
    {
        currentInterviewRank = 5;
        Vector3 spawnPoint = GameObject.FindGameObjectWithTag("CustomerSpawnPoint").transform.position;
        Transform mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;

        if (previousCustomerIndex == -1)
        {
            int randomIndex = Random.Range(0, customerPrefabs.Length);
            GameObject spawnedCustomer = Instantiate(customerPrefabs[randomIndex], spawnPoint, Quaternion.identity);
            spawnedCustomer.transform.localScale = new Vector3(spawnedCustomer.transform.localScale.x * 45f, spawnedCustomer.transform.localScale.y * 45f, 1f);
            spawnedCustomer.transform.SetParent(mainCanvas);
        }
        else
        {
            HashSet<int> exclude = new HashSet<int>() { previousCustomerIndex };
            IEnumerable<int> range = Enumerable.Range(0, shipPrefabs.Length).Where(i => !exclude.Contains(i));

            int randomIndex = Random.Range(0, customerPrefabs.Length - exclude.Count);
            int customerIndex = range.ElementAt(randomIndex);

            GameObject spawnedCustomer = Instantiate(customerPrefabs[customerIndex], spawnPoint, Quaternion.identity);
            spawnedCustomer.transform.localScale = new Vector3(spawnedCustomer.transform.localScale.x * 45f, spawnedCustomer.transform.localScale.y * 45f, 1f);
            spawnedCustomer.transform.SetParent(mainCanvas);
            previousCustomerIndex = customerIndex;
        }

        GameObject.Find("Interview").GetComponent<Button>().interactable = true;          //Refresh buttons
        GameObject.Find("Boast").GetComponent<Button>().interactable = true;
        GameObject.Find("Snacks").GetComponent<Button>().interactable = true;
        GameObject.Find("Offer").GetComponent<Button>().interactable = true;

        dealerActions = maxActions;
        actionsText.text = "Dealer Actions: " + dealerActions + "/5";
        feedbackText.text = "";

        audioMng.PlayAudio("Customer Arrives");
    }

    public void AddIncome(float amount)
    {
        income += amount;
        incomeText.text = income.ToString();
        int randomSound = Random.Range(1, 4);
        audioMng.PlayAudio("Spaceship Sold " + randomSound);
    }

    //private void ShowToolTip(string toolTip)
    //{

    //}

    public void GetCurrentShip(ShipStats selectedShip, Transform parent)
    {
        statsPannelController.UpdateStats(selectedShip.model, selectedShip.size.ToString(), selectedShip.appearance, selectedShip.interior, selectedShip.safety, selectedShip.speed, Mathf.FloorToInt(selectedShip.value));
        currentShip = selectedShip;
        ActivateCurrentShipDock(parent.Find("Dock"));
    }

    private void ActivateCurrentShipDock(Transform currentDock)
    {
        GameObject[] docks = GameObject.FindGameObjectsWithTag("ShipDock");
        foreach (GameObject dock in docks)
        {
            dock.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        currentDock.GetComponent<Image>().color = new Color32(22, 103, 222, 255);
    }

    private void DealerActionCountdown()
    {
        dealerActions--;
        actionsText.text = "Dealer Actions: " + dealerActions + "/5";
        if (dealerActions == 0)
        {
            StartCoroutine("WaitForTextBeforeEndOfCustomer");
        }
    }

    private IEnumerator WaitForTextBeforeEndOfCustomer()
    {
        // The player has 2 seconds to read whatever text was recently displayed, then another customer will be spawned after another 2 seconds
        GameObject.Find("Interview").GetComponent<Button>().interactable = false;          //block buttons
        GameObject.Find("Boast").GetComponent<Button>().interactable = false;
        GameObject.Find("Snacks").GetComponent<Button>().interactable = false;
        GameObject.Find("Offer").GetComponent<Button>().interactable = false;

        bool stillWaiting = true;

        while (stillWaiting)
        {
            yield return new WaitForSeconds(2.0f);
            currentCustomer.UpdatePatience(-100f);
            stillWaiting = false;
        }
    }
}
