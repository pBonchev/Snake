using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    public static AppleManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GrowApple()
    {

    }
}
