

$.noty.defaults = {
    layout: 'imbTopCenter', //'topCenter' //imbTopCenter 
    imbTopCenterWidth: "50%", //default eshte 40%
    killOldest: true, //true per te bere maksimumin e elementeve ne radhe sa maxVisible - pra te hiqet mesazhi me i vjeter nese kemi arritur maxVisible dhe vjen mesazh i ri 
    theme: 'relax', // or 'relax' //bootstrapTheme
    type: 'alert',
    text: '', // can be html or string
    dismissQueue: true, // If you want to use queue feature set this true
    template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
    animation: {
        open: { height: 'toggle' }, // or Animate.css class names like: 'animated bounceInLeft'
        close: { height: 'toggle' }, // or Animate.css class names like: 'animated bounceOutLeft'
        easing: 'swing',
        speed: 500 // opening & closing animation speed
    },
    timeout: 5000, // delay for closing event. Set false for sticky notifications
    force: false, // adds notification to the beginning of queue when set to true
    modal: false,
    maxVisible: 3, // you can set max visible notification for dismissQueue true option,
    killer: false, // for close all notifications before show
    closeWith: ['click'], // ['click', 'button', 'hover', 'backdrop'] // backdrop click will close all notifications
    callback: {
        onShow: function (e) {

            //            this.options.text = myTimestamp() + " - " + this.options.text;
        },
        afterShow: function () {
            //$(this.$message).find(".noty_text").text(myTimeStamp() + " - " + $(this.$message).find(".noty_text").text());
        },
        onClose: function () { },
        afterClose: function () { },
        onCloseClick: function () { },
    },
    buttons: false // an array of buttons
};