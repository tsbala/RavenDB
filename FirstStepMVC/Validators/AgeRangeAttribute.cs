using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FirstStepMVC.Validators
{
    public class AgeRangeAttribute : RangeAttribute, IClientValidatable
    {
        public AgeRangeAttribute(string errorMessage)
            : base(typeof(DateTime), DateTime.Today.AddYears(-15).ToShortDateString(), DateTime.Today.AddYears(-3).ToShortDateString())
        {
            ErrorMessage = errorMessage;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule()
                             {
                                 ErrorMessage = ErrorMessage,
                                 ValidationType = "agerange"
                             };
        }
    }
}