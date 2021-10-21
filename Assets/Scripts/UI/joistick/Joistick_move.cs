using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Joistick_move : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler {
    public RectTransform space,joiRect;
    public Image bk, stick;
    public Vector2 vectorOfMove;
    Vector2 joiPos;
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bk.rectTransform, eventData.position, Camera.main, out pos))
        {
            pos.x = 2 * (pos.x / bk.rectTransform.sizeDelta.x);
            pos.y = 2* (pos.y / bk.rectTransform.sizeDelta.y);
            vectorOfMove = pos;
            vectorOfMove = (vectorOfMove.magnitude > 1.0f) ? vectorOfMove.normalized : vectorOfMove;
            stick.rectTransform.anchoredPosition = new Vector2(vectorOfMove.x * bk.rectTransform.sizeDelta.x / 2, vectorOfMove.y * bk.rectTransform.sizeDelta.y / 2);
        }
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(space, eventData.position, Camera.main, out joiPos);
        joiRect.localPosition =  joiPos;
        OnDrag(eventData);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        joiRect.localPosition = new Vector2(-500, 0);
        vectorOfMove = Vector2.zero;
        stick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public void Update()
    {
        if ( vectorOfMove == Vector2.zero )
        {
            groupManager.Instance.SetMoveX(0);
            groupManager.Instance.SetJump(3);
        }
        if (vectorOfMove.x > 0.2f)
        {
           groupManager.Instance.SetMoveX(1);
        }
        if (vectorOfMove.x < -0.2f)
        {
            groupManager.Instance.SetMoveX(-1);
        }
        if (vectorOfMove.x >= -0.2f && vectorOfMove.x <= 0.2f && vectorOfMove.y <= 0.4f)
        {
            groupManager.Instance.SetMoveX(0);
        }
        if (vectorOfMove.y > 0.4f)
        {
           groupManager.Instance.SetJump(1);
        }
        if (vectorOfMove.y < -0.5f)
        {
           groupManager.Instance.SetMoveX(0);
           groupManager.Instance.SetJump(2);
        }
    }
    public void ResetJoistick()
    {
        joiRect.localPosition = new Vector2(-500, 0);
        vectorOfMove = Vector2.zero;
        stick.rectTransform.anchoredPosition = Vector2.zero;
        groupManager.Instance.SetMoveX(0);
        groupManager.Instance.SetJump(0);
    }
}
 