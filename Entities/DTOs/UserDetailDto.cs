using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserDetailDto : IDto
    {
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
    }
}
