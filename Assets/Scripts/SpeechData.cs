using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeech", menuName ="Speech")]
public class SpeechData : ScriptableObject
{
    public string id;

    public string name;

    [TextArea(3,10)]
    public string[] speechFR;

    [TextArea(3, 10)]
    public string[] speechGB;
}
