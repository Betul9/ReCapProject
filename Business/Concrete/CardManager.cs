using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CardManager : ICardService
    {
        ICardDal _cardDal;

        public CardManager(ICardDal cardDal)
        {
            _cardDal = cardDal;
        }

        public IResult Add(Card card)
        {
            _cardDal.Add(card);
            return new SuccessResult(Messages.CardAdded);
        }

        public IResult Delete(Card card)
        {
            _cardDal.Delete(card);
            return new SuccessResult(Messages.CardDeleted);
        }

        public IDataResult<List<Card>> GetAll()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll());
        }

        public IDataResult<Card> GetById(int cardId)
        {
            return new SuccessDataResult<Card>(_cardDal.Get(p => p.CardId == cardId));
        }

        public IResult DoesCardExist(Card card)
        {
            var result = _cardDal.Get(p => p.CvvNumber == card.CvvNumber && p.CardNumber == card.CardNumber && p.FirstName == card.FirstName && p.LastName == card.LastName);
            if (result == null)
            {
                return new ErrorResult(Messages.CardDoesNotExist);
            }
            return new SuccessResult(Messages.CardExists);
        }

        public IDataResult<Card> GetByCardNumber(int cardNumber)
        {
            return new SuccessDataResult<Card>(_cardDal.Get(p => p.CardNumber == cardNumber));
        }

        public IResult Update(Card card)
        {
            _cardDal.Update(card);
            return new SuccessResult(Messages.CardUpdated);
        }
    }
}
