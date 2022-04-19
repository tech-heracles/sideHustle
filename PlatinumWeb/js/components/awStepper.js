;
$.widget("alphaweb.awStepper", {
    options: {
        value: 0, //selected index
        items: null, //array with steps names
        disabledItems: null, //array with steps that should be disabled on initialize
        itemsCount: 0, //number of elements
        onClick: null //event to be called on step click
    },
    setIndex: function (value) {
        this.option("value", value);
    },
    _itemsElements: new Array(),
    _containerElement: null,
    _create: function () {
        var widget = this;
        this.options.itemsCount = this.options.items.length;
        this._containerElement = $("<div>").addClass("mdl-card mdl-shadow--2dp");
        var stepper = $("<div>").addClass("mdl-card__supporting-text");
        var stepperUl = $("<div>").addClass("mdl-stepper-horizontal-alternative");
        for (var i = 0, itemsCount = this.options.itemsCount; i < itemsCount; i++) {
            var stepperLi = $("<div>").addClass(i == 0 ? "mdl-stepper-step active-step editable-step" : "mdl-stepper-step").addClass("step-clicker").attr("data-index", i).css("width", ((100 / itemsCount) - 1) + "%").on("click", function (e) { widget._onClick(e); });;
            if (!(this.options.disabledItems && this.options.disabledItems.includes(this.options.items[i])))
                stepperLi.addClass("clickable-step");
            var stepperCircle = $("<div>").addClass("mdl-stepper-circle");
            var stepperNumber = $("<span>").html((i + 1));
            var stepperTtile = $("<div>").addClass("mdl-stepper-title").html(this.options.items[i]);
            var stepperLeftBar = $("<div>").addClass("mdl-stepper-bar-left");
            var stepperRightBar = $("<div>").addClass("mdl-stepper-bar-right");
            stepperUl.append(stepperLi.append(stepperCircle.append(stepperNumber)).append(stepperTtile).append(stepperLeftBar).append(stepperRightBar));
        }
        this._itemsElements = stepperUl.children();
        this.element.append(this._containerElement.append(stepper.append(stepperUl)));
    },
    _setOption: function (key, value) {
        this._super(key, value);
        switch (key) {
            case "value":
                this._setSelectedIndex();
                break;
            case "disabled":
                this._disable();
                break;
            case "disabledItems":
                this._disableItems();
                break;
        }
    },
    _onClick: function (e) {
        if (!e.currentTarget.classList.contains("clickable-step"))
            return;
        this.options.value = parseInt(e.currentTarget.getAttribute("data-index"));
        if (this.options.onClick)
            this.options.onClick(this.options.value);
        this._setSelectedIndex();
    },
    _setSelectedIndex: function () {
        var widget = this;
        for (var i = 0, itemsCount = this.options.itemsCount; i < itemsCount; i++) {
            if (i >= this.options.value)
                $(this._itemsElements[i]).removeClass("active-step").removeClass("step-done").removeClass("editable-step");
            else
                $(this._itemsElements[i]).addClass("active-step").addClass("step-done");
            if (i > this.options.value + 1)
                this._containerElement.find("[data-index=" + i + "]").removeClass("clickable-step");
        }
        this._containerElement.find("[data-index=" + (this.options.value + 1) + "]").addClass("clickable-step");
        this._containerElement.find(".active-step").removeClass("editable-step").addClass("step-done");
        this._containerElement.find("[data-index=" + this.options.value + "]").addClass("active-step").addClass("editable-step");
    },
    _disableItems: function () {
        this._containerElement.find(".step-clicker").addClass("clickable-step");
        for (var i = 0, length = this.options.disabledItems.length; i < length; i++)
            this._containerElement.find("[data-index=" + this.options.items.indexOf(this.options.disabledItems[i]) + "]").removeClass("clickable-step");
    },
    _disable: function () {
        if(this.options.disabled)
            this._containerElement.find(".clickable-step").removeClass("clickable-step");
        else
            this._containerElement.find(".step-clicker").addClass("clickable-step");
    }
});