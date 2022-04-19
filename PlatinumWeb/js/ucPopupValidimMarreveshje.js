$(document).ready(function () {

    $('#ValidoMarreveshje').on('show.bs.modal', function (e) {
        perktheModal(pageState.idGjuha);
        $('#collapse1').collapse({ 'toggle': false, 'parent': '#accordion' });
        $('#collapse2').collapse({ 'toggle': false, 'parent': '#accordion' });
        if (pageState.lloji == 'klonim') {
            $('#collapse1').collapse('show');
            $('#togglecollapse2').attr('href', '');
            $('#togglecollapse2').addClass('disabled');
            $('#buttonNgarkoFile').hide();
        }
        else {
            $('#collapse2').collapse('show');
            if ($('#togglecollapse2').hasClass('disabled'))
                $('#togglecollapse2').removeClass('disabled');
            if ($('#togglecollapse2').attr('href') == '')
                $('#togglecollapse2').attr('href', '#collapse2');
            $('#buttonNgarkoFile').show();
            
        }
    }).on('hidden.bs.modal', function (e) {
        $('#fine-uploader').fineUploader('reset');
        $('#idmarreveshje_text').val('');
    });

    $('#collapse2').on('show.bs.collapse', function (e) {
        $("#buttonNgarkoFile").show();
    }).on('hide.bs.collapse', function (e) {
        $("#buttonNgarkoFile").hide();
    });

    $("#buttonNgarkoFile").on('click', function () {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "NgarkoMarreveshje"),
            data: JSON.stringify({ idNdermarrje: pageState.idNdermarrje, dtdok: Utils.ktheDateDefault(pageState.periudha)})
        }).done(function (result) {
            $('#fine-uploader').fineUploader('reset');
            SucceededCallbackMarreveshje(result);
            });
    });

    $("#buttonKerkoId").on('click', function () {
        if ($('#idmarreveshje_text').val().trim() == "")
            return;
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KerkoMarreveshje"),
            data: JSON.stringify({ idMarreveshje: $('#idmarreveshje_text').val(), dtdok: Utils.ktheDateDefault(pageState.periudha)})
        }).done(function (result) {
            $('#idmarreveshje_text').val("");
            SucceededCallbackIDMarreveshje(result);
        });
    });

    $('#fine-uploader').fineUploader({
        template: 'qq-template-singleFile',
        request: {
            endpoint: Utils.getServerApiUrl("Rregjistrime", "NgarkoFile"),
            params: {
                scopeID: Utils.getUrlVar("scopeID"),
                lloji: "SkedareMarreveshje"
            }
        },
        thumbnails: {
            placeholders: {
                waitingPath: '/fine-uploader/placeholders/waiting-generic.png',
                notAvailablePath: '/fine-uploader/placeholders/not_available-generic.png'
            }
        },
        paramsInBody: true,
        deleteFile: {
            enabled: true,
            forceConfirm: true,
            endpoint: Utils.getServerApiUrl("Rregjistrime", "FshiFile"),
            params: {
                scopeID: Utils.getUrlVar("scopeID"),
                lloji: "SkedareMarreveshje"
            }
        },
        validation: {
            allowedExtensions: ['xlsx', 'xlsm'],
            itemLimit: 1
        },
        callbacks: {
            onError: function (id, name, errorReason, xhrOrXdr) {
                myMesazh.ShtoMesazhGabimi(qq.format("Error ne ngarkimin e skedarit me numer {} - {}.  Gabimi: {}", id, name, errorReason));
            }
        },
        messages: Utils.getGlobalization(pageState.idGjuha)
    });
});

function perktheModal(idgjuha) {
    if (idgjuha == 1) {
        $(".modal-title").html("Validate Agreement");
        $("#togglecollapse1").html("Agreement ID");
        $("#idmarreveshje_text").attr("placeholder", "Agreement ID");
        $("#togglecollapse2").html("Upload File");
        $("#buttonNgarkoFile").html("Upload");
        $("#buttonKerkoId").html("Search");
        $(".qq-uploader-selector").attr("qq-drop-area-text", "Drop the file here.");
        $(".qq-upload-button-selector").children().html("Choose File");
    }

}