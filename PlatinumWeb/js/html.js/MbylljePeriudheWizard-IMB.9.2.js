;
/**Aplikacioni i wizard-it te faqes */
function MbylljePeriudheWizard() {
    var app = this;

    /**Klasa e wizardPopUpState te wizard-it qe permban variablat qe ruajne gjendjen e wizard-it */
    this.wizardPopUpState = function () {
        this.messages = null;
        this.entities = null;
        this.actions = null;
        this.years = null;
        this.months = null;
        this.wizardTemplates = null;
        this.wizardIds = null;
        this.refreshTime = 5000;

        /**Funksion inicializues per vlerat default te wizardPopUpState */
        this.initializeWizardPopUpState = function () {
            if (!this.messages) {
                this.messages = {
                    MP_lblYear: "Year",
                    MP_lblEntitety: "Entity",
                    MP_lblPcAction: "Action",
                    MP_lblPeriod: "Period",
                    MP_lblProgress: "Progress",
                    MP_msgStartProcess: "Do you want to start the process?",
                    MP_msgStopProcess: "Do you want to stop the process?"
                };
            }

            this.wizardTemplates = [
                { index: 0, template: $("#entitetiTemplate") },
                { index: 1, template: $("#veprimiTemplate") },
                { index: 2, template: $("#periudhaTemplate") },
                { index: 3, template: $("#progresiTemplate") }
            ];

            this.wizardIds = {
                entitete: "#entitete-items",
                months: "#months-items",
                actions: "#actions-items",
                years: "#years-items",
                wizardPopUp: "#wizardPopUp",
                wizardTemplate: "#wizardTemplate",
                wizardEntitetiView: "#entiteti-view",
                wizardActionView: "#action-view",
                wizardPeriudhaView: "#periudha-view",
                wizardProgressView: "#progress-item",
                wizardProgressBar: "#wizard-progressBar",
                wizardContainer: "#wizard-container",
                processProgressBar: "#process-progressBar"
            };

            this.progressBarOptions = {
                min: 0,
                max: 12,
                value: 0,
                showStatus: true,
                text: "",
                processRunning: false
            };
        };
        
        this.disposeWizardPopUpState = function () {
            this.entities = null;
            this.actions = null;
            this.years = null;
            this.months = null;
            this.wizardTemplate = null;
            this.wizardIds = null;
            this.progressBarOptions = null;
        };
    };

    /**Klasa e model qe eshte pergjegjese per logjiken e entiteteve ne wizard si dhe per lidhjen me serverin (thirrjet ajax) */
    this.model = function () {
        var model = this;
        this.getWizardConfiguration = function (doneFunction) {
            $.ajax({
                url: Utils.getServerApiUrl("MbylljePeriudhe", "GetWizardConfigurations")
            }).done(function (result) {
                var configurations = {};
                if (result.ProcessRunning)
                    configurations = model.buildRunningProcessConfigurations(result);
                else
                    configurations = model.buildWizardConfigurations(result);
                doneFunction(configurations);
            });
        };

        this.buildRunningProcessConfigurations = function (result) {
            var configurations = {
                processRunning: result.ProcessRunning,
                max: result.ProgressBarSize,
                value: result.ProgressBarValue,
                text: result.Text
            };
            return configurations;
        };

        this.buildWizardConfigurations = function (result) {
            var configurations = {
                processRunning: result.ProcessRunning,
                entities: model.buildEntities(result.Entities),
                years: model.buildYears(result.Years),
                actions: model.buildActions(result.Actions)
            };
            return configurations;
        };

        this.getWizardMonths = function (yearId, entityId, actionId, doneFunction) {
            Utils.shfaqLoadingGif("#periudha-view");
            $.ajax({
                url: Utils.getServerApiUrl("MbylljePeriudhe", "GetMonths"),
                data: JSON.stringify({
                    yearId: yearId,
                    entityId: entityId,
                    actionId: actionId
                })
            }).done(function (result) {
                var months = model.buildMonths(result.Months);
                doneFunction(months);
                Utils.hiqLoadingGif("#periudha-view");
            });
        };

        this.buildEntities = function (entities) {
            return entities.map(function (entity) { return { id: entity.IdModuli, text: entity.PershkrimiModuli }; });
        };

        this.buildYears = function (years) {
            return years.map(function (year) { return { id: year.IdNderViti, text: year.Viti }; });
        };

        this.buildMonths = function (months) {
            return months.map(function (month) { return { id: month.IdMuaji, text: month.KodiMuaji, yearId: month.IdNdermarrjeVit, disabled: month.Disabled }; });
        };

        this.buildActions = function (actions) {
            return actions.map(function(action){ return { id: action.IdAction, text: action.ActionText }; });
        };

        this.startActionMbylljePeriudhe = function (entityId, actionId, yearId, months, doneFunction) {
            $.ajax({
                url: Utils.getServerApiUrl("MbylljePeriudhe", "StartActionMbylljePeriudhe"),
                data: JSON.stringify({
                    entityId: entityId,
                    actionId: actionId,
                    yearId: yearId,
                    months: months
                })
            }).done(function (result) {
                doneFunction(result.mesazh, model.buildRunningProcessConfigurations(result.runningProcess));
            });
        };

        this.stopActionMbylljePeriudhe = function (doneFunction) {
            $.ajax({
                url: Utils.getServerApiUrl("MbylljePeriudhe", "StopActionMbylljePeriudhe"),
                data: JSON.stringify({})
            }).done(function (result) {
                doneFunction(result);
            });
        };

        this.checkRunningProcess = function (doneFunction) {
            $.ajax({
                url: Utils.getServerApiUrl("MbylljePeriudhe", "CheckRunningProcess"),
                data: JSON.stringify({})
            }).done(function (result) {
                doneFunction(model.buildRunningProcessConfigurations(result));
            });
        };
    };

    /**Klasa e view qe eshte pergjegjese per renderizimet e elementeve te DOM te wizard-it */
    this.view = function View() {
        var view = this;
        this.wizardIds = null;
        this.wizardPopUp = null;
        this.wizardElements = {
        };

        this.onWizardPopUpHidden = null;
        this.onWizardContainerSelectionChanged = null;
        this.onWizardYearsSelectionChanged = null;
        this.onProgressBarComplete = null;

        this.renderPopUp = function (messages, content) {
            view.wizardPopUp = $(view.wizardIds.wizardPopUp).dxPopup({
                contentTemplate: function () { return content; },
                width: "50%",
                height: "65%",
                minWidth: "700px",
                minHeight: "520px",
                showcancelButton: false,
                showTitle: false,
                onHidden: view.onWizardPopUpHidden
            }).dxPopup("instance");
            view.wizardPopUp.show();
        };

        this.renderProcessProgressBar = function (options, messages) {
            view.processProgressBar = $(view.wizardIds.processProgressBar).dxProgressBar(options).dxProgressBar("instance");
            view.processProgressBar.option("onComplete", view.onProgressBarComplete);
            view.processProgressBar.option("statusFormat", function (value) { return messages.MP_lblProgress +': ' + Math.round(value * 100) + '% - ' + view.processProgressBar.option("text"); });
        };

        this.renderCheckBoxes = function (containerId, textArray, groupLength) {
            var container = $(containerId);
            view.wizardElements[containerId] = container.dxRadioGroup({
                items: textArray,
                valueExpr: "id",
                displayExpr: "text",
                value: textArray[0].id
            }).dxRadioGroup("instance");

            container.parents(".scrollable-container").dxScrollView({ height: '70%', width: '100%'});
            container.find(".dx-radiobutton").addClass("bootstrap-iso col-md-" + parseInt(12 / groupLength) + " entitete-radio-container");
            container.find(".dx-radio-value-container").addClass("entiete-radio-value");
        };

        this.renderMonths = function (actionId, containerId, textArray, groupLength, onWizardCheckBoxValueChanged) {
            var container = $(containerId);
            view.disposeRenderedCheckBoxes(containerId);
            var fieldSet = $("<div>");
            var elements = new Array();
            for (var i = 0, length = textArray.length; i < length; i++) {
                var element = textArray[i];
                var field = $("<div>").addClass("checkbox-row bootstrap-iso col-md-" + parseInt(12 / groupLength));
                var widget = $("<div>").dxCheckBox({
                    id: element.id,
                    text: element.text,
                    disabled: element.disabled,
                    onValueChanged: onWizardCheckBoxValueChanged
                });
                field.append(widget);
                fieldSet.append(field);

                elements.push(widget.dxCheckBox("instance"));
            }
            container.append(fieldSet);
            container.parents(".scrollable-container").dxScrollView({ height: '60%', width: '100%' });
            view.wizardElements[containerId] = elements;
        };

        this.disposeRenderedCheckBoxes = function (containerId) {
            $(containerId).children().remove();
            if (view.wizardElements[containerId]) {
                view.wizardElements[containerId].map(function (item) { item.dispose(); });
                view.wizardElements[containerId] = null;
            }
        };

        this.renderYearsSeletBox = function (items, value) {
            view.wizardElements[view.wizardIds.years] = $(view.wizardIds.years).dxSelectBox({
                items: items,
                displayExpr: "text",
                valueExpr: "id",
                onSelectionChanged: view.onWizardYearsSelectionChanged,
                value: value
            }).dxSelectBox("instance");
        };

        this.disposeWizardPopUp = function () {
            view.wizardPopUp.dispose();
        };
    };

    /**
     * Klasa e controller qe eshte pergjegjese per lidhjen e view me model-in e wizardit
     * @param {any} wizardPopUpState -> instanca e wizardPopUpState te wizard-it
     * @param {any} model -> instanca e model-it te wizard-it
     * @param {any} view -> instanca e view-s se wizard-it
     */
    this.controller = function (wizardPopUpState, model, view) {
        var controller = this;

        this.view = view;
        this.model = model;
        this.wizardPopUpState = wizardPopUpState;

        this.onDisposeFunction = null;
            
        this.initializeWizardPopUp = function () {
            //controller.stepper = new Stepper().init([controller.wizardPopUpState.messages.MP_lblEntitety, controller.wizardPopUpState.messages.MP_lblPcAction, controller.wizardPopUpState.messages.MP_lblPeriod, controller.wizardPopUpState.messages.MP_lblProgress], controller.wizardGoToIndex);
            controller.stepper = $("<div>").awStepper({
                items: [controller.wizardPopUpState.messages.MP_lblEntitety, controller.wizardPopUpState.messages.MP_lblPcAction, controller.wizardPopUpState.messages.MP_lblPeriod, controller.wizardPopUpState.messages.MP_lblProgress],
                disabledItems: [controller.wizardPopUpState.messages.MP_lblPeriod, controller.wizardPopUpState.messages.MP_lblProgress],
                onClick: controller.wizardGoToIndex
            }).awStepper("instance");

            controller.wizard = new Wizard().init(controller.wizardPopUpState.wizardTemplates, {
                onSelectionChanged: controller.onWizardContainerSelectionChanged,
                onSubmit: controller.wizardFinish,
                onStop: controller.wizardStop,
                onDispose: controller.wizardCancel
            }, controller.wizardPopUpState.messages, true);

            var popUpContainer = $("<div style='height: 100%;'>");
            popUpContainer.append(controller.stepper.element);
            popUpContainer.append(controller.wizard.getElement());

            controller.view.renderPopUp(controller.wizardPopUpState.messages, popUpContainer);

            controller.setupWizardConfiguration();
        };

        this.initializeWizardPopUpState = function () {
            controller.wizardPopUpState.initializeWizardPopUpState();
            controller.view.wizardIds = controller.wizardPopUpState.wizardIds;
        };

        this.initializeWizardPopUpEvents = function () {
            controller.view.onWizardPopUpHidden = controller.onWizardPopUpHidden;
            controller.view.onWizardYearsSelectionChanged = controller.onWizardYearsSelectionChanged;
            controller.view.onProgressBarComplete = controller.onProgressBarComplete;
        };

        this.onWizardPopUpHidden = function () {
            controller.disposeWizardPopUp();
        };

        this.disposeWizardPopUp = function () {
            controller.wizardPopUpState.disposeWizardPopUpState();
            controller.view.disposeWizardPopUp();
            if (controller.onDisposeFunction)
                controller.onDisposeFunction();
        };

        this.setupWizardConfiguration = function () {
            controller.model.getWizardConfiguration(controller.applyWizardConfiguration);
        };

        this.applyWizardConfiguration = function (configurations) {
            if (configurations.processRunning) {
                controller.setProgressBarOptions(configurations);
                controller.wizard.goToIndex(3);
                return;
            }
            controller.wizardPopUpState.entities = configurations.entities;
            controller.wizardPopUpState.actions = configurations.actions;
            controller.wizardPopUpState.years = configurations.years;
            controller.wizardPopUpState.months = configurations.months;
            controller.wizardPopUpState.selectedYearId = configurations.selectedYearId;
            controller.view.renderCheckBoxes(controller.view.wizardIds.entitete, controller.wizardPopUpState.entities, 2);
        };

        this.onWizardContainerSelectionChanged = function (e) {
            var currentIndex = e.addedItems[0].index;
            switch (currentIndex) {
                case 1:
                    if (!controller.view.wizardElements[controller.view.wizardIds.actions])
                        controller.view.renderCheckBoxes(controller.view.wizardIds.actions, controller.wizardPopUpState.actions, 1);
                    break;
                case 2:
                    if ($(controller.view.wizardIds.years).children().length == 0)
                        controller.view.renderYearsSeletBox(controller.wizardPopUpState.years, 0);
                    controller.model.getWizardMonths(0, controller.getSelectedEntityId(), controller.getSelectedActionId(), controller.setupMonthsCheckboxes);
                    break;
                case 3:
                    controller.stepper.disable();
                    controller.view.renderProcessProgressBar(controller.wizardPopUpState.progressBarOptions, controller.wizardPopUpState.messages);
                    setTimeout(controller.checkRunningProcess, controller.wizardPopUpState.refreshTime);
                    break;
            }
            controller.stepper.setIndex(currentIndex);
        };

        this.onWizardYearsSelectionChanged = function (e) {
            controller.wizard.disableButton("finish", true);
            if (e.selectedItem.id != controller.wizardPopUpState.months[0].yearId)
                controller.model.getWizardMonths(e.selectedItem.id, controller.getSelectedEntityId(), controller.getSelectedActionId(), controller.setupMonthsCheckboxes);
        };

        this.setupMonthsCheckboxes = function (months) {
            controller.wizardPopUpState.months = months;
            controller.view.wizardElements[controller.view.wizardIds.years].option("value", months[0].yearId);
            var actionId = controller.getSelectedActionId();
            controller.view.renderMonths(actionId, controller.view.wizardIds.months, controller.wizardPopUpState.months, 3, controller.onWizardMonthCheckBoxChanged);
        }

        this.onWizardMonthCheckBoxChanged = function (e) {
            controller.wizard.disableButton("finish", controller.getSelectedMonths().length <= 0);
            var actionId = controller.getSelectedActionId();
            var monthElements = controller.view.wizardElements[controller.view.wizardIds.months];
            var currentElementId = e.component.option("id");
            switch (actionId) {
                case 1:
                    if (e.value)
                        controller.view.wizardElements[controller.view.wizardIds.months].map(function (checkbox) { if (checkbox.option("id") < currentElementId && !checkbox.option("disabled")) checkbox.option("value", e.value); });
                    else
                        controller.view.wizardElements[controller.view.wizardIds.months].map(function (checkbox) { if (checkbox.option("id") > currentElementId) checkbox.option("value", e.value); });
                    break;
                case 2:
                    if (e.value)
                        controller.view.wizardElements[controller.view.wizardIds.months].map(function (checkbox) { if (checkbox.option("id") > currentElementId && !checkbox.option("disabled")) checkbox.option("value", e.value); });
                    else
                        controller.view.wizardElements[controller.view.wizardIds.months].map(function (checkbox) { if (checkbox.option("id") < currentElementId) checkbox.option("value", e.value); });
                    break;
            }
        };

        this.wizardGoToIndex = function (index) {
            if (index == 3) {
                if (controller.getSelectedMonths().length <= 0) {
                    myMesazh.ShtoMesazhGabimi("Ju nuk keni asnje muaj te zgjedhur.");
                    controller.stepper.setIndex(2);
                }
                else
                    controller.wizardFinish();
                return;
            }
            controller.wizard.goToIndex(index);
        };

        this.wizardFinish = function () {
            controller.shtoPyetje(controller.wizardPopUpState.messages.MP_msgStartProcess, controller.wizardStartProcess, function () { controller.stepper.setIndex(2); });
        };

        this.wizardStartProcess = function () {
            var entityId = controller.getSelectedEntityId();
            var actionId = controller.getSelectedActionId();
            var yearId = controller.getSelectedYearId();
            var months = controller.getSelectedMonths();
            controller.model.startActionMbylljePeriudhe(entityId, actionId, yearId, months, controller.wizardProcessStarted);
        }

        this.wizardProcessStarted = function (mesazh, runningProcessConfigurations) {
            if (mesazh.Status) {
                myMesazh.ShtoMesazhInformues(mesazh.PershkrimMesazhi);
                controller.setProgressBarOptions(runningProcessConfigurations);
                controller.wizard.goToIndex(3);
            }
            else
                myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi)
        }
        this.wizardCancel = function () {
            controller.view.wizardPopUp.hide();
        };

        this.wizardStop = function () {
            controller.shtoPyetje(controller.wizardPopUpState.messages.MP_msgStopProcess, controller.wizardStopProcess, function () { });
        };

        this.wizardStopProcess = function () {
            controller.model.stopActionMbylljePeriudhe(controller.wizardProcessStoped);
        };

        this.wizardProcessStoped = function (result) {
            myMesazh.ShtoMesazhInformues(result.PershkrimMesazhi);
            controller.view.wizardPopUp.hide();
        };

        this.onProgressBarComplete = function (e) {
            e.element.addClass("complete-wizard-progressBar");
        };

        this.shtoPyetje = function (mesazhi, poClick, joClick) {
            window.parent.myMesazh.ShtoMesazh({
                type: "confirm",
                UseCancelButton: true,
                text: mesazhi,
                modal: true,
                idGjuha: Utils.getApplicationLanguageId(),
                okClick: poClick,
                cancelClick: joClick
            });
        };

        this.setProgressBarOptions = function (options) {
            controller.wizardPopUpState.progressBarOptions = options;
        };

        this.retrieveWizardData = function () {
            return { selectedEntity: controller.getSelectedEntityId(), selectedAction: controller.getSelectedActionId(), selectedMonths: controller.getSelectedMonths() };
        };

        this.getSelectedEntityId = function () {
            var entityId = controller.view.wizardElements[controller.view.wizardIds.entitete].option("value");
            return entityId ? entityId : -1;
        };

        this.getSelectedActionId = function () {
            var actionId = controller.view.wizardElements[controller.view.wizardIds.actions].option("value");
            return actionId ? actionId : -1;
        };

        this.getSelectedYearId = function () {
            var yearId = controller.view.wizardElements[controller.view.wizardIds.years].option("value");
            return yearId ? yearId : -1;
        };

        this.getSelectedMonths = function () {
            return controller.view.wizardElements[controller.view.wizardIds.months].filter(function (checkbox) { return checkbox.option("value") == true; }).map(function (checkbox) { return checkbox.option("id"); });
        };

        this.checkRunningProcess = function () {
            if (!controller.view.processProgressBar.option("processRunning")) {
                return;
            }
            controller.model.checkRunningProcess(controller.doneCheckRunningProcess);
            setTimeout(controller.checkRunningProcess, controller.wizardPopUpState.refreshTime);
        };

        this.doneCheckRunningProcess = function (result) {
            if (!result.processRunning)
                controller.wizard.disableButton("stop", true);
            controller.view.processProgressBar.option("processRunning", result.processRunning);
            controller.view.processProgressBar.option("text", result.text);
            controller.view.processProgressBar.option("max", result.max);
            controller.view.processProgressBar.option("value", result.value);
        };

        (function (controller) {
            controller.initializeWizardPopUpState();
            controller.initializeWizardPopUpEvents();
            controller.initializeWizardPopUp();
        }(controller));
    };

    return {
        init: function (messages, onDisposeFunction) {
            var wizardPopUpState = new app.wizardPopUpState(); //inicializohet wizardPopUpState
            wizardPopUpState.messages = messages;
            var model = new app.model(); //inicializohet model-i
            var view = new app.view(); //inicializohet viewe-ri
            var controller = new app.controller(wizardPopUpState, model, view); //inicializohet controller-i
            controller.onDisposeFunction = onDisposeFunction;
        }
    };

};