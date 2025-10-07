using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// A fluent builder class for creating and displaying popups dynamically.
/// Supports both synchronous (Action) and asynchronous (Func<Task>) callbacks.
/// </summary>
public class PopupBuilder
{
    private string title;
    private string message;

    // Supports both sync and async buttons
    private readonly List<(string label, Action syncCallback, Func<Task> asyncCallback)> buttons = new();

    private PopupBuilder() { }

    /// <summary>
    /// Creates a new popup with the given title.
    /// </summary>
    public static PopupBuilder Create(string title)
    {
        var builder = new PopupBuilder();
        builder.title = title;
        return builder;
    }

    /// <summary>
    /// Sets the popup message body.
    /// </summary>
    public PopupBuilder SetMessage(string message)
    {
        this.message = message;
        return this;
    }

    /// <summary>
    /// Adds a normal (synchronous) button to the popup.
    /// </summary>
    public PopupBuilder AddButton(string label, Action callback)
    {
        buttons.Add((label, callback, null));
        return this;
    }

    /// <summary>
    /// Adds an asynchronous button to the popup.
    /// </summary>
    public PopupBuilder AddAsyncButton(string label, Func<Task> asyncCallback)
    {
        buttons.Add((label, null, asyncCallback));
        return this;
    }

    /// <summary>
    /// Displays the popup using the PopupManager instance.
    /// </summary>
    public void Show()
    {
        if (PopupManager.Instance == null)
        {
            UnityEngine.Debug.LogError("[PopupBuilder] No PopupManager found in the scene!");
            return;
        }

        PopupManager.Instance.ShowPopup(title, message, buttons);
    }
}