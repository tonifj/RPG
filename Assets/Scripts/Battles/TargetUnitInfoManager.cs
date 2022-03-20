using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetUnitInfoManager : MonoBehaviour
{ 
    public TMP_Text unit_name;
    public TMP_Text current_hp;
    public TMP_Text max_hp;
    public TMP_Text current_psique;
    public TMP_Text max_psique;
    public GameObject ContainerGO;

    Unit target;

    void Start()
    {
        unit_name.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //TODO: make the character glow intermitently

            ContainerGO.SetActive(true);
            unit_name.text = target.GetName();
            max_hp.text = "/ " + target.GetMaxHP().ToString();
            current_hp.text = target.GetCurrentHP().ToString();
            max_psique.text = "/ " + target.GetMaxPsique().ToString();
            current_psique.text = target.GetCurrentPsique().ToString();
        }

        else
            ContainerGO.SetActive(false);

    }

    public void SetUnit(Unit u)
    {
        target = u;
    }
}
