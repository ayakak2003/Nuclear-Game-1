using UnityEngine;
using DialogueEditor;

public class DialogueTrigger : MonoBehaviour
{
    public NPCConversation _conversation;

    public void StartDialogue()
    {
        if (_conversation == null)
        {
            Debug.LogError("No conversation assigned!");
            return;
        }

        // 1. Start the conversation 
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(_conversation);
        }

        // 2. NOW sync the parameters.
        SyncParameters();
    }

    public void SyncParameters()
    {
        var manager = DialogueParameterManager.Instance;
        if (manager == null) return;

        for (int i = 0; i < manager.paramNames.Length; i++)
        {
            // Use the manager's data to update the active conversation
            ConversationManager.Instance.SetBool(manager.paramNames[i], manager.parameters[i]);
        }
    }

    // Call this from a Dialogue Event within the Editor to save a choice
    public void SetGlobalBoolTrue(int index)
    {
        DialogueParameterManager.Instance.parameters[index] = true;
    }
    public void SetGlobalBoolFalse(int index)
    {
        DialogueParameterManager.Instance.parameters[index] = false;
    }
    public void EndConversation()
    {
        // Safety: Force close dialogue if player walks away
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.EndConversation();
        }
    }
}