using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitInfoManager : MonoBehaviour
{

    public TMP_Text unit_name;
    public TMP_Text current_hp;
    public TMP_Text max_hp;
    public TMP_Text current_psique;
    public TMP_Text max_psique;

    void Start()
    {
        unit_name.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(TurnManager.instance.GetUnitWithTurn() != null)
        {
            unit_name.text = TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().GetName();
            max_hp.text = "/ " + TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().GetMaxHP().ToString();
            current_hp.text = TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().GetCurrentHP().ToString();
            max_psique.text = "/ " + TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().GetMaxPsique().ToString();
            current_psique.text = TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().GetCurrentPsique().ToString();
        }

    }
}
