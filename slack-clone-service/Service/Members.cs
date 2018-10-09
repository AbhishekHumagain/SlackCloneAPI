using Microsoft.EntityFrameworkCore;
using slack_clone_model;
using slack_clone_repo;
using slack_clone_service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slack_clone_service.Service
{
    public class Members : IMembers
    {
        #region Constructor

        private SlackCloneDBContext _context;

        public Members(SlackCloneDBContext context)
        {
            _context = context;
        }

        public IQueryable<MemberDataViewModel> GetFilterData(int nu)
        {
            try
            {
                var filterData = from member in _context.SlackData
                                 where (member.AccountType == nu || member.BillingStatus == nu)
                                 select new MemberDataViewModel
                                 {
                                     Id = member.Id,
                                     AccountType = member.AccountType,
                                     Authentication = member.Authentication,
                                     BillingStatus = member.BillingStatus,
                                     Email = member.Email,
                                     Name = member.Name
                                 };
                return filterData.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Constructor

        public IQueryable<MemberDataViewModel> GetMemberDetails()
        {
            try
            {
                var memberDetail = from member in _context.SlackData
                                   select new MemberDataViewModel
                                   {
                                       Id = member.Id,
                                       AccountType = member.AccountType,
                                       Authentication = member.Authentication,
                                       BillingStatus = member.BillingStatus,
                                       Email = member.Email,
                                       Name = member.Name
                                   };
                return memberDetail.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<MemberDataViewModel> SearchMember(string search)
        {
            try
            {
                var data = from member in _context.SlackData
                           where (member.Name.ToLower().Contains(search))
                           select new MemberDataViewModel
                           {
                               Id = member.Id,
                               AccountType = member.AccountType,
                               Authentication = member.Authentication,
                               BillingStatus = member.BillingStatus,
                               Email = member.Email,
                               Name = member.Name
                           };
                return data.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAccountType(MemberDataViewModel memberData)
        {
            try
            {
                var memberDetails = _context.SlackData.ToList().Where(x => x.Id == memberData.Id).FirstOrDefault();
                memberDetails.AccountType = memberData.AccountType;
                _context.Entry(memberDetails).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}