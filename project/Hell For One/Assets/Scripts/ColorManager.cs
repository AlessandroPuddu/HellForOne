﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public GameObject circle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateCircle() {
        circle.SetActive(true);
        circle.GetComponent<MeshRenderer>().material = gameObject.GetComponent<DemonBehaviour>().groupBelongingTo.GetComponent<GroupBehaviour>().groupColor;
    }

    public void DeactivateCircle() {
        circle.SetActive(false);
    }
}
