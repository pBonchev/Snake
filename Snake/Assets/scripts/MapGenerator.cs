using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;

    [SerializeField]
    private GameObject grass;

    public int widgth = 10;
    public int heigth = 10;

    private void Awake()
    {
        instance = this;
    }

    public void CreateMap()
    {
        for (int i = 0; i < widgth; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                Instantiate(grass, new Vector3(transform.position.x + i, transform.position.y + j, transform.position.z), Quaternion.identity, this.transform);
            }
        }
    }
}
