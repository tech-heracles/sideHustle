<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPopupValidimMarreveshje.ascx.cs" Inherits="PlatinumWeb.ucPopupValidimMarreveshje" %>

<link href="fine-uploader/fine-uploader-new.css" rel="stylesheet"/>
<script src="fine-uploader/jquery.fine-uploader.js"></script>
<script src="js/Utils-IMB.2.1.js"> </script>
<script src="js/ucPopupValidimMarreveshje.js"> </script>
<style>
    a.disabled {
        cursor: not-allowed;
        pointer-events: none;
        color: gray !important;
    }
</style>

    <!-- #include file="~/fine-uploader/templates/imb-singleFile.html" -->
<div class="modal fade" id="ValidoMarreveshje" tabindex="-1" role="dialog" >
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Valido Marreveshje</h4>
            </div>
            <div class="modal-body">
                <%--<div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon">
                            <input type="radio" name="llojValidim" id="llojValidimID" value="ID" >
                        </span>
                        <input type="text" class="form-control" id="idmarreveshje_text" placeholder="ID Marreveshje" />
                        <span class="input-group-btn">
                            <button type="button" class="btn btn-outline-secondary" id="buttonKerkoId">Kerko</button>
                        </span>
                    </div>
                    <br />
                    <input type="radio" name="llojValidim" id="llojValidimFile" value="File"> Ngarko File 
                    <div id="fine-uploader"></div>
                    
                </div>--%>
                <div class="panel-group" id="accordion">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title" >
                                <a id="togglecollapse1" data-toggle="collapse" data-parent="#accordion" href="#collapse1">ID Marreveshje</a>
                            </h4>
                        </div>
                        <div id="collapse1" class="panel-collapse collapse ">
                            <div class="panel-body form-group">
                                <div class="input-group">
                                    <input type="text" class="form-control" id="idmarreveshje_text" placeholder="ID Marreveshje" />
                                    <span class="input-group-btn">
                                        <button type="button" class="btn btn-outline-secondary" id="buttonKerkoId">Kerko</button>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a id="togglecollapse2" data-toggle="collapse" data-parent="#accordion" href="#collapse2">Ngarko File</a>
                            </h4>
                        </div>
                        <div id="collapse2" class="panel-collapse collapse">
                            <div class="panel-body">
                                <div id="fine-uploader"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="form-group">
                    <button type="button" style="float: right" class="btn btn-outline-secondary" id="buttonNgarkoFile" >Ngarko</button>
                </div>
            </div>
        </div>

    </div>
</div>
