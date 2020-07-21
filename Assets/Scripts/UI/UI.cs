using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class UI : MonoBehaviour
{
    #region Fields

    [SerializeField] private Button _startGameButton;//кнопка начала игры
    [SerializeField] private Button _exitButton;//кнопка выхода
    [SerializeField] private Button _hpBar;//хп бар
    public GameObject GameUi;//игровой интерфейс 
    public  GameObject Menu;//меню
    public Text MenuText;//текст 

    #endregion


    #region Method

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartGameButtonClick);        
        _exitButton.onClick.AddListener(ExitButtonClick);
        InvokeRepeating(nameof(ShakeText),1.0f,1.0f); //движение текста       
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(StartGameButtonClick);        
        _exitButton.onClick.RemoveListener(ExitButtonClick);
        CancelInvoke(nameof(ShakeText));        
    }

    private void StartGameButtonClick()
    {
        MainController.Instance.InitGame();//при нажатие на старт запускаем игру
    }

    private void ExitButtonClick()
    {
        EditorApplication.isPlaying = false;//выходим из редактора
    }

    public  void ShowCount()//метод показывает здоровье в интерфейсе
    {
        if (MainController.Instance.Character != null)
        {      
            _hpBar.transform.GetChild(0).GetComponent<Text>().text = ((int)MainController.Instance.Character.Hp).ToString();
            _hpBar.image.fillAmount = MainController.Instance.Character.Hp / 100;
            _hpBar.image.color = (MainController.Instance.Character.Hp >= 60) ? Color.green : Color.red;
        }
    }

    private void ShakeText()//движение текста
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

    #endregion

}
