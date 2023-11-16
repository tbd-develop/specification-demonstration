using System.Diagnostics;
using demo.Data.Models;
using demo.Infrastructure;

namespace demo;

public class BlazorFrontEndMock
{
    private readonly LargeService _largeService;
    private IQueryable<User> _users = null!;
    private IQueryable<User> _filteredUsers = null!;

    public BlazorFrontEndMock(LargeService largeService)
    {
        _largeService = largeService;
    }

    public async Task GetUsersWithFollowerCount()
    {
        Debug.WriteLine("Query 1");
        
        _users = (await _largeService.GetUsersDbSetWithNoDispose()).AsQueryable();
    }

    public IQueryable<User> ShowUsers(string nameFilter)
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