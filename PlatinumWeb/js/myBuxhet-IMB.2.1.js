; if (typeof myBuxhet == 'undefined') {
    myBuxhet = {};
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
myBuxhet.ShtoTotal1 = function (editor, edgjendja, eddiff, hidField, counter) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        alert('Vlera e totalit te buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() == '') {
        editor.SetFocus();
        alert('Jepni vleren e totalit te buxhetit');
    }
    else {
        var i;
        var s = parseFloat(editor.GetText()) / 12;
        hidField.value = '';

        for (i = 1; i < 13; i++) {
            window.arr[counter] = i + ":" + s;
            counter += 1;
        }
        hidField.value = window.arr;
        textboxBuxh11.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh12.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh13.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh14.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh15.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh16.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh17.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh18.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh19.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh110.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh111.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh112.SetText(parseFloat(editor.GetText()) / 12);
        if (edgjendja != null) {
            eddiff.SetText(editor.GetText() - edgjendja.GetText());
            labelDiff11.SetText(textboxBuxh11.GetText() - labelGjendja1.GetText());
            labelDiff12.SetText(textboxBuxh12.GetText() - labelGjendja2.GetText());
            labelDiff13.SetText(textboxBuxh13.GetText() - labelGjendja3.GetText());
            labelDiff14.SetText(textboxBuxh14.GetText() - labelGjendja4.GetText());
            labelDiff15.SetText(textboxBuxh15.GetText() - labelGjendja5.GetText());
            labelDiff16.SetText(textboxBuxh16.GetText() - labelGjendja6.GetText());
            labelDiff17.SetText(textboxBuxh17.GetText() - labelGjendja7.GetText());
            labelDiff18.SetText(textboxBuxh18.GetText() - labelGjendja8.GetText());
            labelDiff19.SetText(textboxBuxh19.GetText() - labelGjendja9.GetText());
            labelDiff110.SetText(textboxBuxh110.GetText() - labelGjendja10.GetText());
            labelDiff111.SetText(textboxBuxh111.GetText() - labelGjendja11.GetText());
            labelDiff112.SetText(textboxBuxh112.GetText() - labelGjendja12.GetText());
        }
    }
    return counter;
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
myBuxhet.ShtoTotal2 = function (editor, edgjendja, eddiff, hidField2, counter2) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        alert('Vlera e totalit te buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() == '') {
        editor.SetFocus();
        alert('Jepni vleren e totalit te buxhetit');
    }
    else {
        var i;
        var s = parseFloat(editor.GetText()) / 12;
        hidField2.value = '';

        for (i = 1; i < 13; i++) {
            window.arr2[counter2] = i + ":" + s;
            counter2 += 1;
        }
        hidField2.value = window.arr2;
        textboxBuxh21.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh22.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh23.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh24.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh25.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh26.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh27.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh28.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh29.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh210.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh211.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh212.SetText(parseFloat(editor.GetText()) / 12);
        if (edgjendja != null) {
            eddiff.SetText(editor.GetText() - edgjendja.GetText());
            labelDiff21.SetText(textboxBuxh21.GetText() - labelGjendja1.GetText());
            labelDiff22.SetText(textboxBuxh22.GetText() - labelGjendja2.GetText());
            labelDiff23.SetText(textboxBuxh23.GetText() - labelGjendja3.GetText());
            labelDiff24.SetText(textboxBuxh24.GetText() - labelGjendja4.GetText());
            labelDiff25.SetText(textboxBuxh25.GetText() - labelGjendja5.GetText());
            labelDiff26.SetText(textboxBuxh26.GetText() - labelGjendja6.GetText());
            labelDiff27.SetText(textboxBuxh27.GetText() - labelGjendja7.GetText());
            labelDiff28.SetText(textboxBuxh28.GetText() - labelGjendja8.GetText());
            labelDiff29.SetText(textboxBuxh29.GetText() - labelGjendja9.GetText());
            labelDiff210.SetText(textboxBuxh210.GetText() - labelGjendja10.GetText());
            labelDiff211.SetText(textboxBuxh211.GetText() - labelGjendja11.GetText());
            labelDiff212.SetText(textboxBuxh212.GetText() - labelGjendja12.GetText());
        }
    }
    return counter2;
}

