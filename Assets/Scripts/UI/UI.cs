using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UI : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _currentLevelLabel;    

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartGameButtonClick);        
        _exitButton.onClick.AddListener(ExitButtonClick);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(StartGameButtonClick);        
        _exitButton.onClick.RemoveListener(ExitButtonClick);
    }

    private void StartGameButtonClick()
    {       
        SceneManager.LoadScene(0);
    }

    private void ExitButtonClick()
    {
        EditorApplication.isPlaying = false;
    }
}
