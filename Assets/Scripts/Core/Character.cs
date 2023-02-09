using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character
{
    public string AnimationSetName;

    [HideInInspector] public RectTransform root;

    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }


    public Vector2 anchorPadding { get { return root.anchorMax - root.anchorMin; } }

    DialogSystem dialogue;


    public Sprite[] animationNormal;

    public Sprite[] animationSpeech;

    public bool AnimationIsSetToNormal = true;

    Coroutine animation;

    public bool preserveAspect = true;

    public Character()
    {
    }

    public void StopAnimation()
    {
        if (animation != null)
        {
            CharacterManager.instance.StopCoroutine(animation);
        }

        animation = null;
    }
    IEnumerator Animate(Sprite[] sprites, bool stopBetweenLoops = false)
    {
        int i = 0;
        while (true)
        {
            renderer.sprite = sprites[i];
            renderer.preserveAspect = true;
            if (stopBetweenLoops && i == 0)
            {
                yield return new WaitForSeconds(2);
            }

            i++;
            if (i >= sprites.Length)
            {
                i = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }

    }

    public void SetAnimation(Sprite[] sprites, bool stopBetweenLoops)
    {
        if (animationSpeech == null || animationNormal == null)
        {
            return;
        }
        if (sprites == animationNormal)
        {
            AnimationIsSetToNormal = true;
        }
        else
        {
            AnimationIsSetToNormal = false;
        }
        if (animation != null)
        {
            CharacterManager.instance.StopCoroutine(animation);
            animation = null;
        }
        animation = CharacterManager.instance.StartCoroutine(Animate(sprites, stopBetweenLoops));
    }



    Vector2 targetPosition;

    public Vector2 _targetPosition { get { return targetPosition; } }

    Coroutine moving;
    bool isMoving { get { return moving != null; } }
    public void MoveTo(Vector2 Target, float speed, bool smooth = true)
    {
        StopMoving();
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }


    public void StopMoving(bool arriveAtTargetPositionImmediately = false)
    {
        if (isMoving)
        {
            CharacterManager.instance.StopCoroutine(moving);
            if (arriveAtTargetPositionImmediately)
                SetPosition(targetPosition);
        }
        moving = null;
    }


    public void SetPosition(Vector2 target)
    {
        Vector2 padding = anchorPadding;
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;
        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);

        root.anchorMin = minAnchorTarget;
        root.anchorMax = root.anchorMin + padding;
    }


    IEnumerator Moving(Vector2 target, float speed, bool smooth)
    {
        targetPosition = target;


        Vector2 padding = anchorPadding;


        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;


        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
        speed *= Time.deltaTime;

        while (root.anchorMin != minAnchorTarget)
        {
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        StopMoving();
    }

    public Color GetColor()
    {
        return root.transform.GetChild(0).GetComponent<Image>().color;
    }

    public void SetColor(Color color)
    {
        root.transform.GetChild(0).GetComponent<Image>().color = color;
    }

    Coroutine fade = null;

    public Color targetColor = Color.white;

    public void FadeIn()
    {
        if (fade != null)
        {
            CharacterManager.instance.StopCoroutine(fade);
            fade = null;
        }
        fade = CharacterManager.instance.StartCoroutine(Fade(new Color(1, 1, 1, 1), 5));
    }

    public void FadeOut()
    {
        if (fade != null)
        {
            CharacterManager.instance.StopCoroutine(fade);
            fade = null;
        }
        fade = CharacterManager.instance.StartCoroutine(Fade(new Color(1, 1, 1, 0), 5));
    }

    IEnumerator Fade(Color target, float speed)
    {
        targetColor = target;
        Image img = root.transform.GetChild(0).GetComponent<Image>();

        while (img.color != target)
        {
            img.color = Color.Lerp(img.color, target, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        fade = null;
    }


    public Image renderer;
}
