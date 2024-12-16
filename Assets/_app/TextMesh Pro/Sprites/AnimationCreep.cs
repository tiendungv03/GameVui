using UnityEngine;

public class AnimationCreep : MonoBehaviour
{
    private Animator animator;
    private string currentAnimation = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimation(string animation, float crossFade = 0.2f)
    {
        if (currentAnimation != animation) 
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossFade);
        }
    }
}
