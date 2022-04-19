function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
       
    }
    catch (err) {
        ///alert('gabim');    
    }
}
function EndRequestHandler(sender, args) {//po
    if (mbyll) {
        if (Utils.getUrlVar('ruaj') == 'po') {
            try {
                if (window.parent["callWebServiceInfoRow"] != undefined)
                    window.parent.callWebServiceInfoRow();
                if (window.parent["callWebServiceInfoKF"] != undefined)
                    window.parent.callWebServiceInfoKF()
            } catch (e) {
                console.log(e);
            }
        }
        window.parent.popupUniversal.Hide();
    }
}
  var mbyll = false;
function menu_click(e) {

    if (e.item.name == "OK") {
        mbyll = true;
    }
    else mbyll = false;
}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
  
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);

    }
    catch (e) {
    }
}).trigger('resize');

function Kalo1(srcListBox, dstListBox) {
    dstListBox.BeginUpdate();
    srcListBox.BeginUpdate();
    var item = srcListBox.GetSelectedItem();
    dstListBox.AddItem(item.text, item.value);
    srcListBox.RemoveItem(item.index);
    dstListBox.EndUpdate();
    srcListBox.EndUpdate();
    if (item.index < srcListBox.GetItemCount())
      srcListBox.SetSelectedIndex(item.index);
     else     srcListBox.SetSelectedIndex(item.index-1);
    UpdateButtonState();
}
function TeGjithe(srcListBox, dstListBox) {
    srcListBox.BeginUpdate();
    var count = srcListBox.GetItemCount();
    for (var i = 0; i < count; i++) {
        var item = srcListBox.GetItem(i);
        dstListBox.AddItem(item.text, item.value);
    }
    srcListBox.EndUpdate();
    srcListBox.ClearItems();
    UpdateButtonState();
}
function LevizSiper(srcListBox) {
    srcListBox.BeginUpdate();
    var item = srcListBox.GetSelectedItem();
    srcListBox.RemoveItem(item.index);
    srcListBox.InsertItem(item.index-1,item.text, item.value);

    srcListBox.EndUpdate(); srcListBox.SetSelectedIndex(item.index-1);
    UpdateButtonState();
}
function LevizPoshte(srcListBox) {
    srcListBox.BeginUpdate();
    var item = srcListBox.GetSelectedItem();
    srcListBox.RemoveItem(item.index);
    srcListBox.InsertItem(item.index+1,item.text, item.value);

    srcListBox.EndUpdate(); srcListBox.SetSelectedIndex(item.index+1);
    UpdateButtonState();
}
function UpdateButtonState() {
    btnDjathtasGjitha.SetEnabled(lbxFushat.GetItemCount() > 0);
    btnMajtaGjitha.SetEnabled(lbxZgjedhur.GetItemCount() > 0);
    btnDjathtas1.SetEnabled(lbxFushat.GetSelectedItems().length > 0);
    btnMajtas1.SetEnabled(lbxZgjedhur.GetSelectedItems().length > 0);
    btnSiper.SetEnabled(lbxZgjedhur.GetSelectedItems().length > 0 && lbxZgjedhur.GetSelectedItem().index > 0);
    btnPoshte.SetEnabled(lbxZgjedhur.GetSelectedItems().length > 0 && lbxZgjedhur.GetSelectedItem().index < lbxZgjedhur.GetItemCount() - 1);
}