using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Scrolling : MonoBehaviour
{
    public int speed = 1;

    public float xScrolling = 0;

    public float yScrolling = 0;

    public new Renderer renderer;

    void Update()
    {
        renderer.material.mainTextureOffset += new Vector2(speed*Time.deltaTime*xScrolling,speed*Time.deltaTime*yScrolling);
        if(renderer.material.mainTextureOffset.x <= -1){
            renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x+1,renderer.material.mainTextureOffset.y);
        }else if(renderer.material.mainTextureOffset.x >= 1){
            renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x-1,renderer.material.mainTextureOffset.y);
        }
        if(renderer.material.mainTextureOffset.y <= -1){
            renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x,renderer.material.mainTextureOffset.y+1);
        }else if(renderer.material.mainTextureOffset.y >= 1){
            renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x,renderer.material.mainTextureOffset.y-1);
        }
    }
}
