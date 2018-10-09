using System;
using System.Collections.Generic;
using System.Text;

namespace slack_clone_model
{
    public class SlackData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int AccountType { get; set; }
        public int BillingStatus { get; set; }
        public int Authentication { get; set; }
    }

    public class MemberDataViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int AccountType { get; set; }
        public int BillingStatus { get; set; }
        public int Authentication { get; set; }
    }
}