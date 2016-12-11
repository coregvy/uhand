using UnityEngine;
using System.Collections;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FirePos : MonoBehaviour
{
    /// <summary>
    /// 手の先に出す炎の位置の基準
    /// </summary>
    [SerializeField]
    Transform hand;
    [SerializeField]
    StimCtrl stimCtrl;
    [SerializeField]
    int padNum = 2;
    /// <summary>
    /// 投げる開始の高さ
    /// </summary>
    [SerializeField]
    float thresholdHurlY = 9.0f;
    /// <summary>
    /// 炎が発生する高さの閾値
    /// </summary>
    [SerializeField]
    float thresholdFireY = 10.0f;
    /// <summary>
    /// 炎を手の先端に固定する
    /// </summary>
    bool isFixFirePos = false;
    /// <summary>
    /// 炎が投げられた状態. 飛んでいったか衝突して消えるときにfalseにすること.
    /// </summary>
    bool isHurl = false;
    /// <summary>
    /// 炎が飛んでいるスピード(毎フレーム加算される座標)
    /// </summary>
    Vector3 hurlSpeed;
    /// <summary>
    /// 炎表示中の前フレームにおける位置
    /// </summary>
    Vector3 beforeFrameFirePos;

    // Use this for initialization
    void Start()
    {
        delete();
//        StartCoroutine(LateStart(25.0f));
    }
    //IEnumerator LateStart(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    uhand.Disconnect();
    //    SceneManager.LoadScene("Clear");
    //    //遅れて初期化処理
    //}

    /// <summary>
    /// 炎を手先に表示
    /// </summary>
    public void show()
    {
        // 投げてる最中は再表示しない
        if (isHurl) return;
        // 手の高さで火の表示判定
        if (hand.position.y < thresholdFireY) return;
        Debug.Log("fire show.");
        //isShow = true;
        isFixFirePos = true;
        isHurl = false;
        // start stim
        stimCtrl.stimStrongly(padNum, () => { return isFixFirePos; });
    }
    /// <summary>
    /// 炎を画面外に移動
    /// </summary>
    public void delete()
    {
        // 投げてる最中は消さない
        if (isHurl) return;
        Debug.Log("fire delete.");
        // reset flag.
        //isShow = false;
        isFixFirePos = false;
        isHurl = false;
        GetComponent<Transform>().position = new Vector3(9999, 9999, 9999);
    }
    /// <summary>
    /// 炎を投げる
    /// </summary>
    /// <param name="speed"></param>
    public void hurl(Vector3 speed)
    {
        hurlSpeed = speed;
        isHurl = true;
        isFixFirePos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixFirePos)
        {
            // 炎の位置を手先に固定する
            GetComponent<Transform>().position = hand.position;
            if(hand.position.y < thresholdHurlY)
            {
                hurlSpeed = new Vector3(
                    hand.position.x - beforeFrameFirePos.x,
                    0,
                    hand.position.z - beforeFrameFirePos.z);
                isHurl = true;
                isFixFirePos = false;
            } else
            {
                beforeFrameFirePos = hand.position;
            }
        }
        else if (isHurl)
        {
            GetComponent<Transform>().position += hurlSpeed;
            float dist = Vector3.Distance(GetComponent<Transform>().position, new Vector3(0, 0, 0));
            //Debug.Log("check dist: " + dist);
            if (dist > 100)
            {
                // 離れすぎたら削除
                isHurl = false;
                delete();
            }
        }
    }
}
