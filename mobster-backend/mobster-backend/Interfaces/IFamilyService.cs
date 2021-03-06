using mobster_backend.DTOs.Read;
using mobster_backend.DTOs.Write;
using mobster_backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobster_backend.Interfaces
{
    public interface IFamilyService
    {
        Task<FamilyDto> AddFamily(SetFamilyDto model);
#nullable enable
        Task<IEnumerable<FamilyDto>> GetFamilies(string? searchstring);
#nullable disable
        Task<IEnumerable<FamilyDto>> GetFamiliesByUserId(Guid userId);
        Task<FamilyDto> GetFamily(Guid familyId);
        Task<IEnumerable<FamilyDto>> GetTop5Families();
        Task<IEnumerable<UserDto>> GetFamilyMembers(Guid familyId);
        Task<IEnumerable<UserDto>> GetInvitableUsers(Guid familyId);
        Task AddFamilyMember(Guid familyId, Guid userId);
        Task AddFamilyMembers(Guid familyId, IEnumerable<UserDto> users);
        Task<FamilyDto> UpdateFamily(Guid familyId, SetFamilyDto model);
        Task DeleteFamily(Guid familyId);
        Task RemoveUserFromFamily(Guid familyId, Guid userId);
        Task RemoveUsersFromFamily(Guid familyId, IEnumerable<Guid> userIds);

    }
}
