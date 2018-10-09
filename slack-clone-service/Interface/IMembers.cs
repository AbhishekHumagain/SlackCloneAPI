using slack_clone_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slack_clone_service.Interface
{
    public interface IMembers
    {
        IQueryable<MemberDataViewModel> GetMemberDetails();

        IQueryable<MemberDataViewModel> SearchMember(string search);

        Task UpdateAccountType(MemberDataViewModel memberData);

        IQueryable<MemberDataViewModel> GetFilterData(int nu);
    }
}