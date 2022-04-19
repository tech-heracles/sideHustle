window.Arkiva = (function () {
    var docTypes = [".ods", ".pdf", ".odt", ".txt", ".ODS", ".PDF", ".ODT", ".TXT"];
    var imageTypes = [".jpg", ".png", ".jpeg", ".JPG", ".PNG", ".JPEG"];
    var idObjekti = 0;
    var idKategoriObjekti = 0;
    var idPerdorues = 0;
    var selectedFile;
    var items;
    var $currentDefaultIcon = undefined;
   
    var Init = function (idDok, idKategoria, fileManager) {
        myFaqeCelje.shtoHandlerSession();
        idObjekti = idDok;
        idKategoriObjekti = idKategoria;
        idPerdorues = hfIdPerdorues.Get("hfIdPerdorues");
        items = fileManager.GetItems();
        
    };
    var fileMgrInit = function fileManager_Init(s, e) {
        var arkivaHF = parent["hfArkiva"];
        if (arkivaHF == undefined) {
            console.log("mungon hiddenFieldi per arkiven tek ambjenti i regjistrimit te dokumentave");
            return;
        }
        //ruaj ne hiddenfieldin e ambjentit qe perfshin arkiven rootFolderin
        //kjo behet me qellim qe te dallojme nese po shtojme dokument te ri apo jo
        arkivaHF.Set("rootFolder", hfMyArkiva.Get("rootFolder"));
        
    };
    var OnCustomCommand = function (s, e) {     
        var fileExt;   
        
        switch (e.commandName) {
            case 'openDocument':
                selectedFile = s.GetSelectedFile();
                if (!selectedFile) {
                    myMesazh.ShtoMesazhInformues("Ju lutem zgjidhni nje dokument!");
                    console.warn("nuk eshte lloj dokumenti i njohur, nese eshte dokument qe mund te hapet nga ky viewer duhet shtuar tek lista e llojeve te pranuara");
                    return;
                }
                fileExt = getFileExt(selectedFile.name);
                if (isDocument(fileExt)) {
                    openDocument(s);
                    return;
                }
                console.warn("nuk eshte lloj dokumenti i njohur, nese eshte dokument qe mund te hapet nga ky viewer duhet shtuar tek lista e llojeve te pranuara");
                myMesazh.ShtoMesazhInformues("Lloji i tipit te skedarit nuk eshte ne listen e tipeve te lejuar!");
                break;
            case 'openImages':
                //if (isImage(fileExt)) {
                openImageSlider(s);
                //}
                //else
                //    console.warn("nuk eshte lloj imazhi i njohur, nese eshte imazh duhet shtuar tek lista e llojeve te pranuara");
                break;
            case "setThumbnail":
                selectedFile = s.GetSelectedFile();
                if (!selectedFile) {
                    myMesazh.ShtoMesazhInformues("Ju lutem zgjidhni nje foto per thumbnail!");
                    console.warn("nuk eshte lloj dokumenti i njohur, nese eshte dokument qe mund te hapet nga ky viewer duhet shtuar tek lista e llojeve te pranuara");
                    return;
                }
                var extention = getFileExt(selectedFile.name);
                if (isImage(extention))
                {
                    var full = selectedFile.fileManager.cpPathArkiva + selectedFile.GetFullName("\\");
                    $.ajax({
                        url: Utils.getServerApiUrl("Celje", "vendosThumbnailDefaultArkiva"),
                        data: JSON.stringify({ idObjekti: idObjekti, idKategoriObjekti: idKategoriObjekti, extention: extention, full: full, name: selectedFile.name, idPerdorues:idPerdorues })
                    }).done(function (result) {
                        updateDefaultThumbnail(result, s);
                    }).fail(function (err) {
                        console.log(err);
                    });
                    
                }
                break;
            default:
                myMesazh.ShtoMesazhGabimi("Lloj komande e panjohur!");
                console.error("Custom commanName e panjohur per file managerin: " + e.commandName + ";");
                break;
        }
    };
    var updateDefaultThumbnail = function (result, s)
    {
        var selected = s.GetSelectedFile();
        removeCssItem();
        setCssItem(selected.elementID);
        //alert("TADAA");
        
    }
    var setCssItem=function (itemId)
    {
        $currentDefaultIcon = $("#" + itemId);
        if ($currentDefaultIcon) {
            if ($currentDefaultIcon.hasClass("dxh0"))
            {
                $currentDefaultIcon.removeClass("dxh0");
                $currentDefaultIcon.addClass("dxh0Custom");
            }
            if ($currentDefaultIcon.hasClass("dxh2")) {
                $currentDefaultIcon.removeClass("dxh2");
                $currentDefaultIcon.addClass("dxh2Custom");
            }
           
            $currentDefaultIcon.addClass("thumbnailDefault");
            $currentDefaultIcon.hover(
                function ()
                {
                    $currentDefaultIcon.css("border", "outset 10px!important");
                    $currentDefaultIcon.attr('style', 'border: outset 10px!important');
                },
                function ()
                {
                });

            
        }
    }
    var removeCssItem = function ()
    {// thirre para setit
        if ($currentDefaultIcon) {
            $currentDefaultIcon.removeClass("thumbnailDefault");
            $currentDefaultIcon.unbind('mouseenter mouseleave');
            $currentDefaultIcon.css("border", "");
        }
        
    }
    var checkIfNotFolder = function (file) {
        if (file.isFolder) {
            console("Nuk perdoret mbi folder!");
            return;
        }
    };
    var openImageSlider = function (s) {        
        var myFiles = s.GetItems();
        var jsonOfImageUrls = [];
        $.each(myFiles, function (index, elem) {
            if (elem.isFolder || !isImage(getFileExt(elem.name)))
                return true;
            jsonOfImageUrls.push({ "Path": elem.GetFullName(), "FileName": elem.name });
        });
        addPrefixUrl(jsonOfImageUrls, s.cpOpenImageUrlPrefix);
        HapLupeImazheArkive(jsonOfImageUrls, { prependSelector: "body" }); //"#" + $(file.fileManager.GetMainElement()).attr("id")
    };
    var openDocument = function (s) {
        var file = s.GetSelectedFile();
        checkIfNotFolder(file);
        window.open(file.fileManager.cpOpenDocUrlPrefix + file.GetFullName(), "_blank");
    };
    var getFileExt = function (fileName) {
        var tmpArrayName = fileName.split(".");
        return "." + tmpArrayName[tmpArrayName.length - 1];
    };
    var doubClickFile = function (s, e) {
        var fileExt = getFileExt(s.GetSelectedFile().name);
        if (isDocument(fileExt)) {
            openDocument(s);
            return;
        }
        if (isImage(fileExt)) {
            openImageSlider(s);
            return;
        }
    };
    var addPrefixUrl = function (arrayItems, prefix) {
        $.each(arrayItems, function (index, elem) {
            elem["Path"] = prefix + elem["Path"];
        });
    };
    var isDocument = function (elemToFind) {
        
        return isFileType(elemToFind, docTypes);
    };
    var isImage = function (elemToFind) {
        
        return isFileType(elemToFind, imageTypes);
    };
    var isFileType = function (elemToFind, fileExt) {
        var myFindings = fileExt.find(function (element) {
            return element == elemToFind;
        });
        return myFindings ? myFindings.length > 0 : myFindings;
    };   
    var HapLupeImazheArkive = function (listaUrl, popUpOptions) {
        var titulliModal = "Imazhe";
        var opsionMbyllje = "Mbyll";
        var defaults = { prependSelector: "#bootPopUp", dialogClass: "dialog-imazhe-arkive", contentClass: "content-imazhe-arkive",titulli:titulliModal, text:{mbyll: opsionMbyllje} };
        options = $.extend({}, defaults, popUpOptions);
        if (typeof $(options.prependSelector).html() == "undefined")
            console.error("Selektori: " + options.prependSelector + "; nuk ekziston ne ambient!");
        Utils.hapLupeImazhe(listaUrl, options);
    };
    var setThumb = function () {
        for (var i = 0; i < items.length; i++) {
            let name = fileManager.cpPathArkiva + items[i].GetFullName("\\");
            if (name == fileManager["cpthumbnailPhotoPath"]) {
                setCssItem(items[i].elementID);
                break;
            }
        }
    }
    return {
        Init: Init,
        fileManager_Init: fileMgrInit,
        OnCustomCommand: OnCustomCommand,
        HapLupeImazheArkive: HapLupeImazheArkive,
        doubClickFile: doubClickFile,
        SetDefaultThumb: setThumb
    };
})();
