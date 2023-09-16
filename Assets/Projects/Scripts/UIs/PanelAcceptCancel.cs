using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class PanelAcceptCancel : MonoBehaviour
{
    [SerializeField] private TMP_Text textInfo;
    [SerializeField] private Button accept;
    [SerializeField] private Button cancel;

    private Action onAccept;
    private Action onCancel;
    private void Start()
    {
        accept.onClick.AddListener(OnAcceptButtonClick);
        cancel.onClick.AddListener(OnCancelButtonClick);
    }
    public void OpenEdgePanel(string info,Action onAccept,Action onCancel)
    {
        gameObject.SetActive(true);
        textInfo.text = info;
        this.onAccept = onAccept;
        this.onCancel = onCancel;
    }
    private void OnAcceptButtonClick()
    {
        onAccept?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnCancelButtonClick()
    {
        onCancel?.Invoke();
        gameObject.SetActive(false);
    }
}
