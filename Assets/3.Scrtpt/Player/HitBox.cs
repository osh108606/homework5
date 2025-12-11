using UnityEditor;
using UnityEngine;

public enum BodyParts
{
    Head,Body,Leg
}
public class HitBox : MonoBehaviour
{
    public Collider2D hitbox;
    public BodyParts bodyParts;
    [Range(0f, 5f)]
    public float damageMultiplier = 1f;
    //defualt: 赣府 2 个 1 促府殿 何加何困0.5 
    public void Awake()
    {
        hitbox = GetComponentInChildren<Collider2D>();
    }
    public float GetDamageMultiplier()
    {
            Debug.Log(bodyParts);
        return damageMultiplier;
    }
}
