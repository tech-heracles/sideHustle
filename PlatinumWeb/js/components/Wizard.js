;
function Wizard() {
    var app = this;

    this.model = function () {
        model = this;

        this.templates = null;

        this.messages = {
            MP_lblNextButton: "Next",
            MP_lblPreviousButton: "Previous",
            MP_lblCloseButton: "Cancel",
            MP_lblFinishButton: "Finish",
            MP_lblStopButton: "Stop"
        };

        this.buttons = {
            nextButton: { id: 1, text: function () { return model.messages.MP_lblNextButton; }, type: "default", height: "7%", focusStateEnabled: false },
            previousButton: { id: 2, text: function () { return model.messages.MP_lblPreviousButton; }, type: "default", height: "7%", focusStateEnabled: false, visible: false },
            cancelButton: { id: 3, text: function () { return model.messages.MP_lblCloseButton; }, type: "normal", height: "7%", focusStateEnabled: false },
            finishButton: { id: 4, text: function () { return model.messages.MP_lblFinishButton; }, type: "success", height: "7%", focusStateEnabled: false, visible: false },
            stopButton: { id: 5, text: function () { return model.messages.MP_lblStopButton; }, type: "danger", height: "7%", focusStateEnabled: false, visible: false }
        };

        this.dispose = function () {
            this.templates = null;
            this.messages = null;
            this.buttons = null;
        };
    };

    this.view = function () {
        var view = this;
        //#region containers
        this.wizardContainer = null;
        this.nextButtonContainer = null;
        this.previousButtonContainer = null;
        this.cancelButtonContainer = null;
        this.finishButtonContainer = null;
        this.stopButtonContainer = null;
        //#endregion

        //#region widgets
        this.wizard = null;
        this.nextButton = null;
        this.previousButton = null;
        this.cancelButton = null;
        this.finishButton = null;
        this.stopButton = null;
        //#endregion

        //#region events
        this.nextButtonOnClick = null;
        this.previousButtonOnClick = null;
        this.cancelButtonOnClick = null;
        this.finishButtonOnClick = null;
        this.stopButtonOnClick = null;
        this.onSelectionChanged = null;
        //#endregion

        this.renderWizard = function (templates) {
            this.wizardContainerDiv = $("<div style='height:100%;'>");
            this.wizardContainer = $("<div>");
            this.wizardContainerDiv.append(this.wizardContainer);
            this.wizard = this.wizardContainer.dxMultiView({
                selectedIndex: 0,
                loop: false,
                animationEnabled: true,
                onSelectionChanged: view.onSelectionChanged,
                height: "80%",
                swipeEnabled: false,
                items: templates
            }).dxMultiView("instance");
        };

        this.renderNextButton = function (options) {
            this.nextButtonContainer = $("<div class='next-button'>");
            this.nextButton = this.nextButtonContainer.dxButton(options).dxButton("instance");
            this.nextButton.option("onClick", this.nextButtonOnClick);
        };

        this.renderPreviousButton = function (options) {
            this.previousButtonContainer = $("<div class='previous-button'>");
            this.previousButton = this.previousButtonContainer.dxButton(options).dxButton("instance");
            this.previousButton.option("onClick", this.previousButtonOnClick);
        };

        this.renderCancelButton = function (options) {
            this.cancelButtonContainer = $("<div class='cancel-button'>");
            this.cancelButton = this.cancelButtonContainer.dxButton(options).dxButton("instance");
            this.cancelButton.option("onClick", this.cancelButtonOnClick);
        }

        this.renderFinishButton = function (options) {
            this.finishButtonContainer = $("<div class='next-button'>");
            this.finishButton = this.finishButtonContainer.dxButton(options).dxButton("instance");
            this.finishButton.option("onClick", this.finishButtonOnClick);
        };

        this.renderStopButton = function (options) {
            this.stopButtonContainer = $("<div class='stop-button'>");
            this.stopButton = this.stopButtonContainer.dxButton(options).dxButton("instance");
            this.stopButton.option("onClick", this.stopButtonOnClick);
        };


        this.disableButton = function (buttonId, value) {
            switch (buttonId) {
                case "next":
                    view.nextButton.option('disabled', value);
                    return;
                case "previous":
                    view.previousButton.option('disabled', value);
                    return;
                case "cancel":
                    view.cancelButton.option('disabled', value);
                    return;
                case "finish":
                    view.finishButton.option('disabled', value);
                    return;
                case "stop":
                    view.stopButton.option('disabled', value);
                    return;
            }
        };

        this.dispose = function () {
            this.wizard.dispose();
            this.nextButton.dispose();
            this.previousButton.dispose();
            this.cancelButton.dispose();
            this.finishButton.dispose();
            this.stopButton.dispose();
        };
    };

    this.controller = function (model, view) {
        var controller = this;

        this.model = model;
        this.view = view;

        this.stopable = false;
        this.events = {
            onSelectionChanged: function () { console.log("onSelectionChanged not implemented"); },
            onSubmit: function () { console.log("onSubmit not implemented"); },
            onStop: function () { console.log("onStop not implemented"); },
            onDispose: function () { console.log("onDispose not implemented"); }
        };        

        this.initializeEvents = function (events) {
            this.view.nextButtonOnClick = this.goNext;
            this.view.previousButtonOnClick = this.goPrevious;
            this.view.cancelButtonOnClick = this.dispose;
            this.view.finishButtonOnClick = this.submit;
            this.view.stopButtonOnClick = this.stop;
            this.view.onSelectionChanged = this.selectionChanged;
        };

        this.initializeWizard = function () {
            this.view.renderCancelButton(this.model.buttons.cancelButton);
            this.view.renderPreviousButton(this.model.buttons.previousButton);
            this.view.renderNextButton(this.model.buttons.nextButton);
            this.view.renderFinishButton(this.model.buttons.finishButton);
            this.view.renderStopButton(this.model.buttons.stopButton);
            this.view.renderWizard(this.model.templates);

            this.view.wizardContainerDiv.append(this.view.cancelButtonContainer).append(this.view.nextButtonContainer).append(this.view.finishButtonContainer).append(this.view.stopButtonContainer).append(this.view.previousButtonContainer);
        };

        this.goNext = function () {
            controller.view.wizard.option("selectedIndex", controller.view.wizard.option("selectedIndex") + 1);
        };

        this.goPrevious = function () {
            controller.view.wizard.option("selectedIndex", controller.view.wizard.option("selectedIndex") - 1);
        };

        this.submit = function () {
            controller.events.onSubmit();
        };

        this.stop = function () {
            controller.events.onStop();
        }

        this.selectionChanged = function (e) {
            var templates = controller.model.templates;
            switch (e.addedItems[0]) {
                case templates[0]:
                    controller.view.cancelButton.option('visible', true);
                    controller.view.nextButton.option('visible', true);
                    controller.view.previousButton.option('visible', false);
                    controller.view.finishButton.option('visible', false);
                    controller.view.stopButton.option('visible', false);
                    break;
                case templates[templates.length - 2]:
                    controller.view.cancelButton.option('visible', true);
                    controller.view.nextButton.option('visible', false);
                    controller.view.previousButton.option('visible', true);
                    controller.view.finishButton.option('visible', true);
                    controller.view.stopButton.option('visible', false);
                    break;
                case templates[templates.length - 1]:
                    controller.view.cancelButton.option('visible', true);
                    controller.view.nextButton.option('visible', false);
                    controller.view.previousButton.option('visible', false);
                    controller.view.finishButton.option('visible', false);
                    if (controller.stopable)
                        controller.view.stopButton.option('visible', true);
                    break;
                default:
                    controller.view.cancelButton.option('visible', true);
                    controller.view.nextButton.option('visible', true);
                    controller.view.previousButton.option('visible', true);
                    controller.view.finishButton.option('visible', false);
                    controller.view.stopButton.option('visible', false);
                    break;
            };
            controller.events.onSelectionChanged(e);
        };


        this.dispose = function () {
            controller.view.dispose();
            controller.model.dispose();
            controller.events.onDispose();
        };


        this.getElement = function () {
            return this.view.wizardContainerDiv;
        };

        this.goToIndex = function (index) {
            this.view.wizard.option("selectedIndex", index);
        };

        this.disableButton = function (buttonId, value) {
            this.view.disableButton(buttonId, value);
        };

        (function (controller) {
            controller.initializeEvents();
            controller.initializeWizard();
        }(controller));
    };

    return {
        init: function (templates, events, messages, stopable) {
            var model = new app.model(); //inicializohet model-i
            model.templates = templates;
            if (messages)
                model.messages = messages;

            var view = new app.view(); //inicializohet viewe-ri
            var controller = new app.controller(model, view); //inicializohet controller-i
            controller.stopable = stopable;
            if (events)
                controller.events = events;

            return {
                getElement: function () { return controller.getElement(); },
                goToIndex: function (index) { controller.goToIndex(index); },
                disableButton: function (buttonId, value) { controller.disableButton(buttonId, value); }
            };
        }
    };
}