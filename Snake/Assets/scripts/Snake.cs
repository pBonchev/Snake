using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake instance;

    private void Awake()
    {
        instance = this;
    }
}
