using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPanel : MonoBehaviour
{
    public Transform warning;

    int minus = 1;

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"warning.localScale.x " + warning.localScale.x);
        if (warning.localScale.x < 0.7f)
        {
            minus = 1;
        }
        if (warning.localScale.x > 1.2f)
        {
            minus = -1;
        }
        warning.localScale = warning.localScale + minus * (Time.deltaTime * new Vector3(1, 1, 0));

    }
}
