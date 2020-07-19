using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class UI : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _hpBar;
    public GameObject GameUi;
    public  GameObject Menu;
    public Text MenuText;    

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartGameButtonClick);        
        _exitButton.onClick.AddListener(ExitButtonClick);
        InvokeRepeating(nameof(ShakeText),1.0f,1.0f);        
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(StartGameButtonClick);        
        _exitButton.onClick.RemoveListener(ExitButtonClick);
        CancelInvoke(nameof(ShakeText));        
    }

    private void StartGameButtonClick()
    {
        MainController.Instance.InitGame();
    }

    private void ExitButtonClick()
    {
        EditorApplication.isPlaying = false;
    }

    public  void ShowCount()
    {
        if (MainController.Instance.Character != null)
        {      
            _hpBar.transform.GetChild(0).GetComponent<Text>().text = ((int)MainController.Instance.Character.Hp).ToString();
            _hpBar.image.fillAmount = MainController.Instance.Character.Hp;
            _hpBar.image.color = (MainController.Instance.Character.Hp >= 0.6) ? Color.green : Color.red;
        }
    }

    private void ShakeText()
    {
        var duration = 0.5f;
        var interval = 0.5f;
        Sequence sequence = DOTween.Sequence().OnStart(() => { MenuText.transform.DORotate(new Vector3(0.0f, 0.0f, 50f), duration); }).
            AppendInterval(interval).
            Append(MenuText.transform.DORotate(new Vector3(0.0f, 0.0f, 50f), duration)).SetEase(Ease.InBack).
            AppendInterval(interval).
            Join(MenuText.transform.DORotate(new Vector3(0.0f, 0.0f, -50f), duration)).SetEase(Ease.OutBounce);
        sequence.Play();
    }
}
