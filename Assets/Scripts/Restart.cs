using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : ButtonObj
{
    public override void OnMouseDown()
    {
        GameManager.Instance.MoveSceneWithString("TitleScene");
        GameManager.Instance.inTitle = true;
        GameManager.Instance.inEnding = false;
        GameManager.Instance.movedDistance = 0;
        WaveGenerate.Instance.totalWaveCount = 0;
    }
}
