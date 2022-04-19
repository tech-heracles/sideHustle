﻿;var MONTH_NAMES = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
var DAY_NAMES = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');

function LZ(x) { return (x < 0 || x > 9 ? "" : "0") + x }

function formatDate(date, format) {
if(date!=null)
{
    date = new Date(date);
    format = format + "";
    var result = "";
    var i_format = 0;
    var c = "";
    var token = "";
    var y = date.getYear() + "";
    var M = date.getMonth() + 1;
    var d = date.getDate();
    var E = date.getDay();
    var H = date.getHours();
    var m = date.getMinutes();
    var s = date.getSeconds();
    var yyyy, yy, MMM, MM, dd, hh, h, mm, ss, ampm, HH, H, KK, K, kk, k;
    // Convert real date parts into formatted versions
    var value = new Object();
    if (y.length < 4) { y = "" + (y - 0 + 1900); }
    value["y"] = "" + y;
    value["yyyy"] = y;
    value["yy"] = y.substring(2, 4);
    value["M"] = M;
    value["MM"] = LZ(M);
    value["MMM"] = MONTH_NAMES[M - 1];
    value["NNN"] = MONTH_NAMES[M + 11];
    value["d"] = d;
    value["dd"] = LZ(d);
    value["E"] = DAY_NAMES[E + 7];
    value["EE"] = DAY_NAMES[E];
    value["H"] = H;
    value["HH"] = LZ(H);
    if (H == 0) { value["h"] = 12; }
    else if (H > 12) { value["h"] = H - 12; }
    else { value["h"] = H; }
    value["hh"] = LZ(value["h"]);
    if (H > 11) { value["K"] = H - 12; } else { value["K"] = H; }
    value["k"] = H + 1;
    value["KK"] = LZ(value["K"]);
    value["kk"] = LZ(value["k"]);
    if (H > 11) { value["a"] = "PM"; }
    else { value["a"] = "AM"; }
    value["m"] = m;
    value["mm"] = LZ(m);
    value["s"] = s;
    value["ss"] = LZ(s);
    while (i_format < format.length) {
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token = token + format.charAt(i_format++);
        }
        if (value[token] != null) { result = result + value[token]; }
        else { result = result + token; }
    }
    }
    return result;
}

//veprimet aritmetike ne JS kryhen sipas standartit IEEE-754 double precision floating point
//rrumbullakimet sjellin rezultat jo te sakte ne shifrat pas presjes
//funksinet me poshte rregullojne kete saktesi

function shumezo(nr1, nr2) {
    var str1 = new Array();
    var str2 = new Array();
    var nr = nr1.toString();
    str1 = nr.split('.');
    nr = nr2.toString();
    str2 = nr.split('.');

    var p1, p2;
    //nr i shifrave pas presjes per numrin e pare
    if (str1.length < 2)
        p1 = 0;
    else
        p1 = String(str1[1]).length;

    //nr i shifrave pas presjes per nr e dyte
    if (str2.length < 2)
        p2 = 0;
    else
        p2 = String(str2[1]).length;

    var string1, string2;
    var numri1, numri2;
    string1 = ""; string2 = "";
    for (var i = 0; i < str1.length; i++) {
        string1 = string1 + str1[i]; //hiqet presja nga numri, formohet string
    }

    numri1 = parseInt(string1); //ruhet numri si vlere e plote pa presje

    for (var j = 0; j < str2.length; j++) {
        string2 = string2 + str2[j]; //hiqet presja nga numri, formohet string
    }
    numri2 = parseInt(string2);

    var resultati;
    rezultati = numri1 * numri2;

    var strRezultat = rezultati.toString();

    var pos;
    pos = strRezultat.length - (p1 + p2);

    var vlKthim = "";
    vlKthim = strRezultat.substring(0, pos) + '.' + strRezultat.substring(pos, strRezultat.length);
    return vlKthim;
}
 
 
 function pjesetim (arg1, arg2) { 
    var t1 = 0, t2 = 0, r1, r2; 
    try {t1 = arg1.toString (). split (".")[ 1]. length} catch (e) {} 
    try {t2 = arg2.toString (). split (".")[ 1]. length} catch (e) {}
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""));
        r2 = Number(arg2.toString().replace(".", ""));
    }
    return (r1/r2) * Math.pow(10, t2-t1); 
}  

//Number.prototype.div = function (arg) { 
//return accDiv (this, arg); 
//}

function mbledhje (arg1, arg2) {
    var r1, r2, m; 
    try {r1 = arg1.toString (). split (".")[ 1]. length} catch (e) {r1 = 0} 
    try {r2 = arg2.toString (). split (".")[ 1]. length} catch (e) {r2 = 0}
    m = Math.pow(10, Math.max(r1, r2)); 
    return parseFloat((arg1 * m + arg2 * m) / m  );
} 

//Number.prototype.add = function (arg) { 
//return accAdd (arg, this); 
//} 

function roundDecimal(nNumber, nDecimals) {
    var tenToPower;
    var newNumber;
    var numPad = 0;
    var curDecimal;
    var locDecimal;
    var i;

    // round the number
    tenToPower = Math.pow(10, nDecimals);
    newNumber = String((Math.round(nNumber * tenToPower) / tenToPower));
    
    if (nDecimals > 0) {
        // see if we need to pad with 0's
        locDecimal = newNumber.indexOf(".");
        if (locDecimal == -1) {
            // no decimal at all
            newNumber = newNumber + ".";
            numPad = nDecimals
        } else {
            // how much padding do we need?
            curDecimal = (newNumber.length - locDecimal) - 1;
            if (curDecimal < nDecimals) {
                numPad = nDecimals - curDecimal;
            }
        }

        // pad the end with 0's
        for (i = 0; i < numPad; i++) {
            newNumber = newNumber + "0";        
        }
    }

    return newNumber;
}

function roundNumber(rnum, rlength) { 
  return   Math.round(rnum*Math.pow(10,rlength))/Math.pow(10,rlength);
  
}
