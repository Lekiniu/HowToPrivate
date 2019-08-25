

//registration ajax scripts
$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on('click', '#registerLink', function (e) {
        $('#myAccountModalContent').load(this.href, function () {
            $('#myAccountModal').modal({
                keyboard: true
            }, 'show');
            bindRegistratioForm(this);
        });
        return false;
    });
});

function bindRegistratioForm(dialog) {
    $('form', dialog).submit(function () {
        //var infodata = (this).serialize();
        var form = $('form', dialog).get(0);
        $('#progress').show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: new FormData(form),             
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.success) {
                    $('#myAccountModal').modal('hide');
                    $('#progress').hide();
                    location.reload();
                } else {
                    $('#progress').hide();
                    //$('#myAccountModalContent').html(result);
                    bindRegistratioForm();
                    //alert("ola");
                }
            }
        });
        return false;
    });
}


//Login ajax scripts
$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on('click', '#loginLink', function (e) {
        $('#myLoginModalContent').load(this.href, function () {
            $('#myLoginModal').modal({
                keyboard: true
            }, 'show');
            bindLoginForm(this);
        });
        return false;
    });
});


function bindLoginForm(dialog) {
    $('form', dialog).submit(function () {
        var form = $('form', dialog).get(0);
        $('#progress').show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: new FormData(form),
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.success) {
                    $('#myLoginModal').modal('hide');
                    $('#progress').hide();
                    location.reload();
                } else {
                    $('#progress').hide();
                    //$('#myLoginModalContent').html(result);
                    bindLoginForm();
                }
            }
        });
        return false;
    });
}



//registration from login ajax scripts
$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on('click', '#registerLoginLink', function (e) {
        $('#myLoginModal').modal('hide');
        $('#myAccountModalContent').load(this.href, function () {
            $('#myAccountModal').modal({
                keyboard: true
            }, 'show');
            bindForm();
        });
        return false;
    });
});
