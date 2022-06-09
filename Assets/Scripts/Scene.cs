using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour {

	public void LoadScene (int sceneId) {
		SceneManager.LoadScene (sceneId); 
	}

	public void Exit () {
		Application.Quit ();
	}
}
