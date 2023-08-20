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
        _subtitle_TMP = _subtitle_TMP.Replace("플레이어", JDataManager.instance.playerData.name);
            subtitleBehaviour._subtitle_TMP = _subtitle_TMP;

        return playable;
    }
}
