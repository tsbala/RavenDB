using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;

namespace FirstStepMVC.Validators
{
    public class GenderRangeAttribute : RangeAttribute, IClientValidatable
    {
        public GenderRangeAttribute(string errorMessage)
            : base(typeof(Gender), Gender.Male.ToString(), Gender.Female.ToString())
        {
            ErrorMessage = errorMessage;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var clientValidation = new ModelClientValidationRule
                {
                    ErrorMessage = ErrorMessage,
                    ValidationType = "genderrange"
                };
            clientValidation.ValidationParameters.Add("validgender", string.Join(",", Gender.Male, Gender.Female));
            yield return clientValidation;
        }
    }
}