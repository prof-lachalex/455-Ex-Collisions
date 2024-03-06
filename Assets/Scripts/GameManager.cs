using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Objets du jeu
    GameObject _gameBall;
    GameObject _ballLauncher;

    // Etat du jeu
    bool _ballInPlay;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameBall = GameObject.Find("GameBall");
        if(_gameBall == null)
        {
            Debug.LogError("Objet GameBall inexistant");
            return;
        }

        _ballLauncher = GameObject.Find("Spring");
        if (_ballLauncher == null)
        {
            Debug.LogError("Objet ballLauncher (Spring) inexistant");
            return;
        }

        _ballInPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        GameObject spawnPoint = GameObject.Find("BallSpawnPoint");
        if(_gameBall != null && spawnPoint != null) 
        { 
            _gameBall.transform.position = spawnPoint.transform.position;
            _ballInPlay = false;
        }
    }

    void ResetGateway()
    {
        GameObject gateway = GameObject.Find("Gateway");
        if(gateway != null) 
        { 
            gateway.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    void CloseGateway()
    {
        GameObject gateway = GameObject.Find("Gateway");
        if (gateway != null)
        {
            // Visuellement non attrayant: on ne voit pas de mur physiquement, mais la balle ne passera pas.
            gateway.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    // Fonctions Publiques
    public bool IsBallInPlay()
    { 
        return _ballInPlay;
    }

    public void OnBallExitGateway()
    {
        LauncherLogic launcherLogic = _ballLauncher.GetComponent<LauncherLogic>();
        if(launcherLogic != null)
        {
            launcherLogic.ResetLauncher();
        }

        _ballInPlay = true;
        CloseGateway();
    }

    public void OnBallExitsGameArea()
    {
        ResetBall();
        ResetGateway();
    }
}
