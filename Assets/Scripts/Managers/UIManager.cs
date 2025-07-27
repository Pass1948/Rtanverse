using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Canvas windowCanvas;
    private Canvas inGameCanvas;
    private Canvas popUpCanvas;
    private Canvas TextCanvas;
    private Stack<PopUpUI> popUpStack;

    private void Awake()
    {
        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        popUpCanvas.gameObject.name = "PopUpCanvas";
        popUpCanvas.sortingOrder = 100;

        popUpStack = new Stack<PopUpUI>();

        windowCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        windowCanvas.gameObject.name = "WindowCanvas";
        windowCanvas.sortingOrder = 10;

        inGameCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        inGameCanvas.gameObject.name = "InGameCanvas";
        inGameCanvas.sortingOrder = 3;

        TextCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        TextCanvas.gameObject.name = "TextCanvas";
        TextCanvas.sortingOrder = 5;
    }
    public void Recreated()
    {
        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        popUpCanvas.gameObject.name = "PopUpCanvas";
        popUpCanvas.sortingOrder = 100;

        popUpStack = new Stack<PopUpUI>();

        windowCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        windowCanvas.gameObject.name = "WindowCanvas";
        windowCanvas.sortingOrder = 10;

        inGameCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        inGameCanvas.gameObject.name = "InGameCanvas";
        inGameCanvas.sortingOrder = 0;

        TextCanvas = GameManager.Resource.Instantiate<Canvas>("Prefabs/UI/Canvas");
        TextCanvas.gameObject.name = "TextCanvas";
        TextCanvas.sortingOrder = 5;
    }
    public void Clear()
    {
        popUpStack.Clear();
        GameManager.Resource.Destroy(windowCanvas);
        GameManager.Resource.Destroy(inGameCanvas);
        GameManager.Resource.Destroy(TextCanvas);
    }

    public T ShowPopUpUI<T>(T popUpUI) where T : PopUpUI
    {
        if (popUpStack.Count > 0)
        {
            PopUpUI prevUI = popUpStack.Peek();
            prevUI.gameObject.SetActive(false);
        }

        T ui = GameManager.Pool.GetUI(popUpUI);
        ui.transform.SetParent(popUpCanvas.transform, false);

        popUpStack.Push(ui);

        return ui;
    }

    public T ShowPopUpUI<T>(string path) where T : PopUpUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowPopUpUI(ui);
    }

    public void ClosePopUpUI()
    {
        PopUpUI ui = popUpStack.Pop();
        GameManager.Pool.Release(ui.gameObject);

        if (popUpStack.Count > 0)
        {
            PopUpUI curUI = popUpStack.Peek();
            curUI.gameObject.SetActive(true);
        }
    }


    public T ShowWindowUI<T>(T windowUI) where T : WindowUI
    {
        T ui = GameManager.Pool.GetUI(windowUI);
        ui.transform.SetParent(windowCanvas.transform, false);
        return ui;
    }

    public T ShowWindowUI<T>(string path) where T : WindowUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowWindowUI(ui);
    }

    public void CloseWindowUI(WindowUI windowUI)
    {
        GameManager.Pool.ReleaseUI(windowUI.gameObject);
    }

    public void SelectWindowUI<T>(T windowUI) where T : WindowUI
    {
        windowUI.transform.SetAsLastSibling();
    }

    public T ShowInGameUI<T>(T inGameUI) where T : InGameUI
    {
        T ui = GameManager.Pool.GetUI(inGameUI);
        ui.transform.SetParent(inGameCanvas.transform.transform, false);
        return ui;
    }

    public T ShowInGameUI<T>(string path) where T : InGameUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowInGameUI(ui);
    }

    public void ColseInGameUI(InGameUI inGameUI)
    {
        GameManager.Pool.ReleaseUI(inGameUI);
    }

    public T ShowPopUpTextUI<T>(T popUpTextUI) where T : PopUpTextUI
    {
        T ui = GameManager.Pool.GetUI(popUpTextUI);
        ui.transform.SetParent(inGameCanvas.transform.transform, false);
        return ui;
    }

    public T ShowPopUpTextUI<T>(string path) where T : PopUpTextUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowPopUpTextUI(ui);
    }

    public void ColsePopUpTextUI(PopUpTextUI popUpTextUI)
    {
        GameManager.Pool.ReleaseUI(popUpTextUI);
    }
}
