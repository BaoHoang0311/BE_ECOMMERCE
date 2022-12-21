using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using API.Dtos;

namespace API.Helpers
{
    public class ValidationListEmptyOrderDetail : ValidationAttribute
    {
        public ValidationListEmptyOrderDetail()
        {
            ErrorMessage = "List rong";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            var z = value as List<OrderDetailDtos>;
            return z.Count > 0;
        }
    }
}
