using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, Subject
{
    private static PlayerManager _instance = null;
    public static PlayerManager Instance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private int gameRound = 0;
    private string whoseTurn = "Enemy";
    private bool isEnd = false;

    private delegate void TurnHandler(int round, string turn);
    private TurnHandler _turnHandler;
    private delegate void FinishHandler(bool isFinish);
    private FinishHandler _finishHandler;


    public void RoundNotify()
    {
        if (!isEnd)
        {
            if (whoseTurn == "Enemy")
            {
                gameRound++;
                Debug.Log($"PlayerManager: Round {gameRound}.");
            }
            TurnNotify();
        }
    }

    public void TurnNotify()
    {
        whoseTurn = whoseTurn == "Enemy" ? "Player" : "Enemy";
        Debug.Log($"PlayerManager: {whoseTurn} turn.");
        _turnHandler(gameRound, whoseTurn);
    }

    public void EndNotify(string name)
    {
        isEnd = true;
        _finishHandler(isEnd);
        Debug.Log("PlayerManager: The End");
        Debug.Log($"PlayerManager: Character {name} is Win!");
    }

    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.ObserverTurnUpdate);
        _finishHandler += new FinishHandler(character.ObserverFinishUpdate);
    }
}
