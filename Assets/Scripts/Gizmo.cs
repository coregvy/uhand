using UnityEngine;
using System.Collections;
using System;

public class Gizmo : MonoBehaviour {
    public float gizmoSize = 0.3f;
    [SerializeField]
    UH uhand;
    [SerializeField]
    FirePos fireBehaviour;
    
    [HideInInspector]
    public int[] uPRSum = new int[8];   //Sum of the Sensors' Values 

    [HideInInspector]
    public int count;                   //Count Number to calculate the Averages of the Sensor's Values

    [HideInInspector]
    public int[] uPRAve = new int[8];   //Averages of the Sensor's Values
    public int FIRE_THRESHOLD_OPEN = 30;    //Fire Threshold which compares with "PRVARSum"
    public int FIRE_THRESHOLD_CLOSE = -30;    //Fire Threshold which compares with "PRVARSum"

    public int PRVARSum;                //Finger Movement SUM

    bool isHandOpen = true;

    float[] quaternionOffest = new float[4];

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, gizmoSize);
    //}
    void Update()
    {
        count++;
        for (int i = 0; i < 8; i++)
        {
            uPRSum[i] += uhand.UHPR[i];
            uPRAve[i] = uPRSum[i] / count;
        }
        if (30 < count)
        {//reset the count
            for (int i = 0; i < 8; i++)
            {
                uPRSum[i] = uPRAve[i];
            }
            count = 1;
        }

        PRVARSum = 0;
        PRVARSum += (uhand.UHPR[1] - uPRAve[1]);        //Use only 4 sensors to detect the gun shoot gesture.
        PRVARSum += (uhand.UHPR[2] - uPRAve[2]);
        PRVARSum += (uhand.UHPR[3] - uPRAve[3]);

        if (PRVARSum > FIRE_THRESHOLD_OPEN || Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("open1");
            // 手を開く処理
            openHandAction();
        }
        else if (PRVARSum < FIRE_THRESHOLD_CLOSE || Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("close1");
            // 手を閉じた処理
            closeHandAction();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            uhand.resetQuaternion();
            uhand.UHQuaternion.CopyTo(quaternionOffest, 0);

        }

        transform.rotation = new Quaternion(uhand.UHQuaternion[0],
                                            uhand.UHQuaternion[1] - quaternionOffest[1],
                                            uhand.UHQuaternion[3] - quaternionOffest[3],
                                            uhand.UHQuaternion[2] - quaternionOffest[2]);
    }

    private void openHandAction()
    {
        if (!isHandOpen)
        {
            isHandOpen = true;
            GetComponent<Animator>().ResetTrigger("gripTrigger");
            GetComponent<Animator>().SetTrigger("openTrigger");
            fireBehaviour.show();
        }
    }

    private void closeHandAction()
    {
        if (isHandOpen)
        {
            isHandOpen = false;
            GetComponent<Animator>().ResetTrigger("openTrigger");
            GetComponent<Animator>().SetTrigger("gripTrigger");
            fireBehaviour.delete();
        }
    }
}
