using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using(ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                             join o in context.Colors on c.ColorId equals o.ColorId
                             join b in context.Brands on c.BrandId equals b.BrandId
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 DailyPrice = c.DailyPrice,
                                 BrandName = b.BrandName, 
                                 ColorName = o.ColorName, 
                                 Description = c.Description, 
                                 ModelYear = c.ModelYear, 
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarImagesByCarId(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars 
                             join im in context.CarImages on c.CarId equals im.CarId
                             join o in context.Colors on c.ColorId equals o.ColorId
                             join b in context.Brands on c.BrandId equals b.BrandId where c.CarId == carId
                             
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 DailyPrice = c.DailyPrice,
                                 BrandName = b.BrandName,
                                 ColorName = o.ColorName,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 Date = im.Date,
                                 ImageId = im.ImageId,
                                 ImagePath = im.ImagePath
                             };
                return result.ToList();
            }
        }
    }
}
