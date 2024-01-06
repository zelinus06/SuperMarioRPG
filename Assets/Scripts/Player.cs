using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public CapsuleCollider2D capsuleCollider { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }
    public Image heartImage;
    public TextMeshProUGUI healthText;   
    public float maxHealth = 3f;
    private float currentHealth;
    public float currentExp = 0f;
    private bool canUseSkill = true;
    public float cooldownTime = 10f;
    public bool isUnlockedskill = false;
    public void Start() {
        if (PlayerPrefs.GetFloat("EXP")>0)
        {
            
            currentExp = PlayerPrefs.GetFloat("EXP");
            Debug.Log(currentExp);
        }
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isUnlockedskill) {
            if (canUseSkill) {
                Starpower();
                StartCoroutine(Cooldown());
            }
        }
        if (currentExp > 3) {
            isUnlockedskill = true;
        }
    }

    void UpdateHealthBar()
    {
        heartImage.fillAmount = currentHealth / maxHealth;
        healthText.text = Mathf.RoundToInt(currentHealth).ToString();
    }

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
    }

    public void Hit()
    {
        if (!dead && !starpower)
        {
            if (big) {
                Shrink();
            } else {
                TakeDamage(1f);

            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        // GameManager.Instance.ResetLevel(3f);
        float delayBeforeTransition = 3f;
        Invoke("LoadStartScene", delayBeforeTransition);
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    public void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;
        float elapsed = 0f;
        float duration = 2f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
        
    }
    private void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    
   public void HitGoomba() {
    currentExp += 1f;
    PlayerPrefs.SetFloat("EXP", currentExp);
    PlayerPrefs.Save();
    Debug.Log(PlayerPrefs.GetFloat("EXP"));
    }
    IEnumerator Cooldown()
    {
        canUseSkill= false;
        yield return new WaitForSeconds(cooldownTime);
        canUseSkill = true;
    }
}
