using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    public static AppleManager instance;

    [SerializeField]
    private GameObject apple;

    private Apple currentApple;

    private void Awake()
    {
        instance = this;
    }

    public void GrowApple()
    {
        currentApple = Instantiate(apple, new Vector3(1f * Random.Range(1, 10), 1f * Random.Range(1, 10), 0f), Quaternion.identity, this.transform).GetComponent<Apple>();
    }

    public void CheckCollision(Vector3 pos)
    {
        if (currentApple.Check(pos)) 
        {
            Destroy(currentApple);
            Snake.instance.AddBody();
            GrowApple();
        }
    }
}
