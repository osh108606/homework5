using UnityEngine;

public class StageSelectNpc : NPC
{
    public Collider2D col;
    public GameObject canvasObjede;
    public StageSelect stageSelect;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasObjede.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasObjede.SetActive(false);
        }
    }

    public void TalkUI()
    {
        if (stageSelect.gameObject.activeSelf == true)
        {
            stageSelect.gameObject.SetActive(false);
        }
        else
        {
            stageSelect.gameObject.SetActive(true);
        }
    }
}
