using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Bird;
    private SpriteRenderer spriteRenderer;
    public Sprite[] StartSprite;
    public Sprite[] sprites;
    public static int spriteIndex;

 
    private static int selectedSkins = 0;
    public GameObject playerskin;

    public float VelocityPerJump = 3f;
    public float RotateUpSpeed = 1, RotateDownSpeed = 1;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fly;

    FlappyYAxisTravelState flappyYAxisTravelState;

    enum FlappyYAxisTravelState
    {
        GoingUp, GoingDown
    }
    
    Vector3 birdRotation = Vector3.zero;

    public float flag = 0;
    public static Player instance;
    private GameObject spawner;
    public int countAudioDie = 0;

    private void Awake()
    {
        Bird = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MakeInstance();
        spawner = GameObject.Find("Spawner");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnEnable()
    {
        Vector3 position = Bird.transform.position;
        position.y = 0f;
        Bird.transform.position = position;
    }
    private void Start()
    {
        spriteRenderer.sprite = StartSprite[selectedSkins];
        if (selectedSkins == 0)
        {
            spriteIndex = 1; 
        }
        else if (selectedSkins == 1)
        {
            spriteIndex = 4;
            
        }
        else if (selectedSkins == 2)
        {
            spriteIndex = 7;
           
        }
        else
        {
            spriteIndex = 10;
           
        }
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    public void NextOption()
    {
        selectedSkins++;
        if (selectedSkins == StartSprite.Length)
        {
            selectedSkins = 0;

        }
        spriteRenderer.sprite = StartSprite[selectedSkins];
        
    }
    public void BackOption()
    {
        selectedSkins--;
        if (selectedSkins < 0)
        {
            selectedSkins = StartSprite.Length - 1;
        }

        spriteRenderer.sprite = StartSprite[selectedSkins];
    }


    private void Update()
    {
        
        if(Player.instance.flag == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Bird.velocity = new Vector2(0, VelocityPerJump);
                audioSource.PlayOneShot(fly);

            }


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Bird.velocity = new Vector2(0, VelocityPerJump);
                    audioSource.PlayOneShot(fly);
                }
            }
        }

    }
    private void FixedUpdate()
    {
        FixFlappyRotation();
    }
    private void AnimateSprite()
    {
        

        if (selectedSkins == 0)
        {
            spriteIndex++;
            if (spriteIndex >= 2)
            {
                spriteIndex = 0;
            }
            spriteRenderer.sprite = sprites[spriteIndex];
        }
        else if (selectedSkins == 1)
        {
            spriteIndex++;
            if (spriteIndex >= 5)
            {
                spriteIndex = 3;
            }
            spriteRenderer.sprite = sprites[spriteIndex];
        }
        else if (selectedSkins == 2)
        {
            spriteIndex++;
            if (spriteIndex >= 8)
            {
                spriteIndex = 6;
            }
            spriteRenderer.sprite = sprites[spriteIndex];
        }
        else
        {
            spriteIndex++;
            if (spriteIndex >= 11)
            {
                spriteIndex = 9;
            }
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void FixFlappyRotation()
    {
        if (Bird.velocity.y > 0) 
            flappyYAxisTravelState = FlappyYAxisTravelState.GoingUp;
        else 
            flappyYAxisTravelState = FlappyYAxisTravelState.GoingDown;

        float degreesToAdd = 0;

        switch (flappyYAxisTravelState)
        {
            case FlappyYAxisTravelState.GoingUp:
                degreesToAdd = 6 * RotateUpSpeed;
                break;
            case FlappyYAxisTravelState.GoingDown:
                degreesToAdd = -3 * RotateDownSpeed;
                break;
            default:
                break;
        }
        //solution with negative eulerAngles found here: http://answers.unity3d.com/questions/445191/negative-eular-angles.html

        //clamp the values so that -90<rotation<45 *always*
        birdRotation = new Vector3(0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, -90, 45));
        transform.eulerAngles = birdRotation;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pipe")
        {
            countAudioDie++;
            flag = 1;
            Destroy(spawner);
            FindObjectOfType<GameManager>().GameOver();
            
        }
        if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            countAudioDie++;
            flag = 1;
            Destroy(spawner);
            FindObjectOfType<GameManager>().GameOver();
            
        }
    }
}
