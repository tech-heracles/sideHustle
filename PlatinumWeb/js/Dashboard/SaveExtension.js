;
// Creates and implements a custom SaveDashboardExtension class.

function SaveDashboardExtension(dashboardControl, messages) {
    var _this = this;
    this._control = dashboardControl;
    this._toolbox = this._control.findExtension("toolbox");
    this._newDashboardExtension = this._control.findExtension("save-dashboard");
    this.name = "dxdde-dashboard-save";
    this._menuItem = {
        id: "dashboard-save",
        title: messages.msg_D_btnSave,
        selected: ko.observable(false),
        disabled: ko.computed(function () { return !dashboardControl.dashboard(); }),
        index: 111,
        data: _this,
        click: function () {
            if (_this.isExtensionAvailable()) {
                _this._control.data = "save";
                _this._toolbox.closeMenu();
                _this._newDashboardExtension.performSaveDashboard(_this._control.getDashboardId(), _this._control.dashboard().getJSON());
            }
        }
    };
};

SaveDashboardExtension.prototype.isExtensionAvailable = function () {
    return this._toolbox !== undefined && this._newDashboardExtension !== undefined;
};

SaveDashboardExtension.prototype.start = function () {
    if (this.isExtensionAvailable())
        this._toolbox.menuItems.push(this._menuItem);
};
SaveDashboardExtension.prototype.stop = function () {
    if (this.isExtensionAvailable())
        this._toolbox.menuItems.remove(this._menuItem);
};

SaveDashboardExtension.prototype.IsDashboardDirty = function () {
    return this._newDashboardExtension._isDashboardDirty();
};

SaveDashboardExtension.prototype.SaveDashboard = function (methodToCallOnSave) {
    this._menuItem.click();
};