using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string tooltipText;
    public bool hovering;
    private TooltipController tooltip;
    private Canvas parentCanvas;
    private ShipStats attachedShip;

    // Start is called before the first frame update
    void Start()
    {
        tooltip = FindObjectOfType<TooltipController>();
        hovering = false;
        parentCanvas = tooltip.transform.parent.GetComponent<Canvas>();

        attachedShip = GetComponent<ShipStats>();
        if (attachedShip != null)
            tooltipText = GetComponent<ShipStats>().GenerateTooltip();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (hovering)
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);

            Vector3 mousePos = parentCanvas.transform.TransformPoint(movePos);
            mousePos.x += tooltip.tooltipBackground.rect.width / 3.0f;
            mousePos.y -= tooltip.tooltipBackground.rect.height / 2.0f;

            tooltip.transform.position = mousePos;
        }
        else if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<ShipStats>() != null && hit.collider.GetComponent<ShipStats>() == attachedShip)
            {
                Vector2 movePos;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentCanvas.transform as RectTransform,
                    Input.mousePosition, parentCanvas.worldCamera,
                    out movePos);

                Vector3 mousePos = parentCanvas.transform.TransformPoint(movePos);
                mousePos.x += tooltip.tooltipBackground.rect.width / 3.0f;
                mousePos.y -= tooltip.tooltipBackground.rect.height / 2.0f;

                tooltip.transform.position = mousePos;

                tooltip.ShowTooltip(tooltipText, false);
            }
        }
        else if (tooltip.waitingForHover == false)
        {
            tooltip.HideTooltip(true);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hovering = true;
        tooltip.ShowTooltip(tooltipText, true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hovering = false;
        tooltip.HideTooltip(false);
    }
}
