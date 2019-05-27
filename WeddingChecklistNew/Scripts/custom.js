var ajaxMethods = {

    post: function (url, parameters, callback) {

        $.post(url, parameters, callback);
    },

    postWithJson: function (url, data, successCallback, errorCallback) {

        $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });

    },

    get: function (url, parameters, callback) {
        return $.get(url, parameters, callback);
    },

    load: function (element, url, parameters, callback) {

        //screenLock.lock();
        $(element).load(url, parameters, callback);
        /*
        $(element).load(url, parameters, function () {
            $.call(this, callback);
            //screenLock.unLock();
        });*/
    },

    put: function (url, data, successCallback, errorCallback) {

        $.ajax({
            type: "PUT",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });
    }
};

var validate = {

    variables: {

        redHighlight: 'has-error',
    },

    control: function (options) {
        returnValue = true;

        options = $.extend({

            element: '',
            allowNull: false,
            cElement: ''

        }, options);


        var element = options.element;
        var allowNull = options.allowNull;
        var containerElement = options.cElement;
        errorElement = containerElement;

        resultHtml = '';
        resultHtml += '<div class="alert alert-danger"><button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>';
        resultHtml += '<ul class="list">';

        //elementler üzerinde donuluyor.
        element.each(function () {

            var $element = $(this);
            var $elementTagName = $element.prop('tagName').toLowerCase();

            if ($elementTagName == 'select') {
                validate._select($element, allowNull);

            } else if ($elementTagName == 'input' || $elementTagName == 'textarea') {

                var dataMin = $element.data('min');
                var dataMax = $element.data('max');
                // eğer data-min ve data-max attributeleri girilmişse input min-max kontrol etme metoduna gider.
                if (dataMin != undefined && dataMax != undefined) {
                    validate._inputMinMax($element, dataMin, dataMax);
                }

                validate._input($element, allowNull);
            }
        });

        resultHtml += '</ul>';
        resultHtml += '</div>';

        if (returnValue == true)
            resultHtml = '';

        if (resultHtml != '') {

            if (containerElement == '') {
                var modal = new Modal();

                modal.popup({
                    title: 'Uyarı',
                    body: resultHtml,
                    width: 600,
                    buttons: [
                        {
                            id: 'btnCloseModal',
                            text: 'Kapat',
                            type: 'btn-danger',
                            callback: function () {
                                modal.hide();
                                return false;
                            }
                        }]

                }).show();

            } else {
                $(containerElement).html(resultHtml);
            }
        }

        return returnValue;
    },

    _select: function (element, allowNull) {

        if (allowNull == false && ($.trim(element.val()) <= 0)) {

            element.parent().addClass(validate.variables.redHighlight);
            resultHtml += "<li>" + $(element).data('req') + "</li>";
            returnValue = false;

        } else {
            if (element.parent().hasClass(validate.variables.redHighlight)) {
                element.parent().removeClass(validate.variables.redHighlight);
                $(errorElement).html('');
            }
        }

    },

    _input: function (element, allowNull) {

        if (allowNull == false && ($.trim(element.val()) == '')) {
            // || Number(element.val()) == 0)
            element.parent().addClass(validate.variables.redHighlight);
            var dataReq = $(element).data('req');
            resultHtml += "<li>" + dataReq + "</li>";
            returnValue = false;
        }
        else if (element.parent().hasClass(validate.variables.redHighlight)) {
            element.parent().removeClass(validate.variables.redHighlight);
            $(errorElement).html('');
        }
    },

    _inputMinMax: function (element, minValue, maxValue) {

        var cond = false;

        if ($.trim(element.val()) >= minValue && $.trim(element.val()) <= maxValue) {
            cond = true;
        } else {
            cond = false;
        }

        if (!cond) {
            element.addClass(validate.variables.redHighlight);
            var dataMaxMinMessage = $(element).data('minmaxmessage');
            resultHtml += "<li>" + dataMaxMinMessage + "</li>";
            returnValue = false;
        }
        else if (element.hasClass(validate.variables.redHighlight)) {
            element.removeClass(validate.variables.redHighlight);
        }


    }

};