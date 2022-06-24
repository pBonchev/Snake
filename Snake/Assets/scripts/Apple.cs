using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public bool Check(Vector3 pos)
    {
        if (transform.position == pos) return true;
        return false;
    }
}
