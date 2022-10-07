using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShowText : MonoBehaviour
{

    public GameObject ColliderObject; // collide with object to make the text appear (after a wait)

    // Start is called before the first frame update
    void Start()
    {
        ColliderObject.SetActive(false);
    }

    private void OnTriggerEnter()
    {
        ColliderObject.SetActive(true);
        StartCoroutine(SetTextInactive());
    }

    IEnumerator SetTextInactive()
    {
        yield return new WaitForSeconds(3);
        ColliderObject.SetActive(false);
    }
}
