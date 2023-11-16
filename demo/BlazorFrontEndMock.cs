using System.Diagnostics;
using demo.Infrastructure;

namespace demo;

public class BlazorFrontEndMock
{
    private readonly LargeService _largeService;
    private IQueryable<UserData> _users = null!;
    private IQueryable<UserData> _filteredUsers = null!;

    public BlazorFrontEndMock(LargeService largeService)
    {
        _largeService = largeService;
    }

    public async Task GetUsersWithFollowerCount()
    {
        Debug.WriteLine("Query 1");
        
        _users = await _largeService.GetUsersNoDispose();
    }

    public IQueryable<UserData> ShowUsers(string nameFilter)
    {
        Debug.WriteLine("Query 2");
        
        _filteredUsers = _users;

        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            _filteredUsers = _filteredUsers.Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        return _filteredUsers;
    }
}