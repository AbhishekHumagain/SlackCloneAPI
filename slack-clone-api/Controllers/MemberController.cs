using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using slack_clone_model;
using slack_clone_repo;
using slack_clone_service.Interface;

namespace slack_clone_api.Controllers
{
    [EnableCors("SlackCloneCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private IMembers _members;
        private IEmailService _emailService;

        #region constructor

        public MemberController(IMembers members, IEmailService emailService)
        {
            _emailService = emailService;
            _members = members;
        }

        #endregion constructor

        #region Get member details

        [HttpGet]
        [Route("MemberDetails")]
        public IQueryable<MemberDataViewModel> GetMemberDetails()
        {
            var memberDetails = _members.GetMemberDetails();
            return memberDetails;
        }

        #endregion Get member details

        [HttpGet]
        [Route("searchMember/{search}")]
        public IQueryable<MemberDataViewModel> SearchMember(string search)
        {
            var memberDetails = _members.SearchMember(search);
            return memberDetails;
        }

        [HttpPut]
        [Route("updateAccountType")]
        public async Task<IActionResult> UpdateAccountType([FromBody] MemberDataViewModel memberData)
        {
            var memberDetails = _members.UpdateAccountType(memberData);
            await memberDetails;
            return Ok(memberDetails);
        }

        #region send mail

        [HttpPost]
        [Route("sendMail")]
        public IActionResult SendEmail(string emailAddress)
        {
            try
            {
                string content = @"Join habhishekiw on Slack' +
                                'Abhishek Humagain (habhishekiw@gmail.​com) has invited you to join the Slack workspace habhishekiw. Join now to start collaborating!";
                string subject = "Invite Members";
                Task result = _emailService.SendEmailAsync(emailAddress, subject, content);
                if (result.IsCompleted)
                    return Ok(JsonConvert.SerializeObject("Email sent successfully"));
                else
                    return BadRequest("Email not sent!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion send mail

        #region Filter Data

        [HttpGet]
        [Route("filterData/{nu:int}")]
        public IQueryable<MemberDataViewModel> GetFilterData(int nu)
        {
            var filterData = _members.GetFilterData(nu);
            return filterData;
        }

        #endregion Filter Data
    }
}