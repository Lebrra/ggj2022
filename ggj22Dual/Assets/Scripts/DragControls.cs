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
    float dragScaler = 1F;

    float resetIterator = 0.05F;

    public GameObject board;

    private void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        if (screenSize.y != 0) dragScaler = screenSize.x / screenSize.y;
        else dragScaler = 1;
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

                Debug.Log($"Current Difference: {currDragPos.x - startDragPos.x}, {currDragPos.y - startDragPos.y}");
                Vector2 percentages = new Vector2((currDragPos.x - startDragPos.x) / screenSize.x, (currDragPos.y - startDragPos.y) / screenSize.y /* dragScaler*/);
                Debug.Log($"Percentage Difference: {percentages.x}, {percentages.y}");
                // do fancy dragging math to angle here

                //board.transform.RotateAround(board.transform.position, new Vector3(percentages.y, 0, -percentages.x), 1F);
                //board.transform.Rotate(new Vector3(percentages.y, 0, -percentages.x));
                float asin = Mathf.Asin(percentages.x) * Mathf.Rad2Deg;
                float acos = Mathf.Acos(-percentages.y) * Mathf.Rad2Deg + 90F;
                board.transform.rotation = Quaternion.Euler(new Vector3(acos, 0, asin));
            }
        }
        else
        {
            // slowly reset to original

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
    }
}
