using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is the overerall manager for the project. It hold the definitive 
/// </summary>
public class ProjectManager : MonoBehaviour {

    private GameData gameData;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameData getGameData()
    {
        return gameData;
    }

    public void setGameData(GameData tempGameData)
    {
        gameData = tempGameData;
    }
}
