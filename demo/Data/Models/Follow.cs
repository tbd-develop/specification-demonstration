namespace demo.Data.Models;

public class Follow
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FollowsUserId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual User FollowsUser { get; set; } = null!;
}