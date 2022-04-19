var indexOf = function(needle) {
    if(typeof Array.prototype.indexOf === 'function') {//ne InternetExplorer nuk eshte ky funksion
  
        indexOf = Array.prototype.indexOf;
    } else {
     
        indexOf = function(needle) {
            var i = -1, index = -1;

            for(i = 0; i < this.length; i++) {
                if(this[i] === needle) {
                    index = i;
                    break;
                }
            }

            return index;
        };
    }

    return indexOf.call(this, needle);
};


