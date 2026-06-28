using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
  [SerializeField] private CanvasGroup WinUI;
  [SerializeField] private Button restartButton;
  [SerializeField] private LayerMask playerMask;

  private void Start()
  {
    restartButton.onClick.AddListener(Restart);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (!LayerMaskUtil.ContainsLayer(playerMask, other.gameObject)) return;
    WinUI.gameObject.SetActive(true);
    Time.timeScale = 0;
  }

  private void Restart()
  {
    Time.timeScale = 1;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
