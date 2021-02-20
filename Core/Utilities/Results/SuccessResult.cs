using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {

        }

        public SuccessResult() : base(true)
        {

        }
    }
}
