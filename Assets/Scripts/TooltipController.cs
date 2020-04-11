using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    private Text tooltipText;
    public RectTransform tooltipBackground;
    public bool waitingForHover;

    private void Awake()
    {
        tooltipText = transform.Find("TooltipText").GetComponent<Text>();
        tooltipBackground = transform.Find("Background").GetComponent<RectTransform>();

        waitingForHover = false;
    }

    public void ShowTooltip(string tooltip, bool waitForHover)
    {
        waitingForHover = waitForHover;

        gameObject.SetActive(true);

        tooltipText.text = tooltip;

        float textPadding = 4.0f;
        Vector2 backgroundSize = new Vector2(tooltipText.GetComponent<RectTransform>().rect.width + textPadding * 2.0f, tooltipText.preferredHeight + textPadding * 2.0f);
        tooltipBackground.sizeDelta = backgroundSize;
        
    }

    public void HideTooltip(bool doneWaiting)
    {
            waitingForHover = doneWaiting;
            gameObject.SetActive(false);    
    }
}
