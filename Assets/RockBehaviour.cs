using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour
{
    [SerializeField]
    UH uhand;
    [SerializeField]
    Quaternion baseQtn = Quaternion.identity;
    [SerializeField]
    int avgOpen = 1000;
    [SerializeField]
    int avgClose=0;
    [SerializeField]
    int margin = 10;
    // Use this for initialization
    void Start()
    {
        ResetAngle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("gripTrigger");
            ResetAngle();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            avgOpen = uhand.UHPR[1];// + uhand.UHPR[1] + uhand.UHPR[2] + uhand.UHPR[3];
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            avgClose = uhand.UHPR[1];// + uhand.UHPR[1] + uhand.UHPR[2] + uhand.UHPR[3];
        }
        if(avgClose+margin > uhand.UHPR[1])// + uhand.UHPR[1] + uhand.UHPR[2] + uhand.UHPR[3])
        {
            GetComponent<Animator>().ResetTrigger("openTrigger");
            GetComponent<Animator>().SetTrigger("gripTrigger");
        }
        else if (avgOpen - margin < uhand.UHPR[1])
        {
            GetComponent<Animator>().ResetTrigger("gripTrigger");
            GetComponent<Animator>().SetTrigger("openTrigger");
        }
        //transform.rotation = new Quaternion(uhand.UHQuaternion[0], uhand.UHQuaternion[1]
        //        , uhand.UHQuaternion[2], uhand.UHQuaternion[3]);
        transform.eulerAngles = new Vector3(uhand.UHAngle[0], uhand.UHAngle[1], uhand.UHAngle[2]);
    }


    void ResetAngle()
    {
        if (uhand == null) return;
        baseQtn = new Quaternion(uhand.UHQuaternion[0], uhand.UHQuaternion[1]
            , uhand.UHQuaternion[2], uhand.UHQuaternion[3]);
        euler = baseQtn.eulerAngles;
        Debug.Log("reset angle: ");
    }
    public Vector3 euler;
}
