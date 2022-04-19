;

function memory(Identifikues) {
    this.lloji = Identifikues;
    this.lista = [];
    this.Get = function (idKodi) {
        if (this.lista.length === 0 || idKodi === '' || isNaN(parseInt(idKodi)))
            return null;
        var idKodiNum = parseInt(idKodi);
        if (typeof idKodi === undefined)
            return this.lista;
        var identifikuesi = this.lloji;
        var result = $.grep(this.lista, function (e) {
            return e[identifikuesi] === idKodiNum;
        });
        if (result.length == 1)
            return result[0];
        if (result.length > 1)
            console.log("gabim: objekti ndodhet me shume se nje here ne gride");
        return null;
    };
    this.GetAll = function () {
        return this.lista;
    };
    this.Set = function (item) {
        var identifikuesi = this.lloji;
        var result = $.grep(this.lista, function (e) {
            return e[identifikuesi] !== item[identifikuesi];
        });
        result.push(item);
        this.lista = result;
        return this;
    };
    this.Contains = function (idKodi) {
        var result = this.Get(idKodi);
        if (result)
            return result;
        return false;
    };
};