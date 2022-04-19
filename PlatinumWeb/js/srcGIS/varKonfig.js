var varSettings = {
    translateArray: [],
    geometryTypes: ["POINT", "MULTIPOINT", "LINESTRING", "MULTILINESTRING", "POLYGON", "MULTIPOLYGON"],
    webConfig: {
        idPerdoruesi: 0,
        idNdermarrja: 0,
        idNdermVit: 0,
        idGjuha: 0,
        teDrejta: [],
        projeksione: [],
        projeksioneArrayStore: [],
        layers: [],
        treeStructure: [],
        userLogin: [],
        userAutoLogin: "vizitor",
        sendEmails: [],
        ndermarrjeLogin : [],
    },
    proxyConfig: {
        url: "GISProxyGEO.ashx?scopeID=" + Utils.getUrlVar("scopeID") + "&url=", //url: "GISProxyGEO.ashx?url=",
        wfsUrl: "wfs",
        publicProxy: "GISPublicProxy.ashx?url=",
        publicProxyFullUrl: window.location.origin + "/GISPublicProxy.ashx?url=",
    },
    uploadConfig: {
        lgpx: '',
        skedaretUploadArray: [],
    },
    
    editim: {
        cmbLayesStore: {},
        controlClickFromWmsLayer: {},
        controlClickFromWmsLayerWeb: {},
        controlClickFromWmsLayerDel: {},
        controlClickFromWmsLayerMerge: null,

        objLidhjeGisWeb: {
            veprimi: 'DEFAULT', //'INSERT','UPDATE','DELETE''POLYGON((2206019.853779966 5060175.552445232,2206036.241747844 5060178.842506439,2206019.8990954123 5060257.708884846,2206004.154248417 5060254.558201708,2206019.853779966 5060175.552445232))'
            gid: 0,
            the_geom: '',
            IDLAYER: 0,
            IDLAYERSTYPE: 0,
            IDMAGAZINA: 0,
            IDKODIFIKIMI: 0,
            IDARTIKULLI: 0,
            IDSERIALI: 0,
            IDKOKADOK: 0,
            IDTRUPIDOKLIDHES: 0,
            DTMODIFIKIMI: '01/01/1990',
            IDPERDORUESI: 0,
            IDNDERMARJE: 0,
            KODI: '',
            PERSHKRIMI: '',
            SERIALKOD: '',
            KODKODIFIKIMI: '',
            gidPrindi: 0,
            kodPrindi: '',
            NRSTATUSI: '',
        },

        objLidhjeGisWebAfishim: {
            ADRESA: '',
            cols: [],
        },

        objOpenLayer: {
            id: "",
            layerSelId: "",
            layerSelNrStatus: ""
        },

        objPanel: {
            tbar: [],
            frmeditim: undefined,
            bbar: [],

            //variabel qe perdoret per te pastruar popupin e insertit kur caktivizohen butonat e insertit. True kur shtypen butonat, false kur thirret deaktivizimi i tyre pasi vizatohet objekti ne harte
            mbyllPopupEditimInserti: true,
        },

        allControls: {
            splitControl: {},
            splitControlPolygon: {},
            dragControlEditim: {},
            snapControl: {},
            undoRedoDrawFeatureControl: null,
        },

        allVectors: {
            wfsSelectedItem: {}
        },

        objZgjedhurMerge : false,
    },
    kerkimHapsinor: {
        polygonLayer: undefined,
        polygonControl: undefined,
        bboxKerkimHapsinor: 0,
    },
    kerkimResultSelected: {
        arrayKerkimiObjects: undefined,
        arrayKerkimiLayerIDs: 0,
    },
    konfigurimeProjektesh: {
        tipeLayerMeAutorizim: ['5', '6', '7', '8', '9', '10', '11'],
    },
    responseComboListAtr: [],
    shkoNeXY: {
        ngaEditimi: false,
        title: "",
        history: undefined,
    },
    kufiShqiperi: {
        WGS84: [[21.068822815239734, 42.667376918567086], [19.187327591033, 42.653217203546355], [19.268228053516264, 39.6323021657143], [21.0652722516752, 39.645031144994412], [21.068822815239734, 42.667376918567086]]
    },
    printimiPdf: {
        meShpjegues: 0,
        ngaEditimi: false,
        whereQuery: '',
    },
    ngjyra: {
        buttonat: '#FF8894'
    },
    info: {
        layesStore: [],
        windowTitles: {} ,
    },
    measure: {
        merrKoordinata: {},
    },
    theme: {
        urlDefault: "../images/GIS/DEFAULT/",
        urlCustom: "../images/GIS/Red/",  //"../images/GIS/" + gisGlobalColor + "/",  
        color: "../images/GIS/Red/" // urlCustom.substring(0, urlCustom.length - 1).substring(14)
    },
    ndryshoStilPerLayer: {
        count: 0,
        idLayer: 550,
        style1: 'V_GIS_Layer_DPSH_AFATE',
        style2: 'V_GIS_Layer_DPSH_AFATE2',
        interval: 2000
    },
};

