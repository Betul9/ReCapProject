using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ReCapProjectContext>, IUserDal
    {
        public List<UserDetailDto> GetUserDetails()
        {
            using(ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from u in context.Users
                             join c in context.Customers on u.UserId equals c.CustomerId
                             select new UserDetailDto
                             {
                                 UserId = u.UserId,
                                 CompanyName = c.CompanyName,
                                 CustomerId = c.CustomerId
                             };
                return result.ToList();
            }
        }
    }
}
