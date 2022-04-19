;

function MultiSelect(options) {
    this.Options = {
        maxItems: null,
        plugins: ['remove_button'],
        create: false,
        meLupe: false,
        onButtonClickLupa: undefined,
        valueField: 'Id',
        labelField: 'Kodi',
        searchField: 'Pershkrimi',
        idGjuha: 0,
        init: false,
        multiSelectId: "",
        valueField: '',
        labelField: '',
        searchField: '',
        delimiter: ',',
        visible: false,
        enabled: true,
        value: '',
        customWebService: undefined
    }
    this.Options = $.extend({}, this.Options, options);

    this._initialized = false;

    this._shtoLupe = function (selectId, buttonClickEvent) {


        $(".selectize-control").css('display', 'inline-flex');
        $(".selectize-control").css('width', '100%');

        $(".selectize-control").append('<span><button type="button" id="' + selectId + 'Lupe"  class="btn btn-default btn-sm"><span class="glyphicon glyphicon-search"></span></button></span>');


        $("#" + this.Options.multiSelectId + "Lupe").on("click", function (e) {
            e.preventDefault();
            buttonClickEvent();
        });


        $("#" + selectId + "Lupe").parent().parent().parent().addClass("bootstrap-iso");
    }

    this.SetValue = function (value) {
        this.Options.value = value;
        if (!this._initialized)
            return;
        this.Control.setValue(value);
    }
    this.GetValue = function () {
        return this.Control.getValue();
    }
    this.GetText = function () {
        return this.GetValue().join();
    }
    this.SetText = function (text) {
        return this.SetValue(text.split(','));
    }
    this.SetEnabled = function (value) {
        this.Options.enabled = value;
        if (!this._initialized)
            return;
        if (this.Options.enabled) {
            this.Control.enable();
            $("#" + this.Options.multiSelectId + "Lupe").removeAttr('disabled');
        }
        else {
            this.Control.disable();
            $("#" + this.Options.multiSelectId + "Lupe").attr('disabled', 'disabled');
        }
    }

    this.GetMainElement = function () {
        return "#" + this.Options.multiSelectId;
    }

    this.SetVisible = function (value) {
        this.Options.visible = value;
        if (!this._initialized)
            return;
        if (this.Options.visible) {
            $(".selectize-control").show(); $("#cmbAutorizimiLupe").show();
        } else {
            $(".selectize-control").hide(); $("#cmbAutorizimiLupe").hide();
        }
    }

    this.IsInitialized = function () { return this._initialized; };
    //this.SetFocus = function (focus) {
    //    this.Options.focus = focus;
    //    if (!this._initialized)
    //        return;
    //    if (this.Options.focus) this.Control.focus();
    //    else this.Control.blur();
    //};

    this.Init = function () {
        if (this._initialized)
            this.Control.destroy();
        this._initialized = true;
        var select = $("#" + this.Options.multiSelectId).selectize(this.Options);
        this.Control = select[0].selectize;
        if (this.Options.meLupe)
            this._shtoLupe(this.Options.multiSelectId, this.Options.onButtonClickLupa);

        this.SetEnabled(this.Options.enabled);
        this.SetValue(this.Options.value);
        // this.SetFocus(this.Options.focus);
        this.SetVisible(this.Options.visible);
    }

    this.FindOptionByValue = function (value) {
        return this.Control.options[value];        
    }
    this.Contains = function (searchValue) {
        return this.Control.search(searchValue).items.length > 0;
    }
    this.RemoveOption = function (value) {
        if (!value) return;

        var foundItem = this.FindOptionByValue(value);
        if (!foundItem) return;

        var Options = this.Options;
        var values = this.GetValue();
        Options.options = Options.options.filter(function (option) { return option[Options.valueField] != foundItem[Options.valueField] });
        values = values.filter(function (value) { return value != foundItem[Options.valueField] });
        this.Init();
        this.SetValue(values);
    }

    this.AddOption = function (element) {
        if (!element) return;
        this.Options.options.push(element);
        var values = this.GetValue();
        this.Init();
        this.SetValue(values);
    }

    this.ClearOptions = function () {
        if (this._initialized)
            this.Control.clearOptions();
    }

    this.GetSelectedItems = function (value) {
        if (!this.Control)
            return; 
        return this.Control.items;
    }
    this.GetItem = function (value) {
        return this.Control.options[value];
    }

    this.GetMaxItems = function () {
        return this.Options.maxItems;
    }
    this.SetMaxItems = function (maxItems) {
        this.Options.maxItems = maxItems;
    }
    this.SetOption = function (option, value) {
        this.Options[option] = value;
    }
    this.GetOption = function (option) {
        return this.Option[option];
    }
    if (this.Options.init) {
        this.Init();
    }

};

