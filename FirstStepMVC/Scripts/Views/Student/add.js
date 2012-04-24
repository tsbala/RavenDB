(function ($) {
    $.validator.addMethod("genderValidate",
                function (value, element) {
                    return FirstStep.GenderValidation(value);
                });
    $.validator.unobtrusive.adapters.addBool('genderrange', 'genderValidate');

    $.validator.unobtrusive.adapters.add('agerange', ['minDateOfBirth', 'maxDateOfBirth'], function (options) {
        var params = {
            minDateOfBirth: options.params.minDateOfBirth,
            maxDateOfBirth: options.params.maxDateOfBirth
        };

        options.rules["agerangevalidate"] = params;
        options.messages["agerangevalidate"] = options.message;
    });

    $.validator.addMethod("agerangevalidate", function (value, element, params) {
        var dobEntered = moment(value, "DD-MM-YYYY"),
                    minDateOfBirth = moment(params.minDateOfBirth, "DD-MM-YYYY"),
                    maxDateOfBirth = moment(params.maxDateOfBirth, "DD-MM-YYYY");

        return FirstStep.AgeRangeValidation(dobEntered, minDateOfBirth, maxDateOfBirth);

    });
} (jQuery));

var FirstStep = FirstStep || {};

FirstStep.GenderValidation = function (value) {
    return (value.toLowerCase() === '1') || (value.toLowerCase() === '2');
};

FirstStep.AgeRangeValidation = function (dobEntered, minDateOfBirth, maxDateOfBirth) {
    return dobEntered >= minDateOfBirth && dobEntered <= maxDateOfBirth;
};
