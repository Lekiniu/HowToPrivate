
        // block user

var BlockLinkObj;
$(".block-link").click(function () {
    BlockLinkObj = $(this);  //for future use
    $('#block-dialog').dialog('open');
            return false; // prevents the default behaviour
        });


$('#block-dialog').dialog({
    autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
    buttons: {
        "Continue": function () {
            $.post(BlockLinkObj[0].href, function (data) {  //Post to action
                if (data == '<%= Boolean.TrueString %>') {
                    BlockLinkObj.closest("tr").hide('fast'); //Hide Row
                    //(optional) Display Confirmation               
                }
                else {
                    //(optional) Display Error
                }
            });
            $(this).dialog("close");
            setTimeout(function () {
                window.location.reload();
                //updateDiv();  
            }, 50);

        },
        "Cancel": function () {
            $(this).dialog("close");
        }
    }
});



// unblock user
var UnBlockLinkObj;

$(".Unblock-link").click(function () {
    UnBlockLinkObj = $(this);  //for future use
    $('#Unblock-dialog').dialog('open');
    return false; // prevents the default behaviour
});


$('#Unblock-dialog').dialog({
    autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
    buttons: {
        "Continue": function () {
            $.post(UnBlockLinkObj[0].href, function (data) {  //Post to action
                if (data == '<%= Boolean.TrueString %>') {
                    UnBlockLinkObj.closest("tr").hide('fast'); //Hide Row
                    //(optional) Display Confirmation
                }
                else {
                    //(optional) Display Error
                }
            });
            $(this).dialog("close");
            setTimeout(function () {
                window.location.reload();
                //updateDiv();                 
            }, 50);

        },
        "Cancel": function () {
            $(this).dialog("close");
        }
    }
});

function updateDiv() {

    ////$("#IsActive").load(location.href + " #IsActive>*", "");

    var link = $(".TitleEstTimeClass").attr("id").toString();
            $("#" + link).load(location.href + " #" + link + ">*", "");
        
            alert(link);
        
        }
        
