using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyCtrl : MonoBehaviour {
    // public static GameObject untitled = (GameObject)Resources.Load("shakemanPrefab");
    [SerializeField]
    public EnemyManage enemyManager;
    bool isDead = false;

    void OnTriggerEnter(Collider hit)
    {
        Debug.Log("fire hit." + hit.tag);
        if (hit.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Detonator>().Explode();
            if (!isDead)
            {
                isDead = true;
                if (enemyManager != null)
                    enemyManager.destroyAndCount();
                Object.Destroy(this.gameObject, 5);
            }
            //Debug.Log("destroy count: " + (destroyCount));
            //if (destroyCount > 10)
            //{
            //    SceneManager.LoadScene("Clear");
            //}
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if((transform.position.y < 7) && !isDead)
        {
            isDead = true;
            if (enemyManager != null)
                enemyManager.destroy();
            Object.Destroy(this.gameObject, 5);
        }
    }
}
