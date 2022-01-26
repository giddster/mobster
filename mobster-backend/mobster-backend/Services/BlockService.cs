﻿using Microsoft.EntityFrameworkCore;
using mobster_backend.Database;
using mobster_backend.DTOs.Read;
using mobster_backend.DTOs.Write;
using mobster_backend.Extensions;
using mobster_backend.Interfaces;
using mobster_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobster_backend.Services
{
    public class BlockService : IBlockService
    {
        private readonly MobsterContext context;
        public BlockService(MobsterContext context)
        {
            this.context = context;
        }
        public async Task BlockUserFromFamily(SetBlockedMemberDto model)
        {
            var user = await context.Users.FindAsync(model.UserId);
            var family = await context.Families.Include(f => f.FamilyMembers).FirstOrDefaultAsync(f => f.FamilyId == model.FamilyId);
            
            if (family.FamilyMembers.Contains(user))
            {
                family.FamilyMembers.Remove(user);
                //checked, works
                family.MemberCount -= 1;
            }
            
            var blockedMember = new BlockedMember(model.FamilyId, model.UserId, model.Description);

            context.BlockedMembers.Add(blockedMember);
            
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetBlockedUserByFamily(Guid familyId)
        {
            var blockedUsers = context.BlockedMembers
                .Where(u => u.FamilyId == familyId);

            var innerJoin = from blockedUser in blockedUsers
                            join user in context.Users on blockedUser.UserId equals user.UserId
                            select new UserDto { UserId = blockedUser.UserId, UserName = user.UserName };

            return await innerJoin.ToListAsync();
        }

        public async Task RemoveBlockedUserFromFamily(Guid userId, Guid familyId)
        {
            var blockedMember = await context.BlockedMembers.FirstOrDefaultAsync(u => u.UserId == userId && u.FamilyId == familyId);
            context.BlockedMembers.Remove(blockedMember);
            await context.SaveChangesAsync();
        }
    }
}
