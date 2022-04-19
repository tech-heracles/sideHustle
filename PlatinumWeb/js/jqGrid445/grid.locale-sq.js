; (function ($) {
    /**
     * jqGrid English Translation
     * Tony Tomov tony@trirand.com
     * http://trirand.com/blog/ 
     * Dual licensed under the MIT and GPL licenses:
     * http://www.opensource.org/licenses/mit-license.php
     * http://www.gnu.org/licenses/gpl.html
    **/
    $.jgrid = $.jgrid || {};
    $.extend($.jgrid, {
        defaults: {
            recordtext: "Shfaq {0} - {1} nga {2}",
            emptyrecords: "Nuk ka asnje regjistrim per te pare",
            loadtext: "Duke u ngarkuar ...",
            pgtext: "Faqe {0} nga {1}"
        },
        search: {
            caption: "Kerko...",
            Find: "Gjej",
            Reset: "Rivendos",
            odata: ['equal', 'not equal', 'less', 'less or equal', 'greater', 'greater or equal', 'begins with', 'does not begin with', 'is in', 'is not in', 'ends with', 'does not end with', 'contains', 'does not contain'],
            groupOps: [{ op: "AND", text: "all" }, { op: "OR", text: "any" }],
            matchText: " pershtat",
            rulesText: " rregulla"
        },
        edit: {
            addCaption: "Shto regjistrim",
            editCaption: "Modifiko regjistrim",
            bSubmit: "Submit",
            bCancel: "Dil",
            bClose: "Mbyll",
            saveData: "Te dhenat u ndryshuan! Doni te ruani ndryshimet?",
            bYes: "Po",
            bNo: "Jo",
            bExit: "Dil",
            msg: {
                required: "Fusha eshte e detyrueshme per t'u plotesuar",
                number: "Ju lutemi, vendosni nje numer te sakte",
                minValue: "Vlera duhet te jete me e madhe ose e barabarte me ",
                maxValue: "Vlera duhet te jete me e vogel ose e barabarte me ",
                email: "E-maili nuk eshte i sakte",
                integer: "Vlera duhet te jete numer i plote i vlefshem",
                date: "Venodsni nje vlere numerike te plote e te vlefshme",
                url: "URL nuk eshte e sakte. Duhet te vendosni prefiksin ('http://' or 'https://')",
                nodefined: " nuk eshte e percaktuar!",
                novalue: " duhet te kthehet nje vlere!",
                customarray: "Funksioni i kostumizuar duhet te ktheje nje koleksion te dhenash!",
                customfcheck: "Funksioni i kostumizuar duhet te jete prezent gjate kontrollit te kostumizuar!"

            }
        },
        view: {
            caption: "Shiko regjistrim",
            bClose: "Mbyll"
        },
        del: {
            caption: "Fshi",
            msg: "Doni te fshini rreshtat e zgjedhura?",
            bSubmit: "Fshi",
            bCancel: "Anullo"
        },
        nav: {
            edittext: "",
            edittitle: "Modifiko rreshtat e zgjedhur",
            addtext: "",
            addtitle: "Shto rresht te ri",
            deltext: "",
            deltitle: "Fshi rreshtat e zgjedhur",
            searchtext: "",
            searchtitle: "Gjej regjistrim",
            refreshtext: "",
            refreshtitle: "Ringarko Griden",
            alertcap: "Kujdes",
            alerttext: "Ju lutemi, zgjidhni nje rresht!",
            viewtext: "",
            viewtitle: "Shiko rreshtat e zgjedhur"
        },
        col: {
            caption: "Zgjidh Kolonat",
            bSubmit: "Ok",
            bCancel: "Anullo"
        },
        errors: {
            errcap: "Gabim",
            nourl: "Nuk ka asnje url te vendosur",
            norecords: "Nuk ka asnje regjistrim per te vazhduar",
            model: "Gjatesia e colNames <> colModel!"
        },
        formatter: {
            integer: { thousandsSeparator: ",", defaultValue: '0' },
            number: { decimalSeparator: ".", thousandsSeparator: ",", decimalPlaces: 2, defaultValue: '0.00' },
            currency: { decimalSeparator: ".", thousandsSeparator: ",", decimalPlaces: 2, prefix: "", suffix: "", defaultValue: '0.00' },
            date: {
                dayNames: [
                    "Die", "Hen", "Mar", "Mer", "Enj", "Pre", "Shtu",
                    "E diele", "E hene", "E marte", "E mermkure", "E enjte", "E premte", "E shtune"
                ],
                monthNames: [
                    "Jan", "Shku", "Mar", "Pri", "Maj", "Qer", "Korr", "Gus", "Shta", "Tet", "Nen", "Dhj",
                    "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor"
                ],
                AmPm: ["am", "pm", "AM", "PM"],
                S: function (j) { return j < 11 || j > 13 ? ['st', 'nd', 'rd', 'th'][Math.min((j - 1) % 10, 3)] : 'th'; },
                srcformat: 'Y-m-d',
                newformat: 'n/j/Y',
                masks: {
                    // see http://php.net/manual/en/function.date.php for PHP format used in jqGrid
                    // and see http://docs.jquery.com/UI/Datepicker/formatDate
                    // and https://github.com/jquery/globalize#dates for alternative formats used frequently
                    // one can find on https://github.com/jquery/globalize/tree/master/lib/cultures many
                    // information about date, time, numbers and currency formats used in different countries
                    // one should just convert the information in PHP format
                    ISO8601Long: "Y-m-d H:i:s",
                    ISO8601Short: "Y-m-d",
                    // short date:
                    //    n - Numeric representation of a month, without leading zeros
                    //    j - Day of the month without leading zeros
                    //    Y - A full numeric representation of a year, 4 digits
                    // example: 3/1/2012 which means 1 March 2012
                    ShortDate: "n/j/Y", // in jQuery UI Datepicker: "M/d/yyyy"
                    // long date:
                    //    l - A full textual representation of the day of the week
                    //    F - A full textual representation of a month
                    //    d - Day of the month, 2 digits with leading zeros
                    //    Y - A full numeric representation of a year, 4 digits
                    LongDate: "l, F d, Y", // in jQuery UI Datepicker: "dddd, MMMM dd, yyyy"
                    // long date with long time:
                    //    l - A full textual representation of the day of the week
                    //    F - A full textual representation of a month
                    //    d - Day of the month, 2 digits with leading zeros
                    //    Y - A full numeric representation of a year, 4 digits
                    //    g - 12-hour format of an hour without leading zeros
                    //    i - Minutes with leading zeros
                    //    s - Seconds, with leading zeros
                    //    A - Uppercase Ante meridiem and Post meridiem (AM or PM)
                    FullDateTime: "l, F d, Y g:i:s A", // in jQuery UI Datepicker: "dddd, MMMM dd, yyyy h:mm:ss tt"
                    // month day:
                    //    F - A full textual representation of a month
                    //    d - Day of the month, 2 digits with leading zeros
                    MonthDay: "F d", // in jQuery UI Datepicker: "MMMM dd"
                    // short time (without seconds)
                    //    g - 12-hour format of an hour without leading zeros
                    //    i - Minutes with leading zeros
                    //    A - Uppercase Ante meridiem and Post meridiem (AM or PM)
                    ShortTime: "g:i A", // in jQuery UI Datepicker: "h:mm tt"
                    // long time (with seconds)
                    //    g - 12-hour format of an hour without leading zeros
                    //    i - Minutes with leading zeros
                    //    s - Seconds, with leading zeros
                    //    A - Uppercase Ante meridiem and Post meridiem (AM or PM)
                    LongTime: "g:i:s A", // in jQuery UI Datepicker: "h:mm:ss tt"
                    SortableDateTime: "Y-m-d\\TH:i:s",
                    UniversalSortableDateTime: "Y-m-d H:i:sO",
                    // month with year
                    //    Y - A full numeric representation of a year, 4 digits
                    //    F - A full textual representation of a month
                    YearMonth: "F, Y" // in jQuery UI Datepicker: "MMMM, yyyy"
                },
                reformatAfterEdit: false
            },
            baseLinkUrl: '',
            showAction: '',
            target: '',
            checkbox: { disabled: true },
            idName: 'id'
        }
    });
})(jQuery);
