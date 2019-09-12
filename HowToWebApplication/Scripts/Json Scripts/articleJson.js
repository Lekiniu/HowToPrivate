

//$(function () {
//    $.ajaxSetup({ cache: false });
//    $("a[data-modal]").on("click", function (e) {
//        $('#myModalContent').load(this.href, function () {
//            $('#myModal').modal({
//                keyboard: true
//            }, 'show');
//            bindForm(this);
//        });
//        return false;
//    });
//});

$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on('click', 'a.modal-data', function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').appendTo("body").modal({
                keyboard: true
            }, 'show');
            //bindForm(this);
        });
        return false;
    });
});

//$(function () {
//    $.ajaxSetup({ cache: false });
//    $('body').on('click', 'a.modal-Images', function (e) {
//        $('#myModalImagesContent').load(this.href, function () {
//            $('#myModalImages').appendTo("body").modal({
//                keyboard: true
//            }, 'show');
//            //bindForm(this);
//        });
//        return false;
//    });
//});





$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on('click', 'a.modal-form', function (e) {
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



    $(function(){
        $.ajaxSetup({ cache: false });
        $('body').on('click', 'a.delete-data', function (e) {
            $('#myModalContent').load(this.href, function () {
                $('#myModal').appendTo("body").modal({
                    keyboard: true
                }, 'show');
                deleteData();
            });
            return false;
        });
    });


function deleteData() {
    var id = $('#articleId').val();
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $('#submitBtn').on('click',function () {
            $('#progress').show();
        $.ajax({
            url: 'DeleteConfirmedArticle',
            type: 'POST',
            data: {
                __RequestVerificationToken: token,
                articleId: id
            },
                success: function (result) {
                    if (result.success) {
                        $('#myModal').modal('hide');
                        $('#progress').hide();
                        location.reload();
                    } else {
                        $('#progress').hide();
                        $('#myModalContent').html(result);
                        deleteData();
                    }
                }
            });
            return false;
        });
    }