myBuxhet.ShtoBuxhet1 = function (editor, edgjendja, eddiff, key, hidField, counter) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        alert('Vlerat e buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() == '') {
        editor.SetFocus();
        alert('Jepni vleren e buxhetit');
    }
    else {
        var shuma = 0;
        window.arr[counter] = key.toString() + ":" + editor.GetText();
        counter += 1;
        hidField.value = window.arr;
        shuma = parseFloat(textboxBuxh11.GetText()) + parseFloat(textboxBuxh112.GetText()) + parseFloat(textboxBuxh12.GetText()) + parseFloat(textboxBuxh13.GetText()) + parseFloat(textboxBuxh14.GetText()) + parseFloat(textboxBuxh15.GetText()) + parseFloat(textboxBuxh16.GetText()) + parseFloat(textboxBuxh17.GetText()) + parseFloat(textboxBuxh18.GetText()) + parseFloat(textboxBuxh19.GetText()) + parseFloat(textboxBuxh110.GetText()) + parseFloat(textboxBuxh111.GetText());
        textboxBuxh10.SetText(shuma);
        if (edgjendja != null) {
            eddiff.SetText(editor.GetText() - edgjendja.GetText());
            labelDiff10.SetText(textboxBuxh10.GetText() - labelGjendja0.GetText());
        }
    }
    return counter;
}
//metoda per te marre vlerat e ndryshuara te buxhetit te dyte dhe per te ndryshuar differencen dhe totalin
myBuxhet.ShtoBuxhet2 = function (editor, edgjendja, eddiff, key, hidField2, counter2) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        alert('Vlerat e buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() == '') {
        editor.SetFocus();
        alert('Jepni vleren e buxhetit');
    }
    else {
        var shuma = 0;
        window.arr2[counter2] = key.toString() + ":" + editor.GetText();
        counter2 += 1;
        hidField2.value = window.arr2;
        shuma = parseFloat(textboxBuxh21.GetText()) + parseFloat(textboxBuxh212.GetText()) + parseFloat(textboxBuxh22.GetText()) + parseFloat(textboxBuxh23.GetText()) + parseFloat(textboxBuxh24.GetText()) + parseFloat(textboxBuxh25.GetText()) + parseFloat(textboxBuxh26.GetText()) + parseFloat(textboxBuxh27.GetText()) + parseFloat(textboxBuxh28.GetText()) + parseFloat(textboxBuxh29.GetText()) + parseFloat(textboxBuxh210.GetText()) + parseFloat(textboxBuxh211.GetText());
        textboxBuxh20.SetText(shuma);
        if (edgjendja != null) {
            eddiff.SetText(editor.GetText() - edgjendja.GetText());
            labelDiff20.SetText(textboxBuxh20.GetText() - labelGjendja0.GetText());
        }
    }
    return counter2;
}
//metoda per te marre vlerat e ndryshuara te buxhetit te pare dhe per te ndryshuar differencen dhe totalin
myBuxhet.ruajBuxhet = function (hidField, hidField2, hidFieldShenime) {
    var editorBuxhet1, editorBuxhet2;
    hidField.value = '';
    hidField2.value = '';
    if (hidFieldShenime != undefined)
        hidFieldShenime.value = '';
    for (i = 1; i < 13; i++) {
        editorBuxhet1 = Utils.ktheKontroll("textboxBuxh1" + i);
        editorBuxhet2 = Utils.ktheKontroll("textboxBuxh2" + i);
        if (i != 12) {
            if (editorBuxhet1.GetText() != '')
                hidField.value = hidField.value + i + ":" + editorBuxhet1.GetText() + ",";
            if (editorBuxhet2.GetText() != '')
                hidField2.value = hidField2.value + i + ":" + editorBuxhet2.GetText() + ",";
        }
        else {
            if (editorBuxhet1.GetText() != '')
                hidField.value = hidField.value + i + ":" + editorBuxhet1.GetText();
            if (editorBuxhet2.GetText() != '')
                hidField2.value = hidField2.value + i + ":" + editorBuxhet2.GetText();
        }
        if (hidFieldShenime != undefined)
            hidFieldShenime.value = hidFieldShenime.value + Utils.ktheKontroll("txtShenime" + i).GetText() + (i == 12 ? "" : ",");
    }
}
myBuxhet.ShtoBuxhet1Pas = function (editor, edgjendja, eddiff, key, buxh) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Vlerat e buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() === '') {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Jepni vleren e buxhetit');
    }
    else {
        var shuma = 0;
        buxh[key].Buxheti_1 = editor.GetText();
        buxh[key].Diferenca_1 = editor.GetText() - edgjendja.GetText();
        eddiff.SetText(editor.GetText() - edgjendja.GetText());
        shuma = parseFloat(textboxBuxh11.GetText()) + parseFloat(textboxBuxh112.GetText()) + parseFloat(textboxBuxh12.GetText()) + parseFloat(textboxBuxh13.GetText()) + parseFloat(textboxBuxh14.GetText()) + parseFloat(textboxBuxh15.GetText()) + parseFloat(textboxBuxh16.GetText()) + parseFloat(textboxBuxh17.GetText()) + parseFloat(textboxBuxh18.GetText()) + parseFloat(textboxBuxh19.GetText()) + parseFloat(textboxBuxh110.GetText()) + parseFloat(textboxBuxh111.GetText());
        textboxBuxh10.SetText(shuma);
        labelDiff10.SetText(textboxBuxh10.GetText() - labelGjendja0.GetText());
        buxh[0].Buxheti_1 = shuma;
        buxh[0].Diferenca_1 = textboxBuxh10.GetText() - labelGjendja0.GetText();
    }
}
//metoda per te marre vlerat e ndryshuara te buxhetit te dyte dhe per te ndryshuar differencen dhe totalin
myBuxhet.ShtoBuxhet2Pas = function (editor, edgjendja, eddiff, key, buxh) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Vlerat e buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() === '') {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Jepni vleren e buxhetit');
    }
    else {
        var shuma = 0;
        buxh[key].Buxheti_2 = editor.GetText();
        buxh[key].Diferenca_2 = editor.GetText() - edgjendja.GetText();
        eddiff.SetText(editor.GetText() - edgjendja.GetText());
        shuma = parseFloat(textboxBuxh21.GetText()) + parseFloat(textboxBuxh212.GetText()) + parseFloat(textboxBuxh22.GetText()) + parseFloat(textboxBuxh23.GetText()) + parseFloat(textboxBuxh24.GetText()) + parseFloat(textboxBuxh25.GetText()) + parseFloat(textboxBuxh26.GetText()) + parseFloat(textboxBuxh27.GetText()) + parseFloat(textboxBuxh28.GetText()) + parseFloat(textboxBuxh29.GetText()) + parseFloat(textboxBuxh210.GetText()) + parseFloat(textboxBuxh211.GetText());
        textboxBuxh20.SetText(shuma);
        labelDiff20.SetText(textboxBuxh20.GetText() - labelGjendja0.GetText());
        buxh[0].Buxheti_2 = shuma;
        buxh[0].Diferenca_2 = textboxBuxh20.GetText() - labelGjendja0.GetText();
    }
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
myBuxhet.ShtoTotal1Pas = function (editor, edgjendja, eddiff, buxh) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Vlera e totalit te buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() === '') {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Jepni vleren e totalit te buxhetit');
    }
    else {
        var s = parseFloat(editor.GetText()) / 12;

        for (var i = 1; i < 13; i++) {
            buxh[i].Buxheti_1 = s;
            buxh[i].Diferenca_1 = s;
        }
        buxh[0].Buxheti_1 = editor.GetText();
        buxh[0].Diferenca_1 = editor.GetText() - edgjendja.GetText();
        eddiff.SetText(editor.GetText() - edgjendja.GetText());
        textboxBuxh11.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh12.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh13.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh14.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh15.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh16.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh17.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh18.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh19.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh110.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh111.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh112.SetText(parseFloat(editor.GetText()) / 12);
        labelDiff11.SetText(textboxBuxh11.GetText() - labelGjendja1.GetText());
        labelDiff12.SetText(textboxBuxh12.GetText() - labelGjendja2.GetText());
        labelDiff13.SetText(textboxBuxh13.GetText() - labelGjendja3.GetText());
        labelDiff14.SetText(textboxBuxh14.GetText() - labelGjendja4.GetText());
        labelDiff15.SetText(textboxBuxh15.GetText() - labelGjendja5.GetText());
        labelDiff16.SetText(textboxBuxh16.GetText() - labelGjendja6.GetText());
        labelDiff17.SetText(textboxBuxh17.GetText() - labelGjendja7.GetText());
        labelDiff18.SetText(textboxBuxh18.GetText() - labelGjendja8.GetText());
        labelDiff19.SetText(textboxBuxh19.GetText() - labelGjendja9.GetText());
        labelDiff110.SetText(textboxBuxh110.GetText() - labelGjendja10.GetText());
        labelDiff111.SetText(textboxBuxh111.GetText() - labelGjendja11.GetText());
        labelDiff112.SetText(textboxBuxh112.GetText() - labelGjendja12.GetText());
    }
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
myBuxhet.ShtoTotal2Pas = function (editor, edgjendja, eddiff, buxh) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Vlera e totalit te buxhetit duhet te jene numerike');
    }
    else if (editor.GetText() === '') {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi('Jepni vleren e totalit te buxhetit');
    }
    else {
        var s = parseFloat(editor.GetText()) / 12;

        for (var i = 1; i < 13; i++) {
            buxh[i].Buxheti_2 = s;
            buxh[i].Diferenca_2 = s;
        }
        buxh[0].Buxheti_2 = editor.GetText();
        buxh[0].Diferenca_2 = editor.GetText() - edgjendja.GetText();
        eddiff.SetText(editor.GetText() - edgjendja.GetText());
        textboxBuxh21.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh22.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh23.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh24.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh25.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh26.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh27.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh28.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh29.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh210.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh211.SetText(parseFloat(editor.GetText()) / 12);
        textboxBuxh212.SetText(parseFloat(editor.GetText()) / 12);
        labelDiff21.SetText(textboxBuxh21.GetText() - labelGjendja1.GetText());
        labelDiff22.SetText(textboxBuxh22.GetText() - labelGjendja2.GetText());
        labelDiff23.SetText(textboxBuxh23.GetText() - labelGjendja3.GetText());
        labelDiff24.SetText(textboxBuxh24.GetText() - labelGjendja4.GetText());
        labelDiff25.SetText(textboxBuxh25.GetText() - labelGjendja5.GetText());
        labelDiff26.SetText(textboxBuxh26.GetText() - labelGjendja6.GetText());
        labelDiff27.SetText(textboxBuxh27.GetText() - labelGjendja7.GetText());
        labelDiff28.SetText(textboxBuxh28.GetText() - labelGjendja8.GetText());
        labelDiff29.SetText(textboxBuxh29.GetText() - labelGjendja9.GetText());
        labelDiff210.SetText(textboxBuxh210.GetText() - labelGjendja10.GetText());
        labelDiff211.SetText(textboxBuxh211.GetText() - labelGjendja11.GetText());
        labelDiff212.SetText(textboxBuxh212.GetText() - labelGjendja12.GetText());
    }
};