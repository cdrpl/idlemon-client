public class SignInResponse : HttpResponse
{
    public string token;
    public User user;
    public Campaign campaign;
    public DailyQuestProgress[] dailyQuestProgress;
    public Resource[] resources;
    public Unit[] units;
    public UnitTemplate[] unitTemplates;
}
