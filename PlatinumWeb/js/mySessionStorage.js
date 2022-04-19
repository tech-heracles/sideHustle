function mySessionStorage() {
    this.pageKeys = new Array();

    this.setItem = function (key, value) {
        this.addKey(key);
        sessionStorage.setItem(key, value);
    };

    this.getItem = function (key) {
        return sessionStorage.getItem(key);
    };

    this.setObject = function (key, object) {
        this.setItem(key, JSON.stringify(object));
    };

    this.getObject = function (key) {
        return JSON.parse(this.getItem(key));
    };

    this.removeByKey = function (key) {
        sessionStorage.removeItem(key);
        this.pageKeys.splice(this.pageKeys.indexOf(key), 1);
    };

    this.removeAll = function () {
        for (var i = this.pageKeys.length; i > 0; i--) {
            this.removeByKey(this.pageKeys[i - 1]);
        }
    };

    this.addKey = function (key) {
        if (this.pageKeys.filter(function (savedKey) { return savedKey == key }).length < 1)
            this.pageKeys.push(key);
    };
}
;