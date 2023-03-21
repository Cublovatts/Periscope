public class Quest
{
    private QuestManager.QuestEnum questEnum;
    private int currentProgress;
    private string questName;
    private string[] questStepDescriptions;

    public Quest(QuestManager.QuestEnum questEnum, string questName, string[] questStepDescriptions)
    {
        this.questEnum = questEnum;
        this.questName = questName;
        this.questStepDescriptions = questStepDescriptions;
        currentProgress = 0;
    }

    public QuestManager.QuestEnum GetQuestEnum() 
    { 
        return questEnum; 
    }

    public string GetName()
    {
        return questName;
    }

    public int GetProgress()
    {
        return currentProgress;
    }

    public void SetProgress(int progress)
    {
        currentProgress = progress;
    }

    public string GetCurrentProgressDescription()
    {
        return questStepDescriptions[currentProgress];
    }
}
