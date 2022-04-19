;
if (typeof myButtonClickLupa == 'undefined') {
    myButtonClickLupa = {};
}
myButtonClickLupa.ButtonClickFurnitori = function (headerText, queryStr, klientfurnitor, widthLupaKF, heightLupaKF) {
    popupUniversal.SetHeaderText(headerText);
    if (klientfurnitor == "Klient")
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=1&idKonfigAmbjente=' + queryStr);
    else if (klientfurnitor == "Furnitor")
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=2&idKonfigAmbjente=' + queryStr);
    else popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
};
myButtonClickLupa.ButtonClickKerko = function (headerText, veprimi, widthLupaKerko, heightLupaKerko) {

    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?veprimi=' + veprimi);
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
};
myButtonClickLupa.ButtonClickLlogaria = function (headerText, queryStr, widthLupaLlogari, heightLupaLlogari) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaLlogaria.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaLlogari, heightLupaLlogari);
    popupUniversal.Show();
};
myButtonClickLupa.KPF_Click = function (headerText, queryStr, widthLupaKPF, heightLupaKPF, id) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaKPF.aspx?id=' + id + '&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKPF, heightLupaKPF);
    popupUniversal.Show();
};
myButtonClickLupa.Grupi_Click = function (headerText, queryStr, widthLupaGrupi, heightLupaGrupi) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaGrupLlogari.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaGrupi, heightLupaGrupi);
    popupUniversal.Show();
};
myButtonClickLupa.LupaUniversal_Click = function (headerText, ContentUrl, widthLupa, heightLupa) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl(ContentUrl);
    popupUniversal.SetSize(widthLupa, heightLupa);
    popupUniversal.Show();
};
myButtonClickLupa.Nengrupi_Click = function (headerText, queryStr, widthLupaNenGrupi, heightLupaNenGrupi, idGrupi) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaNengrupLlogari.aspx?idgrupi=' + idGrupi + '&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaNenGrupi, heightLupaNenGrupi);
    popupUniversal.Show();
};
myButtonClickLupa.Autorizime_Click = function (headerText, queryStr, widthLupaAutorizime, heightLupaAutorizime) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaAutorizim.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaAutorizime, heightLupaAutorizime);
    popupUniversal.Show();
};
myButtonClickLupa.ElementePerIntegrim_Click = function (headerText, idkonfigAmbjenti, widthLupa, heightLupa, lloji) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaElementePerIntegrim.aspx?lloji=' + lloji + '&idKonfigAmbjente=' + idkonfigAmbjenti);
    popupUniversal.SetSize(widthLupa, heightLupa);
    popupUniversal.Show();
};
myButtonClickLupa.KodifikimArtikulli_Click = function (queryStr, widthLupaKodifikime, heightLupaKodifikime) {
    popupUniversal.SetHeaderText('Zgjidh grupin e artikullit');
    popupUniversal.SetContentUrl('LupaKodifikimArtikulli.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKodifikime, heightLupaKodifikime);
    popupUniversal.Show();
};
myButtonClickLupa.DetajimArtikulli_Click = function (queryStr, widthLupaDetajime, heightLupaDetajime, value) {
    popupUniversal.SetHeaderText('Zgjidh detajimet e artikullit');
    if (value != "")
        popupUniversal.SetContentUrl('LupaDetajime.aspx?veprimi=' + value + '&idKonfigAmbjente=' + queryStr);
    else popupUniversal.SetContentUrl('LupaDetajime.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaDetajime, heightLupaDetajime);
    popupUniversal.Show();
};
myButtonClickLupa.Skema_Click = function (headerText, queryStr, widthLupaSkema, heightLupaSkema, klasa, parametri) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaSkemaKontabelArtikulli.aspx?value=' + klasa + '&idKonfigAmbjente=' + queryStr + '&llojiart=' + parametri);
    popupUniversal.SetSize(widthLupaSkema, heightLupaSkema);
    popupUniversal.Show();
};
myButtonClickLupa.Kodbare_Click = function (headerText, queryStr, widthLupaKodbare, heightLupaKodbare, kodbar) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaKodbare.aspx?value=' + kodbar + '&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKodbare, heightLupaKodbare);
    popupUniversal.Show();
};
myButtonClickLupa.ShtoArtikull_Click = function (queryStr, widthLupaShtoArt, heightLupaShtoArt, artikull, emertime, prioritete) {
    popupUniversal.SetHeaderText('Vendosni artikujt');
    popupUniversal.SetContentUrl('LupaShtoArtikull.aspx?value=' + artikull + '&emri=' + emertime + '&prioriteti=' + prioritete + '&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaShtoArt, heightLupaShtoArt);
    popupUniversal.Show();
};
myButtonClickLupa.GrupiBanka_Click = function (header, queryStr, widthLupaGrupiBanka, heightLupaGrupiBanka, lloji) {//thiret popup i grupe banke
    popupUniversal.SetHeaderText(header);
    popupUniversal.SetContentUrl('LupaGrupBanke.aspx?veprimi=' + lloji + '&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaGrupiBanka, heightLupaGrupiBanka);
    popupUniversal.Show();
};
myButtonClickLupa.AfateMaturimi_Click = function (headerText, queryStr, widthLupaMaturimi, heightLupaMaturimi, lloji) {
    if (lloji == 'Klient')
        popupUniversal.SetContentUrl('LupaAfateMaturimi.aspx?veprimi=2&idKonfigAmbjente=' + queryStr);
    else if (lloji == 'Furnitor')
        popupUniversal.SetContentUrl('LupaAfateMaturimi.aspx?veprimi=1&idKonfigAmbjente=' + queryStr);
    else popupUniversal.SetContentUrl('LupaAfateMaturimi.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMaturimi, heightLupaMaturimi);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.KategoriZbritje_Click = function (headerText, queryStr, widthLupaKategoriaZbritje, heightLupaKategoriaZbritje) {
    popupUniversal.SetContentUrl('LupaKategoriZbritje.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKategoriaZbritje, heightLupaKategoriaZbritje);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.NivelCmimi_Click = function (headerText, queryStr, widthLupaNivelCmimi, heightLupaNivelCmimi, lloji) {
    popupUniversal.SetContentUrl('LupaNivelCmimiPrind.aspx?KF=' + lloji + '&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaNivelCmimi, heightLupaNivelCmimi);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.NivelZbritje_Click = function (headerText, queryStr, widthLupaNivelZbritje, heightLupaNivelZbritje) {
    popupUniversal.SetContentUrl('LupaNivelZbritjePrind.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaNivelZbritje, heightLupaNivelZbritje);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.KushtetPageses_Click = function (headerText, queryStr, widthLupaKushtePagese, heightLupaKushtePagese) {
    popupUniversal.SetContentUrl('LupaKushtePagese.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKushtePagese, heightLupaKushtePagese);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.KushteDergimi_Click = function (headerText, queryStr, widthLupaKushteDergimi, heightLupaKushteDergimi) {
    popupUniversal.SetContentUrl('LupaKushteDergimi.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKushteDergimi, heightLupaKushteDergimi);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.MenyraTransporti_Click = function (headerText, queryStr, widthLupaMenyreTransporti, heightLupamenyreTrasnporti) {
    popupUniversal.SetContentUrl('LupaMenyraTransporti.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMenyreTransporti, heightLupamenyreTrasnporti);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.AgjentShitjesh_Click = function (headerText, queryStr, widthLupaMenyreTransporti, heightLupamenyreTrasnporti) {
    popupUniversal.SetContentUrl('LupaAgjenteShitje.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMenyreTransporti, heightLupamenyreTrasnporti);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};
myButtonClickLupa.ButtonClickMagazina = function (headerText, queryStr, widthLupaMagazina, heightLupaMagazina) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
};
myButtonClickLupa.ButtonClickArtikulli = function (headerText, queryStr, widthLupaArtikull, heightLupaArtikull) {
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.SetContentUrl("LupaArtikull.aspx?idKonfigAmbjente=" + queryStr);
    popupUniversal.SetSize(widthLupaArtikull, heightLupaArtikull);
    popupUniversal.Show();
};
myButtonClickLupa.ButtonClickModelAutomjeti = function (widthLupaModelAutomjeti, heightLupaModelAutomjeti) {
    popupUniversal.SetHeaderText('Zgjidh modelin e automjetit');
    popupUniversal.SetContentUrl("LupaModelAutomjeti.aspx");
    popupUniversal.SetSize(widthLupaModelAutomjeti, heightLupaModelAutomjeti);
    popupUniversal.Show();
};
myButtonClickLupa.ButtonClickLupaEksporto = function (values, lloji, idkat, kategori, formati)  {
    var queryStringId = "";
    for (var i = 0; i < values.length; i++) {
        queryStringId += values[i] + ",";
    }
    queryStringId = queryStringId.substring(0, queryStringId.length - 1);
    popupUniversal.SetHeaderText("Eksport");
    popupUniversal.SetSize(1100, 700);
    popupUniversal.SetContentUrl('Eksport.aspx?veprimi=' + lloji + '&idDok=' + queryStringId + '&idkat=' + idkat + '&kategori=' + kategori + '&formati=' + formati);
    popupUniversal.Show();
};
myButtonClickLupa.ButtonClickLupaSerialeUnike = function (popup, header, queryObject) {

    popup.SetHeaderText(header);
    popup.SetSize(650, 600);
    popup.SetContentUrl(Utils.buildUrl('LupaSerialeUnike.aspx', queryObject));
    popup.Show();
};
myButtonClickLupa.ButtonClickGjeneral = function (contentUrl, headerText, widthLupa, heightLupa) {
    popupUniversal.SetContentUrl(contentUrl);
    popupUniversal.SetSize(widthLupa, heightLupa);
    popupUniversal.SetHeaderText(headerText);
    popupUniversal.Show();
};