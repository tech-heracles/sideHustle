function MyAjax(serviceUrl) {
    var _I = this;
    this.serviceUrl = serviceUrl;
    var optionsDefault = {
        cache: false,
        type: "POST",
        processData: false,
        contentType: "application/json",
        timeout: 100000,
        dataType: "JSON"
    };

    $.ajaxSetup(optionsDefault);


    this.merrFaqe = function (url, doneCallback, failCallback, alwaysCallback) {
        $.ajax({
            type: "GET",
            url: url,

        }).done(function (res) {
            if (!doneCallback)
                return;
            doneCallback(res)
        }).fail(function (err) {
            if (!failCallback) return;
            if (xhr.responseText) {
                var err = null;
                try { var err = JSON.parse(xhr.responseText); }
                catch (e) { err = xhr.responseText; }

                if (err)
                    failCallback(err);
                else
                    failCallback({ Message: "Nje Gabim i panjohur ka ndodhur!." })
            }
            return;

        }).always(function (a) {
            if (!alwaysCallback)
                return;
            alwaysCallback();
        });
    }
    this.thirrWebService = function (method, data, doneCallback, failCallback, alwaysCallback, beforeSendCallback) {
        var params = {};

        if (data == undefined || data == null) {
            params = "{}";
        }
        else {
            params = JSON.stringify(data);
        }

        var url = _I.serviceUrl + method;
        $.ajax({
            url: url,
            data: params,
            beforeSend: function () {
                if (!beforeSendCallback)
                    return;
                beforeSendCallback();
            }
        }).done(function (res) {
            if (!doneCallback)
                return;

            for (var property in res) {
                doneCallback(res[property]);
                break;
            }
        }).fail(function (xhr) {
            if (!failCallback) return;
            if (xhr.responseText) {
                var err = null;
                try { var err = JSON.parse(xhr.responseText); }
                catch (e) { err = xhr.responseText; }

                if (err)
                    failCallback(err);
                else
                    failCallback({ Message: "Nje Gabim i panjohur ka ndodhur!." })
            }
            return;
        })
        .always(function (res) {
            if (!alwaysCallback)
                return;
            else
                alwaysCallback();
        })

    }
    this.thirrWebServiceMeObjekt = function (params) {
        if (params = undefined || params == null)
            return;
        this.thirrWebService(params.method, params.data, params.done, params.fail, params.always, params.beforeSend)
    }
    this.percaktoSettingsAjaxRequests = function (options) {
        if (options !== undefined || options !== null) {
            $.ajaxSetup(options);
        }
    }

}



/*
Shembull


var ajax=new MyAjax("../Service1/");//parameter emrin e servicit

ajax.thirrWebServiceMeObjekt(
{
method:"merrTeDhena",
data:{nr1:4,nr2:5},
done:function(s)
     {
       console.log(s);
     }
},
fail:function(e)
     {
       console.log(e.Message);
     }
});








*/