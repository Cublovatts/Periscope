public class Quest
{
    private int currentProgress;
    private string questName;
    private string[] questStepDescriptions;

    public Quest(string questName, string[] questStepDescriptions)
    {
        this.questName = questName;
        this.questStepDescriptions = questStepDescriptions;
        currentProgress = 0;
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
}
