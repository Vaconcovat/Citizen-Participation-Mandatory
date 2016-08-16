using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour {

	public void Exit(){
		Application.Quit();
	}

	public void Arena(){
		RoundManager.roundNumber = 1;
		SceneManager.LoadScene(1);
	}

	public void Menu(){
		SceneManager.LoadScene(0);
	}

	public void ToPostArena(){
		FindObjectOfType<StaticGameStats>().Influence(1, StaticGameStats.CorEndOfTournamentDecayDecrease);
		StaticGameStats.committed = false;
		StaticGameStats.toPost = true;
		SceneManager.LoadScene(0);
	}

	public void RoundRestart(){
		SceneManager.LoadScene(1);
	}
}
