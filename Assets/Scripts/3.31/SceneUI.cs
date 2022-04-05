using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{

    enum Buttons
    {
        AttackButton,
        InventoryButton
    }
    enum Texts
    {
        GameRoundText,
        GameOverText
    }
    enum Images
    {
        PlayerHpBar,
        EnemyHpBar,
        GameOverPanel,
        HealImage
    }

    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn;
    private Character _player;
    private Character _enemy;

    private bool _isClicked = false;


    private void Start()
    {
        Init();
        _player = GameManager.Instance().GetCharacter("Player");
        _enemy = GameManager.Instance().GetCharacter("Enemy");
        GameManager.Instance().AddUI(this);
        GameManager.Instance().InitNotify();
        GetImage((int)Images.HealImage).gameObject.SetActive(false);
        GetImage((int)Images.GameOverPanel).gameObject.SetActive(false);
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClick_AttackButton);
        GetButton((int)Buttons.InventoryButton).gameObject.BindEvent(OnClick_InventoryButton);
    }

    public void OnClick_AttackButton(PointerEventData data)
    {
        if (!_isClicked)
        {
            _isClicked = true;
            GameManager.Instance().RoundNotify();
            GameRoundText();
            _player.Attack();
            _enemy.Attack();
            StartCoroutine(GetDamageCoroutine());
        }
    }

    public void OnClick_InventoryButton(PointerEventData data)
    {
        if (_whoseTurn.Equals("Enemy"))
        {
            UIManager.UI.ShowPopupUI<UIPopup>("Inventory");
        }
    }


    public void GameRoundText()
    {
        GetText((int)Texts.GameRoundText).text = "GameRound" + _gameRound;
    }

    public void CharacterHp()
    {
        GetImage((int)Images.PlayerHpBar).fillAmount = _player._myHp / _player._myHpMax;
        GetImage((int)Images.EnemyHpBar).fillAmount = _enemy._myHp / _enemy._myHpMax;
    }

    IEnumerator GetDamageCoroutine()
    {
        GetButton((int)Buttons.AttackButton).interactable = false;
        if (_whoseTurn.Equals("Player")) GetButton((int)Buttons.InventoryButton).interactable = false;
        yield return new WaitForSeconds(1.2f);
        CharacterHp();
        float beforeHp = _enemy._myHp;
        yield return new WaitForSeconds(1.2f);
        GameEnd();
        float afterHp = _enemy._myHp;
        CharacterHp();
        if (beforeHp != afterHp)
        {
            GetImage((int)Images.HealImage).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.65f);
            GetImage((int)Images.HealImage).gameObject.SetActive(false);
        }
        GetButton((int)Buttons.AttackButton).interactable = true;
        if (_whoseTurn.Equals("Enemy")) GetButton((int)Buttons.InventoryButton).interactable = true;
        _isClicked = false;
    }

    public void GameEnd()
    {
        if (_isEnd)
        {
            GetImage((int)Images.GameOverPanel).gameObject.SetActive(true);
            GetText((int)Texts.GameOverText).text = "Gameover!!\n" + _whoseTurn + " Win!!";
        }
    }

    public void UIUpdate(int round, string turn, bool isFinish)
    {
        this._gameRound = round;
        this._whoseTurn = turn;
        this._isEnd = isFinish;
    }
}
