using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartGame : ButtonObj
{

    public AudioSource ads;
    private void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    public override void OnMouseDown()
    {
        ads.Play();
        
        GameManager.Instance.MoveSceneWithString("PlayScene");
        GameManager.Instance.settleProbablilty = 0.1f;
        GameManager.Instance.inTitle = false;
    }
}
