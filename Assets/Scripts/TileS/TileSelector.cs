using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 start_pos;
    float value = 0;
    void Start()
    {
        start_pos = gameObject.GetComponentInParent<Transform>().position;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MakeTheSelectorFloat();
    }

    void MakeTheSelectorFloat()
    {
        value += Time.deltaTime*6;

        gameObject.transform.position = new Vector3(start_pos.x, start_pos.y + 0.25f*Mathf.Sin(value), start_pos.z);

    }
}
