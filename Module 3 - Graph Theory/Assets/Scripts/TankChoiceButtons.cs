using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TankChoiceButtons : MonoBehaviour
{
    public List<GameObject> tankControlButtons; 
    void Start()
    {
        //Get control buttons
        foreach (Transform child in transform)
        {
            tankControlButtons.Add(child.gameObject);
        } 
    }
    public void ControlSpecificTank(GameObject controlledTank)
    {
        List<UnityAction> functionButtons = controlledTank.GetComponent<FollowPath>().buttonCommands;
        Color tankColor = controlledTank.GetComponent<FollowPath>().color;

        //Clear all listeners
        for (int i = 0; i < tankControlButtons.Count; i++)
        {
            tankControlButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
        //Fill with new functions and change color
        for (int i = 0; i < tankControlButtons.Count; i++)
        {
            tankControlButtons[i].GetComponent<Image>().color = tankColor;
            tankControlButtons[i].GetComponent<Button>().onClick.AddListener(functionButtons[i]);
        }
    }
}
