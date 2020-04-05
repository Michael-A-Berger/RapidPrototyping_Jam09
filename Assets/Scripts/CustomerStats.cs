using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStats : MonoBehaviour
{
    // Prioritized ship stats are weighted the highest at 5, lowest at 1
    public int appearanceWeight;
    public int interiorWeight;
    public int safetyWeight;
    public int speedWeight;

    // Ship size preference is given if it falls in the top three, otherwise it is set to Irrelevant
    public int sizeRankNumber;
    public ShipStats.SizeCategory sizePreference;

    // Customer modifiers are distinct from ship modifiers
    public enum CustomerModifier { None }
    public CustomerModifier modifier = CustomerModifier.None;

    // Start is called before the first frame update
    void Start()
    {
        // All values are set in the Inspector
    }
}
