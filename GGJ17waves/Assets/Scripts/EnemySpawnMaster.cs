using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState {
	Init,
	Idle,
	Spawn,
	Wait,
	Barrage,
	BarrageWait,
	Reset
}

//SPAWN AS CHILDREN UNDER PARENT

public class EnemySpawnMaster : MonoBehaviour {
	public GameState _state;
	private int _score;
	private int _highScore;
	private float _frequency = 1.5f;
	private int _barrageChance = 1;
	private float _barrageLength = 5.0f;
	private float _barrageTimer = 0.0f;
	private float _barrageFrequency = .5f;
	private int _beltChance = 30;
	private int _beltMax = 10;
	private int _beltMin = 3;
	private GameObject _title;
	private GameObject _playerShip;
	private TextMesh _scoreText;
	private TextMesh _highScoreText;

    public GameObject[] enemy;
	
	// Use this for initialization
	void Start () {
		//PoolMaster.PlayAudio("Music", true);
		StartCoroutine ("GameController");
	}

	private IEnumerator GameController () {
		while (true) {
			switch (_state) {
			case GameState.Init:
				Init ();
				yield return new WaitForSeconds (5.0f);
				break;
			case GameState.Idle:
				Idle ();
				yield return new WaitForEndOfFrame ();
				break;
			case GameState.Spawn:
				Spawn ();
				yield return new WaitForEndOfFrame ();
				break;
			case GameState.Wait:
				Wait ();
				yield return new WaitForSeconds (_frequency);
				break;
			case GameState.Barrage:
				Barrage ();
				yield return new WaitForEndOfFrame ();
				break;
			case GameState.BarrageWait:
				BarrageWait ();
				yield return new WaitForSeconds (_barrageFrequency);
				break;
			case GameState.Reset:
				Reset ();
				yield return new WaitForEndOfFrame ();
				break;
			}
		}
	}
	
	private void Init () {
		_highScore = PlayerPrefs.GetInt ("HighScore");
		_scoreText = GameObject.Find ("scoreText").GetComponent<TextMesh> ();
		_highScoreText = GameObject.Find ("highText").GetComponent<TextMesh> ();
		_highScoreText.text = "HIGH SCORE: " + _highScore.ToString ();
		_playerShip = GameObject.Find ("playerShip");
		_title = GameObject.Find ("_title");
		//_playerShip.SetActive (false);
		_state = GameState.Idle;
	}
	
	private void Idle () {
		if (_title.activeSelf) {
			//_playerShip.SetActive (true);
			_title.SetActive (false);
		}
		_state = GameState.Spawn;
	}

	private void ActualSpawn () {
		Vector3 tPos = new Vector3 (Random.Range (.1f, .9f), 1.15f, 0);
		int randEn = Random.Range(0, 7);
		var newEnemy = Instantiate(enemy[randEn], this.gameObject.transform.position, Quaternion.identity);
		newEnemy.transform.parent = gameObject.transform;
	}
	
	private void Spawn () {
		if (Random.Range (0, 100) < _beltChance) {
			int beltSize = Random.Range (_beltMin, _beltMax);
			for (int i = 0; i < beltSize; i++) {
				ActualSpawn ();
                //PoolMaster.SpawnRandom (new string[]{"s"}, tPos);
            }
		} else {
			ActualSpawn ();
		}
		_state = GameState.Wait;
	}
	
	private void Wait () {
		if (Random.Range (0, 100) < _barrageChance) 
			_state = GameState.Barrage;
		else
			_state = GameState.Spawn;
	}
	
	private void Barrage () {
		if (Random.Range (0, 100) < _beltChance) {
			int beltSize = Random.Range (_beltMin, _beltMax);
			for (int i = 0; i < beltSize; i++) {
				ActualSpawn ();
			}
		} else {
			ActualSpawn ();
		}
		_state = GameState.BarrageWait;
	}
	
	private void BarrageWait () {
		if (_barrageTimer < _barrageLength) {
			_barrageTimer += _barrageFrequency;
			_state = GameState.Barrage;
		} else {
			_barrageTimer = 0;
			_state = GameState.Wait;
		}
	}
	
	private void Reset () {
		
	}
	
	public void ModifyScore (int amt) {
		_score += amt;
		_scoreText.text = "SCORE: " + _score.ToString ();
		if (_score > _highScore) {
			_highScore = _score;
			_highScoreText.text = "HIGH SCORE: " + _highScore.ToString ();
			PlayerPrefs.SetInt ("HighScore", _highScore);
		}
	}
	
	
}
