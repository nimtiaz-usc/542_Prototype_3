using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSprite : MonoBehaviour
{
    private Image sprite;
    public Sprite[] frames;
    public float frameRate;
    private int frameIndex = 0;
    public bool loop = true;

    void Start()
    {
        sprite = GetComponent<Image>();
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        sprite.sprite = frames[frameIndex];
        frameIndex++;
        yield return new WaitForSeconds(1f / frameRate);
        if (frameIndex >= frames.Length && loop)
        {
            frameIndex = 0;
            StartCoroutine(PlayAnimation());
        } else if (frameIndex < frames.Length)
        {
            StartCoroutine(PlayAnimation());
        }
    }
}
