using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsScreenUI : MonoBehaviour
{
    public TextMeshProUGUI damageDealtText;
    public TextMeshProUGUI damageTakenText;
    public Button backToMenuButton;
    public CanvasGroup canvasGroup;
    public Animator playerAnimator;
    public PlayerStats playerStats; 
    public Damagable playerDamagable;

    private void Start()
    {
        backToMenuButton.onClick.AddListener(BackToMenu);
        Hide();

        // Проверка ссылок перед подпиской
        if(playerStats != null)
        {
            playerStats.OnStatsUpdated += UpdateStats;
        }
        else
        {
            Debug.LogError("PlayerStats reference is missing!");
        }

        // Подписываемся на событие смерти игрока
        if(playerDamagable != null)
        {
            playerDamagable.dmgDeath.AddListener(OnPlayerDeath);
        }
        else
        {
            Debug.LogError("PlayerDamagable reference is missing!");
        }
    }

    private void OnPlayerDeath()
    {
        ShowStats();
        Time.timeScale = 0f; // Ставим игру на паузу
        Cursor.visible = true; // Показываем курсор
        Cursor.lockState = CursorLockMode.None; // Разблокируем курсор
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Восстанавливаем нормальное время
        SceneManager.LoadScene("Menu");
        playerStats.ResetStats();
    }

    public void ShowStats()
    {
        Time.timeScale = 1f;

        if (playerAnimator != null)
            playerAnimator.SetBool("lockVelocity", true);

        UpdateStats();

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void UpdateStats()
    {
        damageDealtText.text = "Damage Dealt: " + playerStats.damageDealt;
        damageTakenText.text = "Damage Taken: " + playerStats.damageTaken;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        playerStats.OnStatsUpdated -= UpdateStats;
    }
}