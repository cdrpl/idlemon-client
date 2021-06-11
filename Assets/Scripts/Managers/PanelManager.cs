using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }

    /// <summary>
    /// List of panels to be registered on start.
    /// </summary>
    public Panel[] panels;

    Dictionary<string, Panel> registeredPanels = new Dictionary<string, Panel>();
    Stack<Panel> history = new Stack<Panel>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && history.Count > 1)
        {
            AudioManager.Instance.Play("button");
            ChangePanelPrevious();
        }
    }

    void Init()
    {
        if (panels.Length == 0)
        {
            Debug.LogWarning("Panel manager has no panels", this);
        }

        foreach (var panel in panels)
        {
            if (registeredPanels.ContainsKey(panel.name))
            {
                Debug.LogWarning("Panel has been registered twice: " + panel.name, this);
            }

            RegisterPanel(panel);
        }
    }

    public void RegisterPanel(Panel panel)
    {
        registeredPanels[panel.panelName] = panel;
    }

    /// <summary>
    /// Will close the current panel and open the specified one.
    /// </summary>
    public void ChangePanel(Panel panel)
    {
        ChangePanel(panel.panelName);
    }

    /// <summary>
    /// Will close the current panel and open the specified one.
    /// </summary>
    public void ChangePanel(string name)
    {
        if (registeredPanels.ContainsKey(name))
        {
            if (history.Count > 0)
            {
                Panel cur = history.Peek();
                cur.Hide();
            }

            Panel to = registeredPanels[name];
            history.Push(to);
            to.Show();
        }
        else
        {
            Debug.LogWarning("Attempt to change to unknown panel: " + name, this);
        }
    }

    /// <summary>
    /// Will close the currently open panel and open the previous one.
    /// </summary>
    public void ChangePanelPrevious()
    {
        Panel prev = history.Pop();
        prev.Hide();

        Panel cur = history.Peek();
        cur.Show();
    }
}
