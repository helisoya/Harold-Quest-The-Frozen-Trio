using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BCFC : MonoBehaviour
{
    public static BCFC instance;

    public LAYER background = new LAYER();
    public LAYER cinematic = new LAYER();
    public LAYER foreground = new LAYER();


    void Start(){
        instance = this;
    }

    [System.Serializable]
    public class LAYER{
        public GameObject root;

        public RenderTexture rdtext;
        public GameObject newImageObject;
        public RawImage activeImage;
        public List<RawImage> allImages = new List<RawImage>();

        public Coroutine specialTransitionCoroutine = null;

        public void SetTexture(Texture text){

            if(text != null){
                if(activeImage == null){
                    CreateNewActiveImage();
                }

                activeImage.texture = text;
                activeImage.color = GlobalF.SetAlpha(activeImage.color,1f);
   
            }else{
                if(activeImage!=null){
                    allImages.Remove(activeImage);
                    GameObject.DestroyImmediate(activeImage.gameObject);
                    activeImage = null;
                }
            }
        }


        public void TransitionToTexture(Texture texture,float speed = 2f,bool smooth = false){
            if(activeImage!=null && activeImage.texture == texture){
                return;
            }
            StopTransitioning();
            transition = BCFC.instance.StartCoroutine(Transitioning(texture,speed,smooth));
        }

        public void StopTransitioning(){
            if(isTransitioning()){
                BCFC.instance.StopCoroutine(transition);
            }
            transition = null;
        }

        public bool isTransitioning(){return transition!=null;}

        Coroutine transition = null;

        IEnumerator Transitioning(Texture texture,float speed,bool smooth){
			if (texture != null)
			{
				for (int i = 0; i < allImages.Count; i++) 
				{
					RawImage image = allImages [i];
					if (image.texture == texture) 
					{
						activeImage = image;
						break;
					}
				}

				if (activeImage == null || activeImage.texture != texture)
				{
					CreateNewActiveImage();
					activeImage.texture = texture;
					activeImage.color = GlobalF.SetAlpha(activeImage.color, 0f);

				}
			}
			else
				activeImage = null;

			while(GlobalF.TransitionRawImages(ref activeImage, ref allImages, speed, smooth))
				yield return new WaitForEndOfFrame();

			StopTransitioning();

        }

        void CreateNewActiveImage(){
            GameObject obj = Instantiate(newImageObject,root.transform) as GameObject;
            obj.SetActive(true);
            RawImage img = obj.GetComponent<RawImage>();
            activeImage = img;
            allImages.Add(img);
        }
    }
}
