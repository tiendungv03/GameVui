using TMPro;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    protected TextMeshProUGUI interactText;
    protected KeyCode interact = KeyCode.F;
    protected bool isPlayerInRange = false; // Kiểm tra xem người chơi có gần hộp máu không
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected virtual void Awake()
    {
        /*isPlayerInRange =false;
        var interactObj = GameObject.FindGameObjectWithTag("InteractText");
        if (interactObj == null)
        {
            Debug.LogWarning("Ko co text tuong tac");
        }
        else
            interactText = GameObject.FindGameObjectWithTag("InteractText").GetComponent<TextMeshProUGUI>();
        ActiveInteractText(isPlayerInRange);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu đối tượng là hộp máu
        {
            isPlayerInRange = true; // Người chơi đã đến gần hộp máu
            /*ActiveInteractText(isPlayerInRange);*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Người chơi không còn gần hộp máu
            /*ActiveInteractText(isPlayerInRange);*/
        }
    }

    public void ActiveInteractText(bool isPlayerInRange)
    {
        if (interactText == null)
        {
            return;
        }

        if (isPlayerInRange)
        {             
            interactText.gameObject.SetActive(true);
            interactText.text = "|F| Pick up";
        }
        else
        {
            interactText.gameObject.SetActive(false);
            interactText.text = "";
        }
    }
}
