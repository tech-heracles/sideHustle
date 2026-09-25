; if (typeof myFushaShtese == 'undefined') {
    myFushaShtese = {};
}
//merr vlerat e fushave shtese
myFushaShtese.ShtoStringOrDate = function (editor, key, arrVlerat) {//rasti per string dhe date
 
    return arrVlerat;
}
myFushaShtese.ShtoIntOrDouble = function (editor, key, arrVlerat) {//rasti per int dhe double
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        alert('Vlerat duhet te jene numerike');
    }
       
    return arrVlerat;
}
myFushaShtese.ShtoCheck = function (editor, key, arrVlerat) {//rasti per check box
       return arrVlerat;
}
myFushaShtese.ShtoList = function (editor, key, arrVlerat) {//rasti per list box
   
    return arrVlerat;
};