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
        Vector3 pos = new Vector3(1f * Random.Range(1, 10), 1f * Random.Range(1, 10), 0f);

        int cnt = 0;

        while (Snake.instance.GrassContainsSnake(pos) && cnt < 1000) 
        {
            pos = new Vector3(1f * Random.Range(1, 10), 1f * Random.Range(1, 10), 0f);
            cnt++;
        }

        if (cnt >= 1000)
        {
            for (int i = 0; i < MapGenerator.instance.widgth; i++)
            {
                for (int j = 0; j < MapGenerator.instance.heigth; j++)
                {
                    if (!Snake.instance.GrassContainsSnake(new Vector3(1f * i, 1f * j, 0f)))
                    {
                        pos = new Vector3(1f * i, 1f * j, 0f);
                        currentApple = Instantiate(apple, pos, Quaternion.identity, this.transform).GetComponent<Apple>();
                        return;
                    }
                }
            }
        }

        currentApple = Instantiate(apple, pos, Quaternion.identity, this.transform).GetComponent<Apple>();
    }

    public void CheckCollision(Vector3 pos)
    {
        if (currentApple.Check(pos)) 
        {
            Destroy(currentApple.gameObject);
            Snake.instance.AddBody();
            GrowApple();
        }
    }
}
