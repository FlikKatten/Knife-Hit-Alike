using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public int force;
    private bool isActive;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Vector2 firstPosition;

    public GameObject knifePrefab, target;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        firstPosition = transform.position;
        isActive = true;
        rigidBody.isKinematic = false;
        boxCollider.isTrigger = true;

        //ENCURTANDO COLLIDER PARA NÃO HAVER MAIS COLISÃO ENTRE TARGET E KNIFE
        boxCollider.offset = new Vector2(boxCollider.offset.x, -0.1f);
        boxCollider.size = new Vector2(boxCollider.size.x, 1.7f);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && isActive)
        {
            boxCollider.isTrigger = false;
            rigidBody.AddForce(Vector2.up * force * 10);
            StartCoroutine(InstantiateKnife());
        }

        if(name == "CloneKnife")
        {
            //HERDANDO FÍSICA DO OBJETO COLIDIDO
            rigidBody.isKinematic = true;
            transform.SetParent(target.GetComponent<Collider>().transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isActive) { return; }

        isActive = false;

        switch (other.gameObject.tag)
        {
            case "Target":

                if (!UI.isGameWon)
                {
                    UI.score++;
                    UI.currentScore++;
                }

                rigidBody.velocity = Vector2.zero;

                //HERDANDO FÍSICA DO OBJETO COLIDIDO
                rigidBody.isKinematic = true;
                transform.SetParent(other.collider.transform);

                //ENCURTANDO COLLIDER PARA NÃO HAVER MAIS COLISÃO ENTRE TARGET E KNIFE
                boxCollider.offset = new Vector2(boxCollider.offset.x, -0.4f);
                boxCollider.size = new Vector2(boxCollider.size.x, 1.2f);
                break;
            case "Knife":
                rigidBody.gravityScale = 1; //DEFININDO O VALOR DA GRAVIDADE
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -2);
                UI.isGameOver = true;
                break;
        }
    }

    IEnumerator InstantiateKnife()
    {
        yield return new WaitForSeconds(0.1f);

        Instantiate(knifePrefab, firstPosition, Quaternion.identity);
    }
}