using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public MenuManager MenuManager;
    public TextMeshProUGUI LevelText;
    private int sceneNumber;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneNumber = int.Parse(currentScene.name[currentScene.name.Length - 1].ToString());
        LevelText.text = "Level " + sceneNumber;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Ball")
            {
                if(sceneNumber == 9)
                {
                    MenuManager.LoadSceneByName("Menu");
                }
                MenuManager.LoadSceneByName("Level" + (sceneNumber + 1));
            }
        }
    }
}
