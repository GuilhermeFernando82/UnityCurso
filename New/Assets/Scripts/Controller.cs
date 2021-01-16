using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joyvec;
    public Vector2 joytouchpos;
    public Vector2 originPos;
    public float joyRadius;
    public static Controller instance;
    void Start()
    {
        originPos = joystickBG.transform.position;
        joyRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;


    }
    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joytouchpos = Input.mousePosition;
    }
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joyvec = (dragPos - joytouchpos).normalized;
        float JoystickDist = Vector2.Distance(dragPos, joytouchpos);
        if (JoystickDist < joyRadius)
        {
            joystick.transform.position = joytouchpos + joyvec * JoystickDist;
        }
        else
        {
            joystick.transform.position = joytouchpos + joyvec * joyRadius;
        }
    }
    public void PonteirUp()
    {
        joyvec = Vector2.zero;
        joystick.transform.position = originPos;
        joystickBG.transform.position = originPos;
    }

}
