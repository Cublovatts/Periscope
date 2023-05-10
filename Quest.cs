public class Quest
{
    private readonly QuestManager.QuestEnum _questEnum;
    private int _currentProgress;
    private readonly string _questName;
    private readonly string[] _questStepDescriptions;

    public Quest(QuestManager.QuestEnum questEnum, string questName, string[] questStepDescriptions)
    {
        this._questEnum = questEnum;
        this._questName = questName;
        this._questStepDescriptions = questStepDescriptions;
        _currentProgress = 0;
    }

    public QuestManager.QuestEnum GetQuestEnum() 
    { 
        return _questEnum; 
    }

    public string GetName()
    {
        return _questName;
    }

    public int GetProgress()
    {
        return _currentProgress;
    }

    public void SetProgress(int progress)
    {
        _currentProgress = progress;
    }

    public string GetCurrentProgressDescription()
    {
        return _questStepDescriptions[_currentProgress];
    }
}
