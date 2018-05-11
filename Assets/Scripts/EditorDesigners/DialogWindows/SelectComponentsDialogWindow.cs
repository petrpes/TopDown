using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectComponentsDialogWindow : EditorWindow
{
    private bool _isShowing;
    private bool _isSuccess;

    private List<WindowData> _list;

    private WindowData CurrentData { get { return _list[0]; } }

    public static void ShowWindow<T>(string title, string caption, T[] components,
        Action<T> onSuccess, Action onFailure) where T : class
    {
        ShowWindow(title, caption, Array.ConvertAll(components, (t) =>
        {
            return t as Component;
        }), 
        (comp) => { onSuccess(comp as T); }, onFailure);
    }

    public static void ShowWindow(string title, string caption, Component[] components, 
        Action<Component> onSuccess, Action onFailure)
    {
        if (components == null || components.Length == 0)
        {
            onFailure();
            return;
        }

        if (components.Length == 1)
        {
            onSuccess(components[0]);
            return;
        }

        SelectComponentsDialogWindow window = 
            (SelectComponentsDialogWindow)GetWindow(typeof(SelectComponentsDialogWindow));

        window.Show(title, caption, components, onSuccess, onFailure);
    }

    public void Show(string title, string caption, Component[] components,
        Action<Component> onSuccess, Action onFailure)
    {
        if (_list == null)
        {
            _list = new List<WindowData>();
        }

        _list.Add(new WindowData()
        {
            Title = title,
            Caption = caption,
            Components = components,
            Texts = Array.ConvertAll(components, (comp) =>
            {
                return comp.gameObject.name + " " + comp.GetType().Name;
            }),
            OnSuccess = onSuccess,
            OnFailure = onFailure,
            CurrentPosition = 0
        });

        if (!_isShowing)
        {
            _isShowing = true;
            ShowPopup();
        }
    }

    private void OnGUI()
    {
        _isSuccess = false;

        GUILayout.Label(CurrentData.Caption);
        CurrentData.CurrentPosition = GUILayout.SelectionGrid(CurrentData.CurrentPosition, CurrentData.Texts, 1);

        if (GUILayout.Button("OK"))
        {
            CurrentData.OnSuccess.SafeInvoke(CurrentData.Components[CurrentData.CurrentPosition]);
            _isSuccess = true;
            OnBeforeClose();
        }

        if (GUILayout.Button("Cancel"))
        {
            OnBeforeClose();
        }
    }

    private void OnBeforeClose()
    {
        if (!_isSuccess)
        {
            CurrentData.OnFailure.SafeInvoke();
        }

        _list.RemoveAt(0);

        if (_list.Count <= 0)
        {
            _isShowing = false;
            Close();
        }
    }

    private class WindowData
    {
        public string Title;
        public string Caption;
        public Component[] Components;
        public string[] Texts;
        public Action<Component> OnSuccess;
        public Action OnFailure;
        public int CurrentPosition;
    }
}

