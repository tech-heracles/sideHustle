; if (typeof myFushaShtese == 'undefined') {
    myFushaShtese = {};
}
//merr vlerat e fushave shtese
myFushaShtese.ShtoStringOrDate = function (editor, key, arrVlerat) {//rasti per string dhe date
   // arrVlerat[key] =  editor.GetText();
 
    return arrVlerat;
}
myFushaShtese.ShtoIntOrDouble = function (editor, key, arrVlerat) {//rasti per int dhe double
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        alert('Vlerat duhet te jene numerike');
    }
    //else {
    //    arrVlerat[key] = editor.GetText();
       
    //}
    return arrVlerat;
}
myFushaShtese.ShtoCheck = function (editor, key, arrVlerat) {//rasti per check box
  //  arrVlerat[key] = editor.GetChecked();
       return arrVlerat;
}
myFushaShtese.ShtoList = function (editor, key, arrVlerat) {//rasti per list box
   // arrVlerat[key] = editor.GetText();
   
    return arrVlerat;
};