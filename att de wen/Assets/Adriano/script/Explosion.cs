using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public spriteanim inicio;
    public spriteanim meio;
    public spriteanim fim;
   
    public void SetActiveRenderer(spriteanim renderer)
    {
        inicio.enabled = renderer == inicio;
        meio.enabled = renderer == meio;
        fim.enabled = renderer == fim;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
    
}
