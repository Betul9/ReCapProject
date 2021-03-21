using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            var result = IsRentable(rental);
            if (result.Success) {
                rental.RentDate = DateTime.Now;
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentalAdded);
            }
            else
                return new ErrorResult(Messages.RentalNotAvailable);
        }

        public IResult Delete(Rental rental)
        {
            var rentalToDelete = _rentalDal.Get(p => p.RentalId == rental.RentalId);

            if (rentalToDelete != null)
            {
                _rentalDal.Delete(rental);
                return new SuccessResult(Messages.RentalDeleted);
            }

            else
                return new ErrorResult(Messages.RentalCouldNotFound);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p=>p.RentalId == rentalId));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p => p.CarId == carId));
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult IsRentable(Rental rental)
        {
            IResult result = BusinessRules.Run(CheckIfCarReturned(rental), CheckIfRentDateAvailable(rental));
            if (result != null)
            {
                return new ErrorResult();
            }
            else
                return new SuccessResult();
        }

        public IResult CheckIfCarReturned(Rental rental)
        {
            var result = this.GetByCarId(rental.CarId).Data.LastOrDefault();
            if (result.ReturnDate != null || ((result.ReturnDate == null) || (result.ReturnDate == default) && (result.RentDate == null) || (result.RentDate == default)))
            {
                return new SuccessResult(Messages.RentalAvailable);
            }
            else
                return new ErrorResult(Messages.RentalNotAvailable); 
        }

        public IResult CheckIfRentDateAvailable(Rental rental)
        {
            var result = this.GetByCarId(rental.CarId).Data.LastOrDefault();
            if(result.RentDate == null || result.RentDate == default || result.RentDate<DateTime.Now)
            {
                return new SuccessResult(Messages.RentalAvailable);
            }
            else
            {
                return new ErrorResult(Messages.RentalNotAvailable);
            }
        }

    }
}
