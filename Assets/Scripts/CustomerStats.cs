using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerStats : MonoBehaviour
{
    private Text speechBubble;
    private string[] greetings = { "Hi there!", "Whatta ya got?", "What're ya sellin'?", "Is this the right place?", "Can I get some service, please?" };
    private string[] boastResponse = { "You don't say!", "I hadn't considered that...", "I'll take your word for it.", "Impressive!", "Wow!" };
    private string[] snackResponse = { "Thank you!", "Thanks!", "For me? Thanks!", "Talk about customer service!", "You have my attention" };
    private string[] appearanceResponse = { "I guess it would have to be the looks?", "Style is everything!", "I want something that looks cool", "Something that'll turn heads", "One that looks as good as I do" };
    private string[] interiorResponse = { "Something that looks good from the inside", "A luxury interior!", "Comfortable seats for long trips", "CUP HOLDERS", "Lots of flashing buttons!" };
    private string[] safteyResponse = { "Something that'll keep my family safe", "Got anything that can blow up a small planet?", "Guns. Lots of them. Don't ask.", "State of the art defense system", "Airbags. Wait, do you need airbags in space?" };
    private string[] speedResponse = { "GOTTA GO FAST", "The fastest ya got", "Speed is key!", "I want to break some speed records", "Something quick would be nice" };
    private string[] sizeResponseSmall = { "Something that doesn't take up too much space", "The smaller the better", "I don't need anything too big", "A smaller one will do", "Itsy bitsy teeny weeny spacey shipy" };
    private string[] sizeResponseRegular = { "Something not too big or too small", "Something sized juuuuuust right", "Average sized would be fine", "Got anything regular sized?", "I'm not looking for anything crazy for size"};
    private string[] sizeResponseLarge = { "Biggest ya got!", "I need something to fit the whole family", "BIG SHIP PLEASE", "I would prefer something on the large side", "Something big enough to fit an asteroid. No reason." };
    private string[] purchaseResponseCheap = { "You're practically giving it away!", "What a steal!", "How do you stay in business with such low prices?!", "Haha, sucker!", "Way less than I was expecting!" };
    private string[] purchaseResponseAverage = { "You got yourself a deal", "Sounds reasonable", "Sure, sounds fair", "A fair price", "I can do that" };
    private string[] purchaseResponseExpensive = { "I can't afford that", "No way, pal", "That's way too expensive", "For that hunk of junk?! No way!", "You're out of your mind!" };

    // Prioritized ship stats are weighted the highest at 5, lowest at 1
    public int appearanceWeight;
    public int interiorWeight;
    public int safetyWeight;
    public int speedWeight;

    //The modifier of the 5 stats
    float appearanceModifier;
    float interiorModifier;
    float safetyModifier;
    float speedModifier;
    float sizeModifier;

    // Ship size preference is given if it falls in the top three, otherwise it is set to Irrelevant
    public int sizeRankNumber;
    public ShipStats.SizeCategory sizePreference;

    // Customer modifiers are distinct from ship modifiers
    public enum CustomerModifier { None }
    public CustomerModifier modifier = CustomerModifier.None;

    private Text patienceText;
    private float patience;

    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        // All weight values are set in the Inspector

        speechBubble = GameObject.Find("SpeechText").GetComponent<Text>();
        speechBubble.text = greetings[Random.Range(0, greetings.Length)];
        manager = FindObjectOfType<GameManager>();
        manager.currentCustomer = this;

        patienceText = GameObject.Find("CustomerPatience").GetComponent<Text>();
        patience = 100.0f;
        patienceText.text = "Customer Patience: " + patience + "%";

        //initialize modifiers
        appearanceModifier = convertWeightToModifier(appearanceWeight);
        interiorModifier = convertWeightToModifier(interiorWeight);
        safetyModifier = convertWeightToModifier(safetyWeight);
        speedModifier = convertWeightToModifier(speedWeight);
        sizeModifier = 1f;
    }

    public float convertWeightToModifier(int weight)
    {
        switch (weight)
        {
            case 1:
                return 1f;
            case 2:
                return 1.1f;
            case 3:
                return 1.3f;
            case 4:
                return 1.6f;
            case 5:
                return 2f;
            case 6:
                return 2.5f;
            default:
                return 1f;
        }
    }


    public void Interview(int preference)
    {
        if (appearanceWeight == preference)
            speechBubble.text = appearanceResponse[Random.Range(0, appearanceResponse.Length)];
        else if (interiorWeight == preference)
            speechBubble.text = interiorResponse[Random.Range(0, interiorResponse.Length)];
        else if (safetyWeight == preference)
            speechBubble.text = safteyResponse[Random.Range(0, safteyResponse.Length)];
        else if (speedWeight == preference)
            speechBubble.text = speedResponse[Random.Range(0, speedResponse.Length)];
        else if (sizeRankNumber == preference)
        {
            switch (sizePreference)
            {
                case ShipStats.SizeCategory.Large:
                    speechBubble.text = sizeResponseLarge[Random.Range(0, sizeResponseLarge.Length)];
                    break;
                case ShipStats.SizeCategory.Regular:
                    speechBubble.text = sizeResponseRegular[Random.Range(0, sizeResponseRegular.Length)];
                    break;
                case ShipStats.SizeCategory.Small:
                    speechBubble.text = sizeResponseSmall[Random.Range(0, sizeResponseSmall.Length)];
                    break;
            }
        }

        UpdatePatience(-10.0f);
    }

    public void Boast(int stat)
    {
        speechBubble.text = boastResponse[Random.Range(0, boastResponse.Length)];

        // MORE PLACEHOLDER MATH. SHOULD CHANGE BASED ON HOW MUCH THEY LIKE THE STAT
        switch (stat)
        {       
            case 1:
                appearanceModifier = convertWeightToModifier(appearanceWeight + 1);
                break;
            case 2:
                interiorModifier = convertWeightToModifier(interiorWeight + 1);
                break;
            case 3:
                safetyModifier = convertWeightToModifier(safetyWeight + 1);
                break;
            case 4:
                speedModifier = convertWeightToModifier(speedWeight + 1);
                break;
            case 5:
                sizeRankNumber += sizeRankNumber;
                break;
               
        }

        UpdatePatience(-1.0f);
    }

    public void OfferSnacks()
    {
        // MORE PLACEHOLDER VALUES
        speechBubble.text = snackResponse[Random.Range(0, snackResponse.Length)];
        UpdatePatience(20.0f);
    }

    public void MakeOffer(float amount, ShipStats ship)
    {
        // THIS IS ALL PLACEHOLDER MATH THAT ALL NEEDS TO BE REPLACED!!!

        //size modifier math
        if (sizePreference != ship.size)
        {
            if (ship.size == ShipStats.SizeCategory.Small)
            {
                if (sizeRankNumber == 1)
                {
                    sizeModifier = 1;
                }
                else if (sizeRankNumber == 2)
                {
                    sizeModifier = 0.98f;
                }
                else if (sizeRankNumber == 3)
                {
                    sizeModifier = 0.96f;
                }
                else if (sizeRankNumber == 4)
                {
                    sizeModifier = 0.93f;
                }
                else if (sizeRankNumber == 5)
                {
                    sizeModifier = 0.90f;
                }
                else if (sizeRankNumber == 6)
                {
                    sizeModifier = 0.85f;
                }
            }
            else if (ship.size == ShipStats.SizeCategory.Regular)
            {
                if (sizeRankNumber == 1)
                {
                    sizeModifier = 1;
                }
                else if (sizeRankNumber == 2)
                {
                    sizeModifier = 0.95f;
                }
                else if (sizeRankNumber == 3)
                {
                    sizeModifier = 0.90f;
                }
                else if (sizeRankNumber == 4)
                {
                    sizeModifier = 0.85f;
                }
                else if (sizeRankNumber == 5)
                {
                    sizeModifier = 0.80f;
                }
                else if (sizeRankNumber == 6)
                {
                    sizeModifier = 0.75f;
                }
            }
            else if (ship.size == ShipStats.SizeCategory.Large)
            {
                if (sizeRankNumber == 1)
                {
                    sizeModifier = 1;
                }
                else if (sizeRankNumber == 2)
                {
                    sizeModifier = 0.90f;
                }
                else if (sizeRankNumber == 3)
                {
                    sizeModifier = 0.70f;
                }
                else if (sizeRankNumber == 4)
                {
                    sizeModifier = 0.40f;
                }
                else if (sizeRankNumber == 5)
                {
                    sizeModifier = 0.20f;
                }
                else if (sizeRankNumber == 6)
                {
                    sizeModifier = 0.10f;
                }
            }

        }
            
       

        //patience f(x)=0.003x+0.8
        //each stats has a value
        float appearanceValue, interiorValue, safetyValue, speedValue;
        appearanceValue = ship.appearance / (ship.appearance + ship.interior + ship.safety + ship.speed) * ship.value;
        interiorValue = ship.interior / (ship.appearance + ship.interior + ship.safety + ship.speed) * ship.value;
        safetyValue = ship.safety / (ship.appearance + ship.interior + ship.safety + ship.speed) * ship.value;
        speedValue = ship.speed / (ship.appearance + ship.interior + ship.safety + ship.speed) * ship.value;

        float maximumOffer = (appearanceValue * appearanceModifier) + (interiorValue * interiorModifier) + (safetyValue * safetyModifier) + (speedValue * speedModifier)
            * (0.003f * patience + 0.8f) * sizeModifier;

        if (amount <= maximumOffer)
        {
            manager.AddIncome(amount);
            if (amount / maximumOffer < 0.85f)
            {
                speechBubble.text = purchaseResponseCheap[Random.Range(0, purchaseResponseCheap.Length)];
                SpawnNextCustomer();
            }
            else
            {
                speechBubble.text = purchaseResponseAverage[Random.Range(0, purchaseResponseAverage.Length)];
                SpawnNextCustomer();
            }
        }
        else 
        {
            if (amount >= maximumOffer && amount < maximumOffer * 1.2f)
                UpdatePatience(-10.0f);
            else if (amount >= maximumOffer * 1.2f && amount < maximumOffer * 1.5f)
                UpdatePatience(-20.0f);
            else if (amount >= maximumOffer * 1.5f && amount < maximumOffer * 2f)
                UpdatePatience(-40.0f);
            else if (amount >= maximumOffer * 2f && amount < maximumOffer * 3f)
                UpdatePatience(-70.0f);
            else if (amount >= maximumOffer * 3f)
                UpdatePatience(-100.0f);
            speechBubble.text = purchaseResponseExpensive[Random.Range(0, purchaseResponseExpensive.Length)];
        }
    }

    private void UpdatePatience(float amount)
    {
        // ALL CALLS TO THIS HAVE PLACEHOLDER VALUES OF -1.0 RIGHT NOW
        patience += amount;
        patience = Mathf.Clamp(patience, 0.0f, 100.0f);
        patienceText.text = "Customer Patience: " + patience + "%";
    }

    private IEnumerator SpawnNextCustomer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            manager.SpawnCustomer();
            Destroy(gameObject);
        }
    }
}
