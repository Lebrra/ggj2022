using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragControls : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    bool isDragging = false;
    Vector2 startDragPos;
    Vector2 currDragPos;

    Vector2 screenSize;

    int resetIterator = 15; //how many iterations should it take to reset pos?
    Coroutine currentResettor = null;

    public GameObject board;

    UnityEngine.UI.Image myInteration;

    private void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = Input.mousePosition;
            if(currDragPos != mousePos)
            {
                // position changed, update direction
                currDragPos = Input.mousePosition;

                //Debug.Log($"Current Difference: {currDragPos.x - startDragPos.x}, {currDragPos.y - startDragPos.y}");
                Vector2 percentages = new Vector2(Mathf.Clamp((currDragPos.x - startDragPos.x) / screenSize.x, -0.2F, 0.2F), Mathf.Clamp((currDragPos.y - startDragPos.y) / screenSize.y, -0.2F, 0.2F));
                //Debug.Log($"Percentage Difference: {percentages.x}, {percentages.y}");
                // do fancy dragging math to angle here

                float asin = Mathf.Asin(-percentages.x) * Mathf.Rad2Deg;
                float acos = Mathf.Acos(-percentages.y) * Mathf.Rad2Deg - 90F;
                board.transform.rotation = Quaternion.Euler(new Vector3(acos, 0, asin));
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentResettor != null)
        {
            StopCoroutine(currentResettor);
            currentResettor = null;
        }
        startDragPos = Input.mousePosition;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // taking this out breaks it for some reason
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        //reset board position
        Vector2 percentages = new Vector2(Mathf.Clamp((currDragPos.x - startDragPos.x) / screenSize.x, -0.2F, 0.2F), Mathf.Clamp((currDragPos.y - startDragPos.y) / screenSize.y, -0.2F, 0.2F));
        currentResettor = StartCoroutine(BoardReset(percentages / (float)resetIterator, resetIterator));
    }

    IEnumerator BoardReset(Vector2 ratios, int iteration)
    {
        iteration--;
        if (iteration <= 0)
        {
            // reset and end
            board.transform.rotation = Quaternion.Euler(Vector3.zero);
            yield return null;
        }
        else
        {
            float asin = Mathf.Asin(-ratios.x * (float)iteration) * Mathf.Rad2Deg;
            float acos = Mathf.Acos(-ratios.y * (float)iteration) * Mathf.Rad2Deg - 90F;
            board.transform.rotation = Quaternion.Euler(new Vector3(acos, 0, asin));

            yield return new WaitForSecondsRealtime(0.02F);
            currentResettor = StartCoroutine(BoardReset(ratios, iteration));
        }
    }

    public void Controls(bool enabled)
    {
        if (!myInteration)
        {
            myInteration = GetComponent<UnityEngine.UI.Image>();
        }
        myInteration.enabled = enabled;
    }
}
