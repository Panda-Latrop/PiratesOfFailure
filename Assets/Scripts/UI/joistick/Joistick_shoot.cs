using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Joistick_shoot : MonoBehaviour, IDragHandler ,IPointerDownHandler,IPointerUpHandler {
    public RectTransform space, joiRect;
    public Image bk, stick;
    public Vector2 vectorOfMove;
    public bool shoot;
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
            shoot = true;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(space, eventData.position, Camera.main, out joiPos);
        joiRect.localPosition = joiPos;
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        joiRect.localPosition = new Vector2(500, 0);
        vectorOfMove = Vector2.zero;
        stick.rectTransform.anchoredPosition = Vector2.zero;
        shoot = false;
        groupManager.Instance.unitCurrentLook =0;
        groupManager.Instance.unitLook = groupManager.Instance.unitShoot = false;
    }
    void Update()
    {
      /*  if (Time.timeScale == 0)
        {
            vectorOfMove = Vector2.zero;
            stick.rectTransform.anchoredPosition = Vector2.zero;
        }*/
        if (shoot)
        {
            float angle = Mathf.Atan2(vectorOfMove.y, vectorOfMove.x) * Mathf.Rad2Deg;
            groupManager.Instance.unitCurrentLook = angle;
            groupManager.Instance.unitLook = true;
        }
        if (vectorOfMove.magnitude > 0.25f)
        {
            groupManager.Instance.unitShoot = true;
        }
        else
        {
            groupManager.Instance.unitShoot = false;
        }
       
    }
    public void ResetJoistick()
    {
        joiRect.localPosition = new Vector2(500, 0);
        vectorOfMove = Vector2.zero;
        stick.rectTransform.anchoredPosition = Vector2.zero;
        groupManager.Instance.unitCurrentLook = 0;
        groupManager.Instance.unitLook = groupManager.Instance.unitShoot = false;
    }
}
