using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSelectorIndicator : MonoBehaviour
{
    public enum WaitDirection
    {
        FRONT,
        RIGHT,
        BACK,
        LEFT
    }

    GameObject[] indicators;
    void Start()
    {
        indicators = GameObject.FindGameObjectsWithTag("wait indicator");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = TurnManager.instance.GetUnitWithTurn().transform.position;
        pos.y += 2;
        transform.position = pos;

        SetIndicators();
        CheckUnitHeading();



    }

    void SetIndicators()
    {
        if (TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().GetFacingDirection().z == 1)
        {
            indicators[0].SetActive(true);
            indicators[1].SetActive(false);
            indicators[2].SetActive(false);
            indicators[3].SetActive(false);
        }

        else if (TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().GetFacingDirection().x == 1)
        {
            indicators[0].SetActive(false);
            indicators[1].SetActive(true);
            indicators[2].SetActive(false);
            indicators[3].SetActive(false);
        }

        else if (TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().GetFacingDirection().z == -1)
        {
            indicators[0].SetActive(false);
            indicators[1].SetActive(false);
            indicators[2].SetActive(true);
            indicators[3].SetActive(false);
        }

        else if (TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().GetFacingDirection().x == -1)
        {
            indicators[0].SetActive(false);
            indicators[1].SetActive(false);
            indicators[2].SetActive(false);
            indicators[3].SetActive(true);
        }

    }

    void CheckUnitHeading()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 dir = new Vector3(0, 0, 1);
            dir.Normalize();
            TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().SetFacingDirection(dir);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 dir = new Vector3(1, 0, 0);
            dir.Normalize();
            TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().SetFacingDirection(dir);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 dir = new Vector3(0, 0, -1);
            dir.Normalize();
            TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().SetFacingDirection(dir);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 dir = new Vector3(-1, 0, 0);
            dir.Normalize();
            TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().SetFacingDirection(dir);
        }
    }
}
