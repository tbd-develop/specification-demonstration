using demo.Infrastructure;

namespace demo;

public class DoWorkWithLargeService
{
    private readonly LargeService _service;

    public DoWorkWithLargeService(LargeService service)
    {
        _service = service;
    }

    public async Task GetUsersWithFollowerCount()
    {
        var users = await _service.GetUsers();

        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name} has {user.Followers} followers");
        }
    }
}