using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public UnityEngine.UI.Image image;

    public int speed = 5;

    void Start()
    {
        StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {

        yield return new WaitForSeconds(40);

        while (image.color != Color.black)
        {
            image.color = Color.Lerp(image.color, Color.black, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
