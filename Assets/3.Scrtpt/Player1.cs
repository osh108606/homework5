using UnityEngine;



public class Player1 : MonoBehaviour
{
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //���������� �ö󰡴� �ڵ� ����
            transform.position = transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //�������� �ö󰡴� �ڵ� ����
            transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //���� �ö󰡴� �ڵ� ����
            transform.position = transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //�Ʒ��� �ö󰡴� �ڵ� ����
            transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
        }
    }
}
