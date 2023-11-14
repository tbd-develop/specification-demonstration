using demo.Infrastructure;

namespace demo;

public class DoWorkWithLargeService
{
    private readonly LargeService _service;

    public DoWorkWithLargeService(LargeService service)
    {
        _service = service;
    }

    /// <summary>
    /// This works because inside of GetUsers we are using the ToListAsync, which enumerates the results into memory
    /// </summary>
    public async Task GetUsersWithFollowerCount()
    {
        var users = await _service.GetUsers();

        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name} has {user.Followers} followers");
        }
    }
    
    /// <summary>
    /// This fails because we are not using ToListAsync, and the context is disposed before the results are enumerated.
    /// Enumeration happens here at line 36, but the context is disposed when GetUsersNoToList returns at line 44
    /// </summary>
    public async Task GetUsersWithFollowerCountNoToList()
    {
        try
        {
            var users = await _service.GetUsersNoToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} has {user.Followers} followers");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
    
    
    /// <summary>
    /// This works because we never dispose of the context inside of GetUsersNoDispose. This is bad because we are leaking the memory allocated
    /// to the context, and we are not releasing the connection back to the connection pool. This will eventually cause the application to crash. 
    /// </summary>
    public async Task GetUsersWithFollowerCountNoDispose()
    {
        var users = await _service.GetUsersNoDispose();

        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name} has {user.Followers} followers");
        }
    }
}