using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public float jumpForce;
    public float gravityModifier;
    private bool grounded = true;
    public bool gameOver = false;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound);
            Debug.Log("Game Over");
            dirtParticle.Stop();
            gameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
            explosionParticle.Play();
        }
    }

}
