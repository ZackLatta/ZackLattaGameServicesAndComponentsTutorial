namespace GameServicesAndComponentsExercise;

public interface IAchievementService
{
    ///<sumarry>
    /// Updates an achievement
    /// </summary>
    /// <param name="achievement">The achievement name</param>
    /// <param name="progress">the achievment progress</param>
    public void UpdateAchievement(string achievement,uint  progress);

}