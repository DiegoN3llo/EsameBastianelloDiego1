using UnityEngine;

public class LogicaCursore : MonoBehaviour
{

    public Transform centerPoint; 
    public float rotationSpeed = 35f;
    public Transform cursorsContainerTransform;
    public float ProductionTimer = 2f;
    float timer = 0f;
    void Start()
    {
        centerPoint = GameObject.Find("Biscottone").transform;
    }
    void Update()
    {
        
        transform.RotateAround(centerPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        transform.up = centerPoint.position-transform.position ;
        timer += Time.deltaTime;
        produceMoney();
    }

    void cambiocolore()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color=Color.red;
        Invoke("resetcolore", 0.1f);
    }

    void resetcolore()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }
    void produceMoney()
    {
        if (timer >= ProductionTimer)
        {
            
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                cambiocolore();
                gameManager.cookies += 0.4f;
                gameManager.UiUpdate();
            }
            timer -= ProductionTimer;
            
        }
    }

}
