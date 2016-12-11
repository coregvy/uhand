using System.Threading;
using UnityEngine;

public class Shake : MonoBehaviour {
	// Use this for initialization
	void Start () {
        var uhand = GameObject.Find("UnlimitedHand").GetComponent<UH>();
        uhand.setVoltageLevelUp();
        Thread.Sleep(10);
        uhand.setVoltageLevelUp();
        Thread.Sleep(10);
        uhand.setVoltageLevelUp();
        Thread.Sleep(10);
        uhand.setVoltageLevelUp();
        Thread.Sleep(10);
        uhand.setVoltageLevelUp();
        Thread.Sleep(10);
        uhand.setVoltageLevelUp();
        Thread.Sleep(10);
        uhand.setVoltageLevelUp();
        Thread.Sleep(10);
        uhand.stimulate(2);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
