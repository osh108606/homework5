using UnityEngine;
using UnityEngine.Serialization;

public class StageSelectNpc : NPC
{
    public Collider2D col;
    public GameObject canvasObject;
    public StageSelect stageSelect;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasObject.SetActive(false);
        }
    }

    public void TalkUI()
    {
        if (stageSelect.gameObject.activeSelf)
        {
            stageSelect.gameObject.SetActive(false);
        }
        else
        {
            stageSelect.gameObject.SetActive(true);
        }
    }
}
