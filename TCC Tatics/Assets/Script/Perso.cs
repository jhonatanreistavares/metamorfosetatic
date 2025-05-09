using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator persoAnimator;
    
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }
    
    private void Update()
    {
       
        //para o personagem não ficar flikando quando chega próximo do targetPosition, adicionei um float para previnir que isso não aconteça
        float parada = .1f;
        if(Vector3.Distance(transform.position, targetPosition) > parada)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            
            
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            persoAnimator.SetBool("IsWalking", true);
        }
        else
        {
             persoAnimator.SetBool("IsWalking", false);
        }
        
    }

    //move o personagem para o targetPosition(Click do Mouse)
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}

