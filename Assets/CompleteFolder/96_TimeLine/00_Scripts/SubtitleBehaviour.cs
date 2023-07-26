using UnityEngine.Playables;

public class SubtitleBehaviour : PlayableBehaviour
{
    public string _subtitle_TMP;

    // public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    // {
    //     TextMeshProUGUI text = playerData as TextMeshProUGUI;
    //     text.text = _subtitle_TMP;
    //     text.color = new Color(1, 1, 1, info.weight);
    // }
}
