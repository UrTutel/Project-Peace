using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public GameObject choicePanel;
    public GameObject continueButton;
    public TMP_Text dialogueText;
    public Button acceptButton;
    public Button declineButton;
    public TMP_Text nameText;
    public Image portraitImage;

    [Header("Settings")]
    public float typingSpeed = 0.05f;

    private DialogueData currentDialogue;
    private int currentLine = 0;
    private System.Action onAccept;
    private System.Action onDecline;
    private System.Action onComplete;
    private bool waitingForChoice = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(DialogueData dialogue, 
                           System.Action onAccepted = null, 
                           System.Action onDeclined = null,
                           System.Action onCompleted = null)
    {
        currentDialogue = dialogue;
        currentLine = 0;
        onAccept = onAccepted;
        onDecline = onDeclined;
        onComplete = onCompleted;

        // Setup NPC info
        nameText.text = currentDialogue.npcName;
        portraitImage.sprite = currentDialogue.npcPortrait;

        dialoguePanel.SetActive(true);
        continueButton.SetActive(true);
        choicePanel.SetActive(false);
        waitingForChoice = false;

        InteractUIManager.instance.HideButton();
        ShowLine();
    }

    public void ShowNextLine()
    {
        if (waitingForChoice || isTyping) return;

        currentLine++;

        if (currentLine < currentDialogue.lines.Length)
        {
            ShowLine();
        }
        else
        {
            ShowChoices();

        }
    }

    void ShowLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        
        typingCoroutine = StartCoroutine(TypeText(currentDialogue.lines[currentLine]));
    }

    System.Collections.IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void ShowChoices()
    {
        waitingForChoice = true;
        continueButton.SetActive(false);
        choicePanel.SetActive(true);

        acceptButton.onClick.RemoveAllListeners();
        declineButton.onClick.RemoveAllListeners();

        acceptButton.onClick.AddListener(() => {
            EndDialogue();
            onAccept?.Invoke();
        });

        declineButton.onClick.AddListener(() => {
            EndDialogue();
            onDecline?.Invoke();
        });
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        continueButton.SetActive(false);
        waitingForChoice = false;
        currentDialogue = null;

        // Show interaction UI again when dialogue ends
        InteractUIManager.instance.ShowButton(null);
    }

    // Quick dialogue message without choices
    public void ShowMessage(string message, float duration = 3f)
    {
        StartDialogue(CreateTempDialogue(message), onCompleted: () => {
            // Optional: Add any post-message logic
        });
        
        if (duration > 0)
        {
            Invoke("EndDialogue", duration);
        }
    }

    DialogueData CreateTempDialogue(string message)
    {
        var tempDialogue = ScriptableObject.CreateInstance<DialogueData>();
        tempDialogue.lines = new string[] { message };
        tempDialogue.hasChoices = false;
        return tempDialogue;
    }
}