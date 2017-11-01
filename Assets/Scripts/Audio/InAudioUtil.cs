using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InAudioSystem;
using InAudioSystem.Internal;

public class InAudioUtil {

    private InAudioNode rootAudioNode;
    private InMusicNode rootMusicNode;

    private static InAudioUtil _instance;
    private static InAudioUtil Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InAudioUtil();
            }
            return _instance;
        }
    }

    public InAudioUtil()
    {
        rootAudioNode = InAudioInstanceFinder.DataManager.AudioTree;
        rootMusicNode = InAudioInstanceFinder.DataManager.MusicTree;
    }

	public static InAudioNode GetAudioNodeByName(string name)
    {
        //If you want to find something by a specific name or another parameter.
        //TreeWalker is the prefered way to traverse the hiearchy.
        return TreeWalker.FindFirst(Instance.rootAudioNode, node => node.GetName == name);
    }

    public static InMusicNode GetMusicNodeByName(string name)
    {
        return TreeWalker.FindFirst<InMusicNode>(Instance.rootMusicNode, node => node.GetName == name);
    }
}
