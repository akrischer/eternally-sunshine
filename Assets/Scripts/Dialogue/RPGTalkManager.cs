using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGTalkManager : MonoBehaviour {

    private RPGTalk _rpgTalk;


	// Use this for initialization
	void Start () {
        _rpgTalk = GetComponent<RPGTalk>();
	}

    /// <summary>
    /// Name of the scripts that can (and will) be played.
    /// In the actual text document, the tags that delineate each script have "_start" and "_end" suffixes attached.
    /// E.g. 'MyScript' would have tags 'MyScript_start' and 'MyScript_end'
    /// </summary>
    public void PlayTalk(string talk)
    {
        string start, end;
        start = talk + "_start";
        end = talk + "_end";
        _rpgTalk.NewTalk(start, end);
    }
}
