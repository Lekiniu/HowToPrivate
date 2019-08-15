/// <reference path="jquery-3.3.1.min.js" />
/// <reference path="jquery-ui-1.12.1.min.js" />
/// <reference path="jquery.validate.unobtrusive.min.js" />



  



var BlockLinkObj;
// block Link
$('.delete-link').click(function () {
    BlockLinkObj = $(this);  //for future use
    $('#delete-dialog').dialog('open');
    return false; // prevents the default behaviour
});


$('#delete-dialog').dialog({
  autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
  buttons: {
     "Continue": function () {
          $.post(deleteLinkObj [0].href, function (data) {  //Post to action
             if (data == '<%= Boolean.TrueString %>') {
                deleteLinkObj.closest("tr").hide('fast'); //Hide Row
                //(optional) Display Confirmation
             }
             else {
                //(optional) Display Error
             }
            });
          $(this).dialog("close");
       },
      "Cancel": function () {
          $(this).dialog("close");
          }
      }
 });