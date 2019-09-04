$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on('click', 'a.Create-form', function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').appendTo("body").modal({
                keyboard: true
            }, 'show');
            bindForm(this);
        });

        return false;
    });
});




function bindForm(dialog) {
    //var formdata = new FormData();

    //formdata.append("Images", $('#UploadFile').get(0).files[0]);

    //var other_data = ('form', dialog).serializeArray();
    //$.each(other_data, function (key, input) {
    //    formdata.append(input.name, input.value);
    //});

    //var formdata = new FormData($('form')).get(0);

    var form = $('form', dialog).get(0);
    $('form', dialog).submit(function () {
        $('#progress').show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: new FormData(form),              /*$(this).serialize(), formdata,*/
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#progress').hide();
                    location.reload();
                } else {
                    $('#progress').hide();
                    //$('#myModalContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}