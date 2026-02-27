using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject dropItemPrefab;

    void Awake()
    {
        UserManager.instance.Load(UserManager.instance.userDataFileName);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Vector2 randomPos = Player.Instance.transform.position + (Vector3)Random.insideUnitCircle * 3f;
            GameObject drop = Instantiate(dropItemPrefab);
            drop.GetComponent<DropItem>().Drop("MP5");
            drop.transform.position = randomPos;


        }
    }
}
