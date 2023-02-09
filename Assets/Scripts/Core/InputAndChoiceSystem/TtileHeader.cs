using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TtileHeader : MonoBehaviour
{
    public Image banner;

    public LocalizedText titleText;

    public enum DISPLAY_METHOD
    {
        instant,
        slowFade,
        typeWriter
    }

    public DISPLAY_METHOD displayMethod = DISPLAY_METHOD.instant;

    public float fadeSpeed = 1;

    public void Show(string displayTitle)
    {
        titleText.SetNewKey(displayTitle);

        if (isRevealing)
        {
            StopCoroutine(revealing);
        }
        revealing = StartCoroutine(Revealing());
    }

    public void Hide()
    {
        if (isRevealing)
        {
            StopCoroutine(revealing);
        }
        revealing = null;
        banner.enabled = false;
        if (titleText != null)
            titleText.GetText().enabled = false;
    }

    public bool isRevealing { get { return revealing != null; } }

    Coroutine revealing = null;

    IEnumerator Revealing()
    {
        banner.enabled = true;
        titleText.GetText().enabled = true;
        switch (displayMethod)
        {
            case DISPLAY_METHOD.instant:
                banner.color = GlobalF.SetAlpha(banner.color, 1);
                titleText.GetText().color = GlobalF.SetAlpha(titleText.GetText().color, 1);
                break;
            case DISPLAY_METHOD.slowFade:
                banner.color = GlobalF.SetAlpha(banner.color, 0);
                titleText.GetText().color = GlobalF.SetAlpha(titleText.GetText().color, 0);
                while (banner.color.a < 1)
                {
                    banner.color = GlobalF.SetAlpha(banner.color, Mathf.MoveTowards(banner.color.a, 1, fadeSpeed * Time.unscaledDeltaTime));
                    titleText.GetText().color = GlobalF.SetAlpha(titleText.GetText().color, banner.color.a);
                    yield return new WaitForEndOfFrame();
                }
                break;
            case DISPLAY_METHOD.typeWriter:
                banner.color = GlobalF.SetAlpha(banner.color, 1);
                titleText.GetText().color = GlobalF.SetAlpha(titleText.GetText().color, 1);
                TextArchitect architect = new TextArchitect(titleText.GetText(), titleText.GetText().text);
                while (architect.isConstructing)
                {
                    yield return new WaitForEndOfFrame();
                }
                break;
        }
        revealing = null;
    }
}
