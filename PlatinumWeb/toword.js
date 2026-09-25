var th = ['','mije','milion', 'miliard','bilion'];

var dg = ['zero', 'nje', 'dy', 'tre', 'kater', 'pese', 'gjashte', 'shtate', 'tete', 'nente'];
var tn = ['dhjete', 'njembedhjete', 'dymbedhjete', 'trembedhjete', 'katermbedhjete', 'pesembedhjete', 'gjashtembedhjete', 'shtatembedhjete', 'tetembedhjete', 'nentembedhjete'];
var tw = ['njezet', 'tridhjete', 'dyzet', 'pesedhjete', 'gjashtedhjete', 'shtatedhjete', 'tetedhjete', 'nentedhjete'];

function toWords(s) {
    if (s == null) return '';
    s = s.toString(); 
    s = s.replace(/[\, ]/g, '');
    if (s === '')
        return '';
//KEVI
        if (isNaN(s))
            return 'nuk eshte numer';
        if (parseFloat(s) == 0)
            return 'Zero';
    var numri = s.split('.');
    var parapikes = numri[0];
    var paspikes = numri[1];
    
    var x = s.indexOf('.');
    if (x == -1) x = s.length;
    if (x > 15) return 'numer shume i madh';

    var n = s.split('');
    var str = '';
    var sk = 0;

    for (var i = 0; i < x; i++) 
    {
        if ((x - i) % 3 == 2) 
        {
            if (n[i] == '1') 
            {
                str = str + tn[Number(n[i + 1])] + ' ';
                i++; 
                sk = 1;
            }
            else if (n[i] != 0) 
            {
                str = str + tw[n[i] - 2] + ' '; 
                sk = 1;
            }
        }
        else if (n[i] != 0) 
        {
            str = str + dg[n[i]] + ' ';
            if ((x - i) % 3 == 0)
                str = str + 'qind ';
            sk = 1;
        }
        
        if ((x - i) % 3 == 1) 
        {
            if (sk)
               str = str + th[(x - i - 1) / 3] + ' ';
            sk = 0; 
       }
   }
     
   if (x != s.length) 
    {
        var y = s.length;
        str = str + ' pike ';
        str = str + toWords(paspikes);
    }
    

    return str.replace(/\s+/g, ' ');
}