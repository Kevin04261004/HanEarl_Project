using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PortraitClip : PlayableAsset
{
    public Sprite _portrait_Sprite;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PortraitBehaviour>.Create(graph);

        PortraitBehaviour portraitBehaviour = playable.GetBehaviour();
        portraitBehaviour._portrait_sprite = _portrait_Sprite;

        return playable;
    }
}
