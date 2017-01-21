using UnityEngine;
using System.Collections;
using ACTBase;

public class GameStart : MonoBehaviour
{

    void Awake()
    {
        TickerMgr.Get();
    }
	// Use this for initialization
	void Start ()
    {
        TickerMgr.Get().Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        TickerMgr.Get().DoTick();
    }
}
