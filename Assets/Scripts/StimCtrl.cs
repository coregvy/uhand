using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class StimCtrl : MonoBehaviour {
    [SerializeField]
    UH uhand;
    [SerializeField]
    int maxStimulateLevel = 10;

    // Use this for initialization
    void Start() {
        resetStimLevel();
    }

    private void resetStimLevel()
    {
        Debug.Log("reset minimum stimulate level.");
        // 少し待ちが必要らしい
        uhand.setVoltageLevelDown();
        Thread.Sleep(10);
        uhand.setVoltageLevelDown();
        Thread.Sleep(10);
        uhand.setVoltageLevelDown();
        Thread.Sleep(10);
        uhand.setVoltageLevelDown();
        Thread.Sleep(10);
        uhand.setVoltageLevelDown();
        Thread.Sleep(10);
        uhand.setVoltageLevelDown();
        Thread.Sleep(10);
        uhand.setVoltageLevelDown();
        Thread.Sleep(10);
    }
    /// <summary>
    /// 徐々に強い刺激を与える非同期処理
    /// </summary>
    /// <param name="pad">刺激を与えるパッド番号</param>
    /// <param name="firePos">炎の状態を取得する</param>
    public void stimStrongly(int pad, Func<bool> isFixFirePos)
    {
        Action act = () =>
        {
            // reset stimulate level.
            resetStimLevel();
            resetStimLevel();
            int count = 0;
            while (true)
            {
                if (!isFixFirePos())
                {
                    // 炎が手先から離れたら刺激ストップ
                    Debug.Log("stop fire stim.");
                    break;
                }
                if(count<maxStimulateLevel)
                {
                    // 最大levelまで上げる
                    Debug.Log("up stim level: " + count);
                    uhand.setVoltageLevelUp();
                }
                uhand.stimulate(pad);
                Thread.Sleep(1000);
                ++count;
            }
        };
        // exec action.
        Debug.Log("start fire stim.");
        act.BeginInvoke((IAsyncResult r) => { }, null);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
