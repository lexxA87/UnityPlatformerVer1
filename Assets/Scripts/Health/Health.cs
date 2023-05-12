using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Health : MonoBehaviour
{
    [Header("Health")]
    [Range(100, 1000)]
    [SerializeField] private int startingHealth;
    public int CurrentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Sounds")]
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _deathSound;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int _damage)
    {
        if (invulnerable) return;
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(_hurtSound);
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;

                SoundManager.instance.PlaySound(_deathSound);
            }
        }
    }
    public void AddHealth(int _value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}