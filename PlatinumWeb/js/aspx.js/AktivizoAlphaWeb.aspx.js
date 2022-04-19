;
//funksion qe sherben per te bere aktiv butonin e Instalimit
function OnTextChangedHandler() {
    if (validoTextBox()) {
        btnInstalo.SetEnabled(true);
    }
}

//Kontrollon vleren e textbox per vlere boshe ose gjatesi karakteshesh me te vogel se 5, nqs plotesohet ky kusht qofte dhe per nje nga textbox kthehet vlera false
//Nqs te gjithe textbox e kalojne kontrollin me sukses kthehet true
function validoTextBox() {
 
    if (txtKey1.GetText() == '' || txtKey1.GetText().length < 5)
        return false;
    if (txtKey2.GetText() == '' || txtKey2.GetText().length < 5)
        return false;
    if (txtKey3.GetText() == '' || txtKey3.GetText().length < 5)
        return false;
    if (txtKey4.GetText() == '' || txtKey5.GetText().length < 5)
        return false;
    if (txtKey5.GetText() == '' || txtKey5.GetText().length < 5)
        return false;

    return true;
}
function TextChanged_txtKey1(s, e) {
    OnTextChangedHandler();
}
function TextChanged_txtKey2(s, e) {
    OnTextChangedHandler();
}
function TextChanged_txtKey3(s, e) {
    OnTextChangedHandler();
}
function TextChanged_txtKey4(s, e) {
    OnTextChangedHandler();
}
function TextChanged_txtKey5(s, e) {
    OnTextChangedHandler();
}

