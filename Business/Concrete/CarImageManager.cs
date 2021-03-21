using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            try
            {
                IResult result = BusinessRules.Run(CheckIfImageLimit(carImage.CarId));

                if (result != null)
                {
                    return result;
                }

                carImage.ImagePath = FileHelper.Add(file);
                carImage.Date = DateTime.Now;

                _carImageDal.Add(carImage);
                return new SuccessResult(Messages.CarImageAdded);
            }

            catch
            {
                return new ErrorResult(Messages.ImageCouldNotBeAdded);
            }
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var oldPath = _carImageDal.Get(p => p.ImageId == carImage.ImageId).ImagePath;

            carImage.ImagePath = FileHelper.Update(oldPath, file);
            carImage.Date = DateTime.Now;

            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int imageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p=>p.ImageId == imageId));
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageNull(carId));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>();
            }

            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId).Data);
        }

        private IDataResult<List<CarImage>> CheckIfCarImageNull(int carId)
        {
             string path = @"\Images\cd927546-debb-4176-9878-fc844e23294d.jpg";
             var result = _carImageDal.GetAll(p => p.CarId == carId).Any();

             if (!result)
             {
                var carImage = new List<CarImage>{
                    new CarImage { CarId = carId, ImagePath = path, Date = DateTime.Now }
                };
                return new SuccessDataResult<List<CarImage>>(carImage);
             }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(p => p.CarId == carId).ToList());
        }


        private IResult CheckIfImageLimit(int carId)
        {
            var imgCounter = _carImageDal.GetAll(p => p.CarId == carId).Count;

            if (imgCounter > 5)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

    }
}
