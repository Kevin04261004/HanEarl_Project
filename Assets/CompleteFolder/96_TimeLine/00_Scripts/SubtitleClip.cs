using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleClip : PlayableAsset
{
    [TextArea(3,5)]
    public string _subtitle_TMP;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

        SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
        subtitleBehaviour._subtitle_TMP = _subtitle_TMP;

        return playable;
    }
}
