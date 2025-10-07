using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles popup display and button interactions.
/// Supports both synchronous and asynchronous callbacks.
/// </summary>
public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private Button buttonPrefab;

    private readonly List<Button> activeButtons = new();
    private readonly Queue<Action> popupQueue = new();
    private bool isShowing;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Hide();
    }

    /// <summary>
    /// Displays a popup with mixed sync/async buttons.
    /// </summary>
    public void ShowPopup(string title, string message,
        List<(string label, Action syncCallback, Func<Task> asyncCallback)> buttons)
    {
        if (isShowing)
        {
            popupQueue.Enqueue(() => ShowPopup(title, message, buttons));
            return;
        }

        isShowing = true;
        ClearButtons();

        titleText.text = title;
        messageText.text = message;

        foreach (var (label, sync, async) in buttons)
        {
            var btn = Instantiate(buttonPrefab, buttonParent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = label;

            btn.onClick.AddListener(async () =>
            {
                try
                {
                    // Run async or sync callback depending on type
                    if (async != null)
                        await async.Invoke();
                    else
                        sync?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[PopupManager] Button callback error: {ex}");
                }
                finally
                {
                    Hide();
                }
            });

            activeButtons.Add(btn);
        }

        container.SetActive(true);
    }

    public void Hide()
    {
        container.SetActive(false);
        isShowing = false;
        ClearButtons();

        if (popupQueue.Count > 0)
        {
            var next = popupQueue.Dequeue();
            next?.Invoke();
        }
    }

    private void ClearButtons()
    {
        foreach (var b in activeButtons)
            Destroy(b.gameObject);
        activeButtons.Clear();
    }
}
