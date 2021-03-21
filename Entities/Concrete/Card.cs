using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Card : IEntity
    {
        public int CardId { get; set; }
        public int CardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CvvNumber { get; set; }

    }
}
