using UnityEngine;
using UnityEngine.UI;

public class TapCircle : MonoBehaviour
{
    SpriteRenderer spriteRenderer = null;
    Image img = null;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        img = GetComponent<Image>();
    }

    void Start()
    {
        if (spriteRenderer)
        {
            spriteRenderer.material.SetFloat("_StartTime", Time.time);

            float animationTime = spriteRenderer.material.GetFloat("_AnimationTime");
            float destroyTime = animationTime;
            destroyTime -= spriteRenderer.material.GetFloat("_StartWidth") * animationTime;
            destroyTime += spriteRenderer.material.GetFloat("_Width") * animationTime;
            Destroy(transform.gameObject, destroyTime);
        }
        if(img)
        {
            img.material.SetFloat("_StartTime", Time.time);

            float animationTime = img.material.GetFloat("_AnimationTime");
            float destroyTime = animationTime;
            destroyTime -= img.material.GetFloat("_StartWidth") * animationTime;
            destroyTime += img.material.GetFloat("_Width") * animationTime;
            Destroy(transform.gameObject, destroyTime);
        }
    }

}