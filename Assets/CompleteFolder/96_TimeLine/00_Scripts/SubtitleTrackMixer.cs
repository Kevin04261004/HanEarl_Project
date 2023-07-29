using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        string curText = "";
        float curAlpha = 0f;

        if (!text)
        {
            return;
        }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; ++i)
        {
            float inputWeigth = playable.GetInputWeight(i);

            if (inputWeigth > 0f)
            {
                ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);

                SubtitleBehaviour input = inputPlayable.GetBehaviour();
                curText = input._subtitle_TMP;
                curAlpha = inputWeigth;
            }
        }

        text.text = curText;
        text.color = new Color(text.color.r, text.color.g, text.color.b, curAlpha);
    }
}
