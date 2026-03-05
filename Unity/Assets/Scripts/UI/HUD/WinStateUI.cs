using UnityEngine;
using UnityEngine.UI;

public class WinStateUI : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public Text objectiveText;

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void ShowWinState()
    {
        winPanel.SetActive(true);
        losePanel.SetActive(false);
    }

    public void ShowLoseState()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(true);
    }
}
