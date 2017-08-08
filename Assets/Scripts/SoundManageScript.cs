using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManageScript : MonoBehaviour {
    //variables 

    //make this singleton
    public static SoundManageScript instance;

    //array of zombie groaning sounds
    public AudioClip[] ZombieGroanAC;
    public GameObject hand;

    //when app awakes create the instance
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playZombieGrowl(Transform Pos)
    {
        int RND = Random.Range(0, ZombieGroanAC.Length);
        AudioSource.PlayClipAtPoint(ZombieGroanAC[RND], Pos.transform.position, 0.3f);

        hand = GameObject.Find("One shot audio");
        hand.GetComponent<AudioSource>().spatialBlend = 10;
    }
}
