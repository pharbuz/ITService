﻿using System;
using ITService.Domain.Entities;
using System.Threading.Tasks;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(Guid id);
        Task DeleteAsync(User user);
        Task<UserPageResult<User>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<string> LoginAsync(string username, string password, bool rememberMe);
        Task LogoutAsync();
    }
}
