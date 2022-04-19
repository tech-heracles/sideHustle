// Creates and implements a custom SaveAsDashboardExtension class.
;
function SaveAsDashboardExtension(dashboardControl, messages) {
    var _this = this;
    this._control = dashboardControl;
    this._toolbox = this._control.findExtension("toolbox");
    this._newDashboardExtension = this._control.findExtension("save-dashboard");
    this.name = "dxdde-dashboard-save-as";
    this._menuItem = {
        id: "dashboard-save-as",
        title: messages.msg_D_btnSaveAs,
        template: "dx-save-as-form",
        selected: ko.observable(false),
        disabled: ko.computed(function () { return !dashboardControl.dashboard(); }),
        index: 112,
        data: _this
    };
    this.saveAs = function () {
        if (this.isExtensionAvailable()) {
            if ($.trim(this.newName())) {
                this._control.data = "save-as";
                this._toolbox.closeMenu();
                this._newDashboardExtension.performSaveDashboard("saveAs;" + this._control.getDashboardId() + ";" + this.newName(), this._control.dashboard().getJSON());
            }
            else
                myMesazh.ShtoMesazhGabimi(messages.msg_D_saveEmptyNameException);
        }
    };
    this.newName = ko.observable("New Dashboard Name");
    this.saveTitle = ko.observable(messages.msg_D_btnSave);
    this.saveAsName = ko.observable(messages.msg_D_columnName + ":");
};

SaveAsDashboardExtension.prototype.isExtensionAvailable = function () {
    return this._toolbox !== undefined && this._newDashboardExtension !== undefined;
};

SaveAsDashboardExtension.prototype.start = function () {
    if (this.isExtensionAvailable())
        this._toolbox.menuItems.push(this._menuItem);
};
SaveAsDashboardExtension.prototype.stop = function () {
    if (this.isExtensionAvailable())
        this._toolbox.menuItems.remove(this._menuItem);
};