using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.UI;

[TrackBindingType(typeof(Image))]
[TrackClipType(typeof(SubtitleClip))]
public class PortraitTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<PortraitTrackMixer>.Create(graph, inputCount);
    }
}
