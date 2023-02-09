using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    public Dictionary<string, Holder_Animation> persos = new Dictionary<string, Holder_Animation>();

    [System.Serializable]
    public struct Sprites
    {
        public string name;
        public Holder_Animation animation;
    }
    public Sprites[] sprites;

    public RectTransform characterPanel;

    public Character character = new Character();


    void Awake()
    {
        instance = this;
        foreach (Sprites s in sprites)
        {
            persos.Add(s.name, s.animation);
        }
        character.root = GameObject.FindWithTag("Character").GetComponent<RectTransform>();
        character.renderer = character.root.transform.GetChild(0).GetComponent<Image>();
    }


    public Character GetCharacter()
    {
        return character;
    }


    public void ChangeCharacter(string name, bool preserveAspect = true)
    {
        if (persos.ContainsKey(name))
        {
            character.preserveAspect = true;
            character.animationNormal = persos[name].animationNormal;
            character.animationSpeech = persos[name].animationSpeech;
            character.AnimationSetName = name;
            character.StopAnimation();
            if (character.AnimationIsSetToNormal)
            {
                character.SetAnimation(character.animationNormal, true);
            }
            else
            {
                character.SetAnimation(character.animationSpeech, false);
            }
        }
    }


    public class CHARACTERPOSITIONS
    {
        public Vector2 bottomLeft = new Vector2(0, 0);
        public Vector2 topRight = new Vector2(1f, 1f);
        public Vector2 center = new Vector2(0.5f, 0.5f);
        public Vector2 bottomRight = new Vector2(1f, 0);
        public Vector2 topLeft = new Vector2(0, 1f);
    }
    public static CHARACTERPOSITIONS characterPositions = new CHARACTERPOSITIONS();

}
