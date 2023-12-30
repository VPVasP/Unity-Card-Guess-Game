using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ButtonScriptable", menuName = "ScriptableObjects/ButtonScriptable", order = 1)]
public class ButtonScriptable : ScriptableObject
{
    public Sprite firstPicture;
    public Sprite secondPicture;
    public int id;
}
