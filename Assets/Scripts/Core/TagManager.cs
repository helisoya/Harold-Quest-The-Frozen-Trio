using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    public static void Inject(ref string s, bool speaker = true)
    {
        if (speaker && s.Equals("narrator")) return;

        if (speaker && !s.Contains("["))
        {
            s = Locals.GetLocal("Char_" + s);
            return;
        }
        s = s.Replace("[MC]", GameItems.instance.GetItem("playerName"));

    }

    public static string[] SplitByTags(string targetText)
    {
        return targetText.Split(new char[2] { '<', '>' });
    }
}
