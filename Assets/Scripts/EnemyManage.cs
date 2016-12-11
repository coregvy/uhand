using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyManage : MonoBehaviour {
    float time = 0.0f;
    [SerializeField]
    float intervalSec = 2.0f;
    [SerializeField]
    int enemyCount = 1;
    [SerializeField]
    int destroyCount = 0;
    [SerializeField]
    private int MAX_ENEMY = 20;
    [SerializeField]
    Text scoreText;
    int score;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (score >= 5000 || Input.GetKeyDown(KeyCode.Z))
            SceneManager.LoadScene("Clear");
        scoreText.text = string.Format("{0,6}", score) + " pt";
        if (scoreText.fontSize > 50)
            scoreText.fontSize--;
        if (score < destroyCount * 1000)
            score += 100;
        this.GetComponent<Transform>().position = this.GetComponent<Transform>().position + new Vector3(-0.05f, 0, 0);
        time += Time.deltaTime;
        if (time > intervalSec && enemyCount < MAX_ENEMY)
        {
            Debug.Log("add enemy");
            var newEnemy = (GameObject)Resources.Load("shakemanPrefab");
            newEnemy.GetComponent<EnemyCtrl>().enemyManager = this;
            Instantiate(newEnemy, new Vector3(Random.value * 60 + 20, 10, Random.value * 100 - 50), Quaternion.Euler(0,-90,0));
            time = 0.0f;
            ++enemyCount;
        }
    }

    public void destroyAndCount()
    {
        --enemyCount;
        ++destroyCount;
        scoreText.fontSize = 70;
    }
    public void destroy()
    {
        --enemyCount;
    }
}
