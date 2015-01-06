var $ = jQuery.noConflict();

$(document).ready(function() {
    $("#subLayoutReferersDialog").dialog({
        autoOpen: false,
        modal: true,
        height: 800,
        width: 1024
    });

    var subLayoutDialog = $('#subLayoutReferersDialog');
    var subLayoutDialogFrame = $("#subLayoutRefererIframe");

    $('.referersDetails').click(function (e) {
        var referId = this.getAttribute("data-refer-id");

        subLayoutDialogFrame.attr('src', './SublayoutsTunerReferrers.aspx?id=' + referId);
    
        $('#subLayoutReferersDialog').dialog('open');

        e.preventDefault();
    });

    /*$(function () {
        $("#tabs").tabs();
    });*/
});