var adresaServer = window.location.origin;
var urlDokumentimiUpload = "../UploadFiles/dokGeo/";
var urlApp = adresaServer + "/";

var adresaGeonetwork = window.location.protocol + "//" + window.location.hostname + ":8091";


var pjesaReplaceGeonetwork = window.location.protocol + "//localhost:8091";

var adresaProxy = urlApp + "GISProxyGEO.ashx";
adresaProxy = adresaProxy.replace(window.location.protocol + '//', '');

var projeksioniGeo = "EPSG:900913";//kjo eshte metrike google
var projeksioniGeoV = "EPSG:32634";//metrike shqiptar(UTM)
var projektioniKorGeo = "EPSG:4326";//kjo eshte me grade
var projeksioniDisplay = 'EPSG:32634';
var projektioniAlb86 = "EPSG:28404"; //"ALB86";

var emerServerPlusPorte = adresaServer.replace(window.location.protocol + '//', '');

var emerKoloneIdDytesore = 'id_dytesore';//kjo eshte emri i kolones qe do te ruaj nje id_dytesore
var emerKoloneDokumentinKonf = 'dokumentim';//do ndryshuar manualisht tek funksionephp.js.php
var emerKoloneIdLidheseGis = 'gr_gis_id';//id qe lidh objektin geografik me te dhenat  hyrese GIS
var emrKolDisableQePlotesohen = new Array("magazine_nr", "magazine_date", "magazine_emertim", "artikull_nr", "artikulli_pershkrimi");//duhet te jene me emer te njejte me te dhenat qe vijne nga json dhe me emrat e kolonave ne db geo
var emrKolQeNukRuhen = new Array("artikulli_pershkrimi");//jane emrat e kolonave qe afishohen por nuk ruhen ne atributet e objektit geografik
var emerKoloneKohaInsert = 'koha_insertimit';//emri i kolones qe do te mbaje kohen kur u be shtimi
var emerKoloneKohaUpdate = 'koha_ne_modifikimin_e_fundit';
var kolonatFshehurneInfo = new Array("id_dytesore");
var kolonatQeHiqenNgaObjGeo = new Array('id_vetjake');
var krijuarNga = 'krijuar_nga';
var modifikuarNeFund = 'modifikuar_ne_fund';

var emerKoloneIdLidheseGisZ = 'Id_Alpha';//id qe lidh objektin geografik me te dhenat  hyrese GIS
var emrKolDisableQePlotesohenZ = new Array("Zona_Pershkrim");
var kontaktUrl = "http://www.turizmi.gov.al/al/kontakt/kontakt";
var homePageUrl = "FaqeKryesore.aspx?vjenNga=GIS";

var urlThemeUI = "../images/GIS/UIRed/"
var gjuhaUserLoguar = 'sq';//rregulloje
var projektiAktual = {
    success: true,
    data:
        {
            id: 1,
            kodiProjektitAktiv: "vendDepMbetjetNdryshe",
            theme: "RED",
            versioniAktual: "2.4.2"
        }
} //rregulloje merre nga db
var id_tabele_kerkimShpejte = 39;
var emri_tabele_kerkimShpejte = "[T_GIS_Layer_QENDRABANIMIAL]";
var emriKolKerkimShpejteComboValue = "gid";//rregulloji
var emriKolKerkimShpejteComboAfish = "Pershkrimi"; //"Emri";//rregulloji
var emriKolKerkimShpejteComboAfish2 = "Specifikimi"; //Emri_Bashk";//rregulloji
var titulliLayerOverLayerInfo46 = "Bashkite";//rregulloji
var titulliLayerOverLayerInfo39 = "Bashkite 39";
var titulliFolderContainerVersioni1 = "Kufijte 47";
var titulliFolderContainerVersioni2 = "Kufijte 39";
var arrayAtributetAfishuarOverInfo = ["Popullsia", "Popullsia ", "Population", "Population ", "Siperfaqja", "Area"]
var arrayAtributetAfishuarOverInfoPrap = ["banore (Burimi:INSTAT)", "banore (Burimi:Gjendja Civile)", "inhabitants (Source INSTAT)", "inhabitants (Source: Civil Register)", " km2", " km2"];
var titujtLayerOverLayersInfo = ["Bashkite", "Popullsia e Bashkive 61"];
var arrayButonat = [1, 2, 3, 4, 5];//rregulloje
var loginVersion = 0;
var userLoguar = "";
var idFolderContainerVersioni1 = 0;//rregulloji,sduhen fare
var idFolderContainerVersioni2 = 0;
