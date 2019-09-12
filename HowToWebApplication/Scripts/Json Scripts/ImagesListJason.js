
$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on('click', 'a.modal-Images', function (e) {
        $('#myModalImagesContent').load(this.href, function () {
            $('#myModalImages').appendTo("body").modal({
                keyboard: true
            }, 'show');
            //bindForm(this);
        });
        return false;
    });
});