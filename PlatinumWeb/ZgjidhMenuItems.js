

//var json = JSON.parse(KonfiguroJson);
//var json = jQuery.parseJSON(KonfiguroJson);//kthen nje string te json ne js object
//var $settings_modal = $('$json.Groups[16]-modal');
//var $settings = $('$json.Groups[16]');
//var $settings_selects = $('$json.Groups[16]-selects select');
//$json.Groups[16].on('click', function (e) {});

                $.extend($.ui.multiselect, {
                    locale: {
                        addAll:'Shfaq te gjitha',
                        removeAll:'Hiq te gjitha',
                        itemsCount:'submenu te shfaqura'
                    }
                });

var $settings_modal = $('#settings-modal');
var $settings = $('#settings');
var $settings_selects = $('#settings-selects select');

var multiselect_ops = {
    searchable: false
};

$settings_modal.dialog({
    autoOpen: false,
    minWidth: 800,
    minHeight: 500,
    draggable: false,
    buttons: {
        Ok: function() {
            $(this).dialog('close');
                         
        },
        Cancel: function() {
            $(this).dialog('close');
        }
    },
    create: function( event, ui ) {
        $(this).closest('.ui-dialog').find('.ui-dialog-title').after('<select id="settings-modal-choices">'+
                '<option value="1">Kontabilitet</option>'+
                '<option value="5">Inventar</option>'+
                '<option value="6">Blerje dhe shitje</option>'+
                '<option value="7">Arka dhe Banka</option>'+
                '<option value="8">Burime Njerëzore</option>'+
                '<option value="9">Prodhim</option>'+
                '<option value="10">Qendra Kosto</option>'+
                '<option value="11">Amortizim</option>'+
                '<option value="12">Raporte</option>'+
                '<option value="13">Raporte Inteligjente</option>'+
                '<option value="14">Ndihme</option>'+
            '</select>');
    },
    open: function() {
        $settings_selects.filter('.active').multiselect(multiselect_ops);
    },
    close: function() {
        $settings_selects.filter('.active').multiselect('destroy');
        $settings.removeClass('opened');
    }
});
$('#settings-modal-choices').on('change', function(e) {
    $settings_selects.filter('.active').multiselect('destroy');
    $settings_selects.filter('.active').removeClass('active').css('display', 'none');
    $settings_selects.css('display', 'none');
    $settings_selects.filter('#settings-select-'+$(this).val()).css('display', '').addClass('active').multiselect(multiselect_ops);
});

$settings.on('click', function(e) {
    e.preventDefault();
    if ($settings.hasClass('opened')) {
        return; // break
    }
    $settings.addClass('opened');
    $settings_modal.dialog('open');
});