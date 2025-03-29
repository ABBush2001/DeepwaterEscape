using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource soundEffectSource;  // For single-shot sounds (jump, hurt, shoot, death)
    public AudioSource walkingSource;      // For looping sounds (walking, sprinting)

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip gunShotSound;
    public AudioClip walkSound;
    public AudioClip playerDeathSound;

    [Header("Walking Audio Settings")]
    public float walkSpeed = 1.0f;
    public float sprintSpeedMultiplier = 1.5f;

    private bool isWalking = false;
    private bool isSprinting = false;

    void Start()
    {
        if (walkingSource != null)
        {
            walkingSource.clip = walkSound;
            walkingSource.loop = true;
        }
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlayHurtSound()
    {
        PlaySound(hurtSound);
    }

    public void PlayGunShotSound()
    {
        PlaySound(gunShotSound);
    }

    public void PlayDeathSound()
    {
        PlaySound(playerDeathSound);
    }

    public void StartWalking()
    {
        if (!isWalking)
        {
            isWalking = true;
            UpdateWalkingSound();
            walkingSource.Play();
        }
    }

    public void StopWalking()
    {
        isWalking = false;
        walkingSource.Stop();
    }

    public void StartSprinting()
    {
        isSprinting = true;
        UpdateWalkingSound();
    }

    public void StopSprinting()
    {
        isSprinting = false;
        UpdateWalkingSound();
    }

    private void UpdateWalkingSound()
    {
        if (walkingSource.isPlaying)
        {
            walkingSource.pitch = isSprinting ? sprintSpeedMultiplier : 1.0f;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            soundEffectSource.PlayOneShot(clip);
        }
    }
}
