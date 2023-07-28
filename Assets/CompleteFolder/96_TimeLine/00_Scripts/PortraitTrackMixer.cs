using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Playables;

public class PortraitTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Image image = playerData as Image;
        Sprite sprite = null;
        float curAlpha = 0f;

        if (!image)
        {
            return;
        }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeigth = playable.GetInputWeight(i);

            if (inputWeigth > 0f)
            {
                ScriptPlayable<PortraitBehaviour> inputPlayable =
                    (ScriptPlayable<PortraitBehaviour>)playable.GetInput(i);

                PortraitBehaviour input = inputPlayable.GetBehaviour();
                sprite = input._portrait_sprite;
                curAlpha = inputWeigth;
            }
        }

        image.sprite = sprite;
        image.color = new Color(1, 1, 1, curAlpha);
    }
}
