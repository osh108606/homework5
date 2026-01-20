using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dropItemPrifap;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Vector2 randfomPos = Player.Instance.transform.position + (Vector3)Random.insideUnitCircle * 3f;
            GameObject drop = Instantiate(dropItemPrifap);
            drop.GetComponent<DropItem>().Drop("MP5");
            drop.transform.position = randfomPos;


        }
    }
}
